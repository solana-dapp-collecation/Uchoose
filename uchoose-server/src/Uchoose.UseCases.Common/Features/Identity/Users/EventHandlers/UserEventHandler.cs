// ------------------------------------------------------------------------------------------------------
// <copyright file="UserEventHandler.cs" company="Life Loop">
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
using Uchoose.Domain.Identity.Events.Users;

namespace Uchoose.UseCases.Common.Features.Identity.Users.EventHandlers
{
    /// <summary>
    /// Обработчик событий пользователя.
    /// </summary>
    public class UserEventHandler :
        INotificationHandler<UserRegisteredEvent>,
        INotificationHandler<UserUpdatedEvent>,
        INotificationHandler<UserDeletedEvent>,
        INotificationHandler<UserLoggedInEvent>
    {
        private readonly ILogger<UserEventHandler> _logger;
        private readonly IStringLocalizer<UserEventHandler> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserEventHandler"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public UserEventHandler(
            ILogger<UserEventHandler> logger,
            IStringLocalizer<UserEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(UserRegisteredEvent)} Raised."]);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(UserUpdatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(UserDeletedEvent)} Raised. {notification.Id} Deleted."]);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(UserLoggedInEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(UserLoggedInEvent)} Raised. UserId: {notification.UserId}"]);
            return Task.CompletedTask;
        }
    }
}