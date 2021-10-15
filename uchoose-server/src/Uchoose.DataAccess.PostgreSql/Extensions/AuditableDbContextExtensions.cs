// ------------------------------------------------------------------------------------------------------
// <copyright file="AuditableDbContextExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Uchoose.CurrentUserService.Interfaces;
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.DataAccess.Interfaces.Enums;
using Uchoose.DataAccess.Interfaces.EventLogging;
using Uchoose.DataAccess.Interfaces.Models;
using Uchoose.DataAccess.PostgreSql.Models;
using Uchoose.DateTimeService.Interfaces;
using Uchoose.Domain.Contracts;
using Uchoose.Domain.Settings;
using Uchoose.SerializationService.Interfaces;
using Uchoose.Utils.Contracts.Deleting;
using Uchoose.Utils.Extensions;

namespace Uchoose.DataAccess.PostgreSql.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IAuditableDbContext"/>.
    /// </summary>
    public static class AuditableDbContextExtensions
    {
        #region SaveChangeWithAuditAndPublishEventsAsync

        /// <summary>
        /// Сохранить изменения асинхронно с аудитом и публикацией доменных событий.
        /// </summary>
        /// <typeparam name="TAuditableDbContext">Тип контекста доступа к данным аудита.</typeparam>
        /// <param name="context">Контекст доступа к данным аудита.</param>
        /// <param name="eventLogger"><see cref="IEventLogger"/>.</param>
        /// <param name="mediator"><see cref="IMediator"/>.</param>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        /// <param name="dateTimeService"><see cref="IDateTimeService"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        /// <param name="entitySettings"><see cref="EntitySettings"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        public static async Task<int> SaveChangesWithAuditAndPublishEventsAsync<TAuditableDbContext>(
            this TAuditableDbContext context,
            IEventLogger eventLogger,
            IMediator mediator,
            ICurrentUserService currentUserService,
            IDateTimeService dateTimeService,
            IJsonSerializer jsonSerializer,
            IOptionsSnapshot<EntitySettings> entitySettings,
            CancellationToken cancellationToken = new())
                where TAuditableDbContext : DbContext, IAuditableDbContext
        {
            var currentUserId = currentUserService.GetUserId();
            foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = dateTimeService.NowUtc;
                        if (currentUserId != Guid.Empty)
                        {
                            entry.Entity.CreatedBy = currentUserId;
                        }

                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = dateTimeService.NowUtc;
                        if (currentUserId != Guid.Empty)
                        {
                            entry.Entity.LastModifiedBy = currentUserId;
                        }

                        break;
                    case EntityState.Deleted:
                        if (entry.Entity is ISoftDelete softDeletedEntity && entitySettings.Value.SoftDeleteEnabled)
                        {
                            softDeletedEntity.SetBySoftDeleted(currentUserId);
                            entry.State = EntityState.Modified;
                        }

                        break;
                }
            }

            var auditEntries = OnBeforeSaveChanges(context, currentUserId, jsonSerializer);
            if (currentUserId == Guid.Empty)
            {
                int result = await PublishEventsAsync(context, eventLogger, mediator, auditEntries, jsonSerializer, cancellationToken);
                return await context.BaseSaveChangesAsync(cancellationToken) + result;
            }
            else
            {
                int result = await PublishEventsAsync(context, eventLogger, mediator, auditEntries, jsonSerializer, cancellationToken);
                result += await OnAfterSaveChangesAsync(context, auditEntries.Where(_ => _.HasTemporaryProperties).ToList());
                return await context.SaveChangesAsync(true, cancellationToken) + result;
            }
        }

        #endregion SaveChangeWithAuditAndPublishEventsAsync

        #region SaveChangesWithAuditAndPublishEvents

        /// <summary>
        /// Сохранить изменения асинхронно с аудитом и публикацией доменных событий.
        /// </summary>
        /// <typeparam name="TAuditableDbContext">Тип контекста доступа к данным аудита.</typeparam>
        /// <param name="context">Контекст доступа к данным аудита.</param>
        /// <param name="eventLogger"><see cref="IEventLogger"/>.</param>
        /// <param name="mediator"><see cref="IMediator"/>.</param>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        /// <param name="dateTimeService"><see cref="IDateTimeService"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        /// <param name="entitySettings"><see cref="EntitySettings"/>.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        public static int SaveChangesWithAuditAndPublishEvents<TAuditableDbContext>(
            this TAuditableDbContext context,
            IEventLogger eventLogger,
            IMediator mediator,
            ICurrentUserService currentUserService,
            IDateTimeService dateTimeService,
            IJsonSerializer jsonSerializer,
            IOptionsSnapshot<EntitySettings> entitySettings)
                where TAuditableDbContext : DbContext, IAuditableDbContext
        {
            return SaveChangesWithAuditAndPublishEventsAsync(context, eventLogger, mediator, currentUserService, dateTimeService, jsonSerializer, entitySettings).GetAwaiter().GetResult();
        }

        #endregion SaveChangesWithAuditAndPublishEvents

        #region OnBeforeSaveChanges

        /// <summary>
        /// Получить коллекцию данных аудита перед сохранением изменений в БД.
        /// </summary>
        /// <typeparam name="TAuditableDbContext">Тип контекста доступа к данным аудита.</typeparam>
        /// <param name="context">Контекст доступа к данным аудита.</param>
        /// <param name="userId">Идентификатор пользователя, вызвавшего изменения.</param>
        /// <param name="jsonSerializer">Сериализатор json.</param>
        /// <returns>Возвращает список данных аудита.</returns>
        private static List<IAuditEntry> OnBeforeSaveChanges<TAuditableDbContext>(
            TAuditableDbContext context,
            Guid userId,
            IJsonSerializer jsonSerializer)
                where TAuditableDbContext : DbContext, IAuditableDbContext
        {
            // context.ChangeTracker.DetectChanges(); - не нужен, так как автоматически запускается
            // (если не поменяли глобальный параметр), когда вызываем ChangeTracker.Entries.
            var auditEntries = new List<IAuditEntry>();
            foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State is EntityState.Detached or EntityState.Unchanged)
                {
                    continue;
                }

                var auditEntry = new AuditEntry(entry, jsonSerializer)
                {
                    UserId = userId
                };
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Detached:
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified && !(property.OriginalValue == null && property.CurrentValue == null) && property.OriginalValue?.Equals(property.CurrentValue) != true)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                context.AuditTrails.Add(auditEntry.ToAudit());
            }

            return auditEntries.ToList();
        }

        #endregion OnBeforeSaveChanges

        #region OnAfterSaveChangesAsync

        /// <summary>
        /// Сохранить коллекцию данных аудита после сохранения изменений в БД.
        /// </summary>
        /// <typeparam name="TAuditableDbContext">Тип контекста доступа к данным аудита.</typeparam>
        /// <param name="context">Контекст доступа к данным аудита.</param>
        /// <param name="auditEntries">Коллекция данных аудита.</param>
        private static Task<int> OnAfterSaveChangesAsync<TAuditableDbContext>(
            TAuditableDbContext context,
            IReadOnlyCollection<IAuditEntry> auditEntries)
                where TAuditableDbContext : DbContext, IAuditableDbContext
        {
            if (auditEntries == null || auditEntries.Count == 0)
            {
                return Task.FromResult(0);
            }

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                context.AuditTrails.Add(auditEntry.ToAudit());
            }

            return context.BaseSaveChangesAsync();
        }

        #endregion OnAfterSaveChangesAsync

        #region PublishEventsAsync

        /// <summary>
        /// Опубликовать доменные события.
        /// </summary>
        /// <typeparam name="TAuditableDbContext">Тип контекста доступа к данным аудита.</typeparam>
        /// <param name="context">Контекст доступа к данным аудита.</param>
        /// <param name="eventLogger"><see cref="IEventLogger"/>.</param>
        /// <param name="mediator"><see cref="IMediator"/>.</param>
        /// <param name="auditEntries">Список данных аудита.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        private static async Task<int> PublishEventsAsync<TAuditableDbContext>(
            TAuditableDbContext context,
            IEventLogger eventLogger,
            IMediator mediator,
            List<IAuditEntry> auditEntries,
            IJsonSerializer jsonSerializer,
            CancellationToken cancellationToken = new())
            where TAuditableDbContext : DbContext, IAuditableDbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<IGeneratesDomainEvents>()
                .Where(x => x.Entity.DomainEvents?.Any() == true)
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    var relatedAuditEntries = auditEntries.Where(x => domainEvent.RelatedEntities.Any(t => t == x.Entry.Entity.GetType())).ToList();
                    if (relatedAuditEntries.Count > 0)
                    {
                        var oldValues = relatedAuditEntries.ToDictionary(x => x.Entry.Entity.GetType().GetGenericTypeName(), y => y.OldValues);
                        var newValues = relatedAuditEntries.ToDictionary(x => x.Entry.Entity.GetType().GetGenericTypeName(), y => y.NewValues);
                        var changes = (oldValues.Count == 0 ? null : jsonSerializer.Serialize(oldValues), newValues.Count == 0 ? null : jsonSerializer.Serialize(newValues));
                        await eventLogger.SaveAsync(domainEvent, changes);
                        await mediator.Publish(domainEvent, cancellationToken);
                    }
                    else
                    {
                        await eventLogger.SaveAsync(domainEvent, (null, null));
                        await mediator.Publish(domainEvent, cancellationToken);
                    }
                });
            await Task.WhenAll(tasks);
            return await context.BaseSaveChangesAsync(cancellationToken);
        }

        #endregion PublishEventsAsync
    }
}