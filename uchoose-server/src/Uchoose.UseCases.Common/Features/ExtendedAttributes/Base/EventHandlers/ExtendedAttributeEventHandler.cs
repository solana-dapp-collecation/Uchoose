// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeEventHandler.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Uchoose.Domain.Events.ExtendedAttributes;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Extensions;

namespace Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.EventHandlers
{
    /// <summary>
    /// Обработчик событий расширенных атрибутов сущностей.
    /// </summary>
    /// <remarks>
    /// Для локализации.
    /// </remarks>
    public class ExtendedAttributeEventHandler
    {
        // for localization
    }

    /// <summary>
    /// Обработчик событий расширенных атрибутов сущностей.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public class ExtendedAttributeEventHandler<TEntityId, TEntity> :
        INotificationHandler<ExtendedAttributeAddedEvent<TEntityId, TEntity>>,
        INotificationHandler<ExtendedAttributeUpdatedEvent<TEntityId, TEntity>>,
        INotificationHandler<ExtendedAttributeRemovedEvent<TEntity>>
            where TEntity : class, IEntity<TEntityId>
    {
        private readonly ILogger<ExtendedAttributeEventHandler> _logger;
        private readonly IStringLocalizer<ExtendedAttributeEventHandler> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <seealso cref="ExtendedAttributeEventHandler{EntityId, TEntity}"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public ExtendedAttributeEventHandler(
            ILogger<ExtendedAttributeEventHandler> logger,
            IStringLocalizer<ExtendedAttributeEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(ExtendedAttributeAddedEvent<TEntityId, TEntity> notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(ExtendedAttributeAddedEvent<TEntityId, TEntity>)} For {typeof(TEntity).GetGenericTypeName()} Raised."]);

            // TODO: добавить логику
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(ExtendedAttributeUpdatedEvent<TEntityId, TEntity> notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(ExtendedAttributeUpdatedEvent<TEntityId, TEntity>)} For {typeof(TEntity).GetGenericTypeName()} Raised."]);

            // TODO: добавить логику
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(ExtendedAttributeRemovedEvent<TEntity> notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(ExtendedAttributeRemovedEvent<TEntity>)} For {typeof(TEntity).GetGenericTypeName()} Raised. {notification.Id} Removed."]);

            // TODO: добавить логику
            return Task.CompletedTask;
        }
    }
}