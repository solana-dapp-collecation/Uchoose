// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleEventHandler.cs" company="Life Loop">
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
using Uchoose.Domain.Identity.Events.Roles;

namespace Uchoose.UseCases.Common.Features.Identity.Roles.EventHandlers
{
    /// <summary>
    /// Обработчик событий роли пользователя.
    /// </summary>
    public class RoleEventHandler :
        INotificationHandler<RoleAddedEvent>,
        INotificationHandler<RoleUpdatedEvent>,
        INotificationHandler<RoleDeletedEvent>
    {
        private readonly ILogger<RoleEventHandler> _logger;
        private readonly IStringLocalizer<RoleEventHandler> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="RoleEventHandler"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public RoleEventHandler(
            ILogger<RoleEventHandler> logger,
            IStringLocalizer<RoleEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(RoleAddedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleAddedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(RoleUpdatedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleUpdatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(RoleDeletedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleDeletedEvent)} Raised. {notification.Id} Deleted."]);
            return Task.CompletedTask;
        }
    }
}