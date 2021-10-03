// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimEventHandler.cs" company="Life Loop">
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
using Uchoose.Domain.Identity.Events.RoleClaims;

namespace Uchoose.UseCases.Common.Features.Identity.RoleClaims.EventHandlers
{
    /// <summary>
    /// Обработчик событий разрешений роли пользователя.
    /// </summary>
    public class RoleClaimEventHandler :
        INotificationHandler<RoleClaimAddedEvent>,
        INotificationHandler<RoleClaimUpdatedEvent>,
        INotificationHandler<RoleClaimDeletedEvent>
    {
        private readonly ILogger<RoleClaimEventHandler> _logger;
        private readonly IStringLocalizer<RoleClaimEventHandler> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="RoleClaimEventHandler"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public RoleClaimEventHandler(
            ILogger<RoleClaimEventHandler> logger,
            IStringLocalizer<RoleClaimEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(RoleClaimAddedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleClaimAddedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(RoleClaimUpdatedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleClaimUpdatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(RoleClaimDeletedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleClaimDeletedEvent)} Raised. {notification.Id} Deleted."]);
            return Task.CompletedTask;
        }
    }
}