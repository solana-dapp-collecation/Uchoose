// ------------------------------------------------------------------------------------------------------
// <copyright file="AuditableDbContext.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Uchoose.CurrentUserService.Interfaces;
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.DataAccess.Interfaces.Contracts;
using Uchoose.DataAccess.Interfaces.EventLogging;
using Uchoose.DataAccess.PostgreSql.Extensions;
using Uchoose.DateTimeService.Interfaces;
using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Entities;
using Uchoose.Domain.Settings;
using Uchoose.SerializationService.Interfaces;
using Uchoose.Utils.Extensions;

namespace Uchoose.DataAccess.PostgreSql.Persistence.Abstractions
{
    /// <inheritdoc cref="IAuditableDbContext"/>
    public abstract class AuditableDbContext :
        DbContext,
        IAuditableDbContext
    {
        private readonly IEventLogger _eventLogger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;
        private readonly IDateTimeService _dateTimeService;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IOptionsSnapshot<EntitySettings> _entitySettings;

        /// <summary>
        /// Инициализирует экземпляр <see cref="AuditableDbContext"/>.
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions"/>.</param>
        /// <param name="eventLogger"><see cref="IEventLogger"/>.</param>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        /// <param name="mediator"><see cref="IMediator"/>.</param>
        /// <param name="dateTimeService"><see cref="IDateTimeService"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        /// <param name="entitySettings"><see cref="EntitySettings"/>.</param>
        protected AuditableDbContext(
            DbContextOptions options,
            IEventLogger eventLogger,
            ICurrentUserService currentUserService,
            IMediator mediator,
            IDateTimeService dateTimeService,
            IJsonSerializer jsonSerializer,
            IOptionsSnapshot<EntitySettings> entitySettings)
            : base(options)
        {
            _eventLogger = eventLogger ?? throw new ArgumentNullException(nameof(eventLogger));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
            _entitySettings = entitySettings ?? throw new ArgumentNullException(nameof(entitySettings));
        }

        /// <inheritdoc/>
        public IDbConnection Connection => Database.GetDbConnection();

        /// <inheritdoc/>
        public bool HasChanges => ChangeTracker.HasChanges();

        /// <inheritdoc/>
        public DbSet<Audit> AuditTrails { get; set; }

        /// <summary>
        /// Схема БД, используемая по умолчанию.
        /// </summary>
        protected abstract string Schema { get; }

        #region SaveChanges

        /// <inheritdoc/>
        public virtual async Task<int> BaseSaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc cref="IAuditableDbContext"/>
        public new virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await this.SaveChangesWithAuditAndPublishEventsAsync(_eventLogger, _mediator, _currentUserService, _dateTimeService, _jsonSerializer, _entitySettings, cancellationToken);
        }

        /// <inheritdoc cref="ISupportsSavingChanges.SaveChanges"/>
        public override int SaveChanges()
        {
            return this.SaveChangesWithAuditAndPublishEvents(_eventLogger, _mediator, _currentUserService, _dateTimeService, _jsonSerializer, _entitySettings);
        }

        /// <inheritdoc/>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return SaveChanges();
        }

        #endregion SaveChanges

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (Schema.IsPresent())
            {
                modelBuilder.HasDefaultSchema(Schema);
            }

            modelBuilder.Ignore<DomainEvent>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // modelBuilder.ApplyModuleConfiguration();
        }
    }
}