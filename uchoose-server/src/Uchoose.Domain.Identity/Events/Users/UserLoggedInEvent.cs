// ------------------------------------------------------------------------------------------------------
// <copyright file="UserLoggedInEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Text.Json.Serialization;

using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Identity.Entities;

namespace Uchoose.Domain.Identity.Events.Users
{
    /// <summary>
    /// Событие входа пользователя.
    /// </summary>
    public class UserLoggedInEvent :
        DomainEvent<UchooseUser>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="UserLoggedInEvent"/>.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventDescription">Описание события.</param>
        public UserLoggedInEvent(Guid userId, string eventDescription)
            : base(
                userId,
                eventDescription, // string.Format(localizer["User '{0}' logged in."], userId),
                null,
                typeof(UchooseUser))
        {
            UserId = userId;
            Timestamp = DateTime.UtcNow;
        }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        [JsonInclude]
        public Guid UserId { get; private set; }

        /// <inheritdoc cref="DomainEvent.Timestamp"/>
        [JsonInclude]
        public new DateTime Timestamp { get; private set; }
    }
}