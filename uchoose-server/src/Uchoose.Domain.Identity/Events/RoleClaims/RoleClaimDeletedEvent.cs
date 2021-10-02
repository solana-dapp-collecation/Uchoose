// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimDeletedEvent.cs" company="Life Loop">
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

namespace Uchoose.Domain.Identity.Events.RoleClaims
{
    /// <summary>
    /// Событие удаления разрешения роли пользователя.
    /// </summary>
    public class RoleClaimDeletedEvent :
        DomainEvent<UchooseRoleClaim>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="RoleClaimDeletedEvent"/>.
        /// </summary>
        /// <param name="id">Идентификатор разрешения роли пользователя.</param>
        /// <param name="roleId">Идентификатор роли пользователя.</param>
        /// <param name="eventDescription">Описание события.</param>
        public RoleClaimDeletedEvent(int id, Guid roleId, string eventDescription)
            : base(
                roleId,
                eventDescription,
                null,
                typeof(UchooseRoleClaim))
        {
            Id = id;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public int Id { get; private set; }
    }
}