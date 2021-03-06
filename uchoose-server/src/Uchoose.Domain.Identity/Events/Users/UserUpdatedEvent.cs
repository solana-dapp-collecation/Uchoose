// ------------------------------------------------------------------------------------------------------
// <copyright file="UserUpdatedEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Identity;
using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Identity.Entities;
using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Domain.Identity.Events.Users
{
    /// <summary>
    /// Событие обновления пользователя.
    /// </summary>
    public class UserUpdatedEvent :
        DomainEvent<UchooseUser>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="UserUpdatedEvent"/>.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="eventDescription">Описание события.</param>
        public UserUpdatedEvent(UchooseUser user, string eventDescription)
            : base(
                user.Id,
                eventDescription, // string.Format(localizer["User '{0}' updated."], user.UserName),
                null,
                typeof(UchooseUser))
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
            Email = user.Email;
            UserName = user.UserName;
            PhoneNumber = user.PhoneNumber;
            Id = user.Id;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public Guid Id { get; private set; }

        /// <inheritdoc cref="UchooseUser.FirstName"/>
        [JsonInclude]
        public string FirstName { get; private set; }

        /// <inheritdoc cref="UchooseUser.LastName"/>
        [JsonInclude]
        public string LastName { get; private set; }

        /// <inheritdoc cref="UchooseUser.MiddleName"/>
        [JsonInclude]
        public string MiddleName { get; private set; }

        /// <inheritdoc cref="IdentityUser{TKey}.Email"/>
        [JsonInclude]
        public string Email { get; private set; }

        /// <inheritdoc cref="IdentityUser{TKey}.UserName"/>
        [JsonInclude]
        public string UserName { get; private set; }

        /// <inheritdoc cref="IdentityUser{TKey}.PhoneNumber"/>
        [JsonInclude]
        public string PhoneNumber { get; private set; }
    }
}