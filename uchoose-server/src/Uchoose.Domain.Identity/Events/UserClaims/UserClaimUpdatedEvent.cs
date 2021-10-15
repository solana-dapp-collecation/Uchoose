// ------------------------------------------------------------------------------------------------------
// <copyright file="UserClaimUpdatedEvent.cs" company="Life Loop">
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

namespace Uchoose.Domain.Identity.Events.UserClaims
{
    /// <summary>
    /// Событие обновления разрешения пользователя.
    /// </summary>
    public class UserClaimUpdatedEvent :
        DomainEvent<UchooseUserClaim>
    {
        /// <summary>
        /// инициализирует экземпляр <see cref="UserClaimUpdatedEvent"/>.
        /// </summary>
        /// <param name="userClaim">Разрешение пользователя.</param>
        /// <param name="eventDescription">Описание события.</param>
        public UserClaimUpdatedEvent(UchooseUserClaim userClaim, string eventDescription)
            : base(
                userClaim.UserId,
                eventDescription,
                null,
                typeof(UchooseUserClaim))
        {
            UserId = userClaim.UserId;
            Group = userClaim.Group;
            ClaimType = userClaim.ClaimType;
            ClaimValue = userClaim.ClaimValue;
            Description = userClaim.Description;
            Id = userClaim.Id;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public int Id { get; private set; }

        /// <inheritdoc cref="IdentityUserClaim{TKey}.UserId"/>
        [JsonInclude]
        public Guid UserId { get; private set; }

        /// <inheritdoc cref="IdentityUserClaim{TKey}.ClaimType"/>
        [JsonInclude]
        public string ClaimType { get; private set; }

        /// <inheritdoc cref="IdentityUserClaim{TKey}.ClaimValue"/>
        [JsonInclude]
        public string ClaimValue { get; private set; }

        /// <inheritdoc cref="UchooseUserClaim.Group"/>
        [JsonInclude]
        public string Group { get; private set; }

        /// <inheritdoc cref="UchooseUserClaim.Description"/>
        [JsonInclude]
        public string Description { get; private set; }
    }
}