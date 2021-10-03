// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeUpdatedEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Text.Json.Serialization;

using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Contracts;
using Uchoose.Domain.Enums;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.Domain.Events.ExtendedAttributes
{
    /// <summary>
    /// Событие обновления расширенного атрибута сущности.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public class ExtendedAttributeUpdatedEvent<TEntityId, TEntity> :
        DomainEvent,
        IHasIsActive
            where TEntity : class, IEntity<TEntityId>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="ExtendedAttributeUpdatedEvent{TEntityId, TEntity}"/>.
        /// </summary>
        /// <param name="extendedAttribute">Расширенный атрибут сущности.</param>
        /// <param name="eventDescription">Описание события.</param>
        /// <param name="messageType">Тип сообщения.</param>
        public ExtendedAttributeUpdatedEvent(
            IExtendedAttribute<TEntityId> extendedAttribute,
            string eventDescription,
            string? messageType = null)
            : base(
                extendedAttribute.Id,
                eventDescription,
                null,
                typeof(TEntity),
                extendedAttribute.GetType())
        {
            Id = extendedAttribute.Id;
            EntityId = extendedAttribute.EntityId;
            Type = extendedAttribute.Type;
            Key = extendedAttribute.Key;
            Decimal = extendedAttribute.Decimal;
            Text = extendedAttribute.Text;
            DateTime = extendedAttribute.DateTime;
            Json = extendedAttribute.Json;
            Boolean = extendedAttribute.Boolean;
            Integer = extendedAttribute.Integer;
            ExternalId = extendedAttribute.ExternalId;
            Group = extendedAttribute.Group;
            Description = extendedAttribute.Description;
            IsActive = extendedAttribute.IsActive;

            // ReSharper disable once VirtualMemberCallInConstructor
            SetMessageType(messageType);
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public Guid Id { get; private set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.EntityId"/>
        [JsonInclude]
        public TEntityId EntityId { get; private set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Type"/>
        [JsonInclude]
        public ExtendedAttributeType Type { get; private set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Key"/>
        [JsonInclude]
        public string Key { get; private set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Decimal"/>
        [JsonInclude]
        public decimal? Decimal { get; private set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Text"/>
        [JsonInclude]
        public string? Text { get; private set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.DateTime"/>
        [JsonInclude]
        public DateTime? DateTime { get; private set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Json"/>
        [JsonInclude]
        public string? Json { get; private set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Boolean"/>
        [JsonInclude]
        public bool? Boolean { get; private set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Integer"/>
        [JsonInclude]
        public int? Integer { get; private set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.ExternalId"/>
        [JsonInclude]
        public string? ExternalId { get; private set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Group"/>
        [JsonInclude]
        public string? Group { get; private set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Description"/>
        [JsonInclude]
        public string? Description { get; private set; }

        /// <inheritdoc cref="IHasIsActive{TProperty}.IsActive"/>
        [JsonInclude]
        public bool IsActive { get; private set; }
    }
}