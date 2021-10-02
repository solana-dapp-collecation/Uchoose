// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleDeletedEvent.cs" company="Life Loop">
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

namespace Uchoose.Domain.Identity.Events.Roles
{
    /// <summary>
    /// Событие удаления роли пользователя.
    /// </summary>
    public class RoleDeletedEvent :
        DomainEvent<UchooseRole>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="RoleDeletedEvent"/>.
        /// </summary>
        /// <param name="id">Идентификатор роли пользователя.</param>
        /// <param name="eventDescription">Описание события.</param>
        public RoleDeletedEvent(Guid id, string eventDescription)
            : base(
                id,
                eventDescription,
                null,
                typeof(UchooseRole))
        {
            Id = id;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public Guid Id { get; private set; }
    }
}