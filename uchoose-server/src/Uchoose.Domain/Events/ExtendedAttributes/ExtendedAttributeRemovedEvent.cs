// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeRemovedEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Linq;
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
        /// <param name="relatedEntities">Связанные с событием сущности.</param>
        public ExtendedAttributeRemovedEvent(
            Guid id,
            string eventDescription,
            string? messageType = null,
            params Type[] relatedEntities)
            : base(
                id,
                eventDescription,
                null,
                relatedEntities.Union(new[] { typeof(TEntity) }).ToArray())
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