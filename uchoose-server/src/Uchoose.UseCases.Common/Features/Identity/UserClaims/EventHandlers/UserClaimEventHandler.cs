// ------------------------------------------------------------------------------------------------------
// <copyright file="UserClaimEventHandler.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Uchoose.Domain.Identity.Events.UserClaims;

namespace Uchoose.UseCases.Common.Features.Identity.UserClaims.EventHandlers
{
    /// <summary>
    /// Обработчик событий разрешений пользователя.
    /// </summary>
    public class UserClaimEventHandler :
        INotificationHandler<UserClaimAddedEvent>,
        INotificationHandler<UserClaimUpdatedEvent>,
        INotificationHandler<UserClaimDeletedEvent>
    {
        private readonly ILogger<UserClaimEventHandler> _logger;
        private readonly IStringLocalizer<UserClaimEventHandler> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserClaimEventHandler"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public UserClaimEventHandler(
            ILogger<UserClaimEventHandler> logger,
            IStringLocalizer<UserClaimEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(UserClaimAddedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(UserClaimAddedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(UserClaimUpdatedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(UserClaimUpdatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(UserClaimDeletedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(UserClaimDeletedEvent)} Raised. {notification.Id} Deleted."]);
            return Task.CompletedTask;
        }
    }
}