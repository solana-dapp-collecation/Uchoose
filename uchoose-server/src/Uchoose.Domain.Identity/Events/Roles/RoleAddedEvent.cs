// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleAddedEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Identity;
using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Identity.Entities;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.Domain.Identity.Events.Roles
{
    /// <summary>
    /// Событие добавления роли пользователя.
    /// </summary>
    public class RoleAddedEvent :
        DomainEvent<UchooseRole>,
        IHasName
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="RoleAddedEvent"/>.
        /// </summary>
        /// <param name="role">Роль пользователя.</param>
        /// <param name="eventDescription">Описание события.</param>
        public RoleAddedEvent(UchooseRole role, string eventDescription)
            : base(
                role.Id,
                eventDescription,
                null,
                typeof(UchooseRole))
        {
            Name = role.Name;
            Description = role.Description;
            Id = role.Id;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public Guid Id { get; private set; }

        /// <inheritdoc cref="IdentityRole{TKey}.Name"/>
        [JsonInclude]
        public string Name { get; private set; }

        /// <inheritdoc cref="UchooseRole.Description"/>
        [JsonInclude]
        public string Description { get; private set; }
    }
}