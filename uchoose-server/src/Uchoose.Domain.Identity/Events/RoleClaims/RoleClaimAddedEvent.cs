// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimAddedEvent.cs" company="Life Loop">
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

namespace Uchoose.Domain.Identity.Events.RoleClaims
{
    /// <summary>
    /// Событие добавления разрешения роли пользователя.
    /// </summary>
    public class RoleClaimAddedEvent :
        DomainEvent<UchooseRoleClaim>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="RoleClaimAddedEvent"/>.
        /// </summary>
        /// <param name="roleClaim">Разрешение роли пользователя.</param>
        /// <param name="eventDescription">Описание события.</param>
        public RoleClaimAddedEvent(UchooseRoleClaim roleClaim, string eventDescription)
            : base(
                roleClaim.RoleId,
                eventDescription,
                null,
                typeof(UchooseRoleClaim))
        {
            RoleId = roleClaim.RoleId;
            Group = roleClaim.Group;
            ClaimType = roleClaim.ClaimType;
            ClaimValue = roleClaim.ClaimValue;
            Description = roleClaim.Description;
            Id = roleClaim.Id;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public int Id { get; private set; }

        /// <inheritdoc cref="IdentityRoleClaim{TKey}.RoleId"/>
        [JsonInclude]
        public Guid RoleId { get; private set; }

        /// <inheritdoc cref="IdentityRoleClaim{TKey}.ClaimType"/>
        [JsonInclude]
        public string ClaimType { get; private set; }

        /// <inheritdoc cref="IdentityRoleClaim{TKey}.ClaimValue"/>
        [JsonInclude]
        public string ClaimValue { get; private set; }

        /// <inheritdoc cref="UchooseRoleClaim.Group"/>
        [JsonInclude]
        public string Group { get; private set; }

        /// <inheritdoc cref="UchooseRoleClaim.Description"/>
        [JsonInclude]
        public string Description { get; private set; }
    }
}