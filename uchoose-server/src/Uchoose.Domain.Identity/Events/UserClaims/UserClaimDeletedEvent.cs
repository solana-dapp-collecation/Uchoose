// ------------------------------------------------------------------------------------------------------
// <copyright file="UserClaimDeletedEvent.cs" company="Life Loop">
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

namespace Uchoose.Domain.Identity.Events.UserClaims
{
    /// <summary>
    /// Событие удаления разрешения пользователя.
    /// </summary>
    public class UserClaimDeletedEvent :
        DomainEvent<UchooseUserClaim>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="UserClaimDeletedEvent"/>.
        /// </summary>
        /// <param name="id">Идентификатор разрешения пользователя.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="eventDescription">Описание события.</param>
        public UserClaimDeletedEvent(int id, Guid userId, string eventDescription)
            : base(
                userId,
                eventDescription,
                null,
                typeof(UchooseUserClaim))
        {
            Id = id;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public int Id { get; private set; }
    }
}