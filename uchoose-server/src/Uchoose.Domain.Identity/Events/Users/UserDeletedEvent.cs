// ------------------------------------------------------------------------------------------------------
// <copyright file="UserDeletedEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Text.Json.Serialization;

using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Identity.Entities;
using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Domain.Identity.Events.Users
{
    /// <summary>
    /// Событие удаления пользователя.
    /// </summary>
    public class UserDeletedEvent :
        DomainEvent<UchooseUser>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="UserDeletedEvent"/>.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="eventDescription">Описание события.</param>
        public UserDeletedEvent(Guid id, string eventDescription)
            : base(
                id,
                eventDescription, // string.Format(localizer["User '{0}' deleted."], userName),
                null,
                typeof(UchooseUser))
        {
            Id = id;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public Guid Id { get; private set; }
    }
}