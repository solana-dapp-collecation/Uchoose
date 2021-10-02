﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeRemovedEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Text.Json.Serialization;

using Uchoose.Domain.Abstractions;

namespace Uchoose.Domain.Events.ExtendedAttributes
{
    /// <summary>
    /// Событие удаления расширенного атрибута сущности.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public class ExtendedAttributeRemovedEvent<TEntity> : DomainEvent
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="ExtendedAttributeRemovedEvent{TEntity}"/>.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <param name="eventDescription">Описание события.</param>
        /// <param name="messageType">Тип сообщения.</param>
        public ExtendedAttributeRemovedEvent(
            Guid id,
            string eventDescription,
            string? messageType = null)
            : base(
                id,
                eventDescription,
                null,
                typeof(TEntity))
        {
            Id = id;

            // ReSharper disable once VirtualMemberCallInConstructor
            SetMessageType(messageType);
        }

        /// <summary>
        /// Идентификатор расширенного атрибута.
        /// </summary>
        [JsonInclude]
        public Guid Id { get; private set; }
    }
}