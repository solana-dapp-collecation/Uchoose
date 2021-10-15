// ------------------------------------------------------------------------------------------------------
// <copyright file="DomainEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

using Uchoose.Domain.Contracts;
using Uchoose.Utils.Abstractions.Common;
using Uchoose.Utils.Attributes.Exporting;
using Uchoose.Utils.Attributes.Importing;
using Uchoose.Utils.Attributes.Searching;
using Uchoose.Utils.Contracts.Events;
using Uchoose.Utils.Enums;
using Uchoose.Utils.Extensions;

namespace Uchoose.Domain.Abstractions
{
    /// <inheritdoc cref="IDomainEvent"/>
    /// <typeparam name="TEntity">Тип сущности доменного события.</typeparam>
    // ReSharper disable once UnusedTypeParameter
    public abstract class DomainEvent<TEntity> :
        DomainEvent, IDomainEvent<TEntity>
            where TEntity : IDomainEntity
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DomainEvent"/>.
        /// </summary>
        /// <param name="aggregateId">Идентификатор агрегата.</param>
        /// <param name="eventDescription">Описание события.</param>
        /// <param name="aggregateVersion">Текущая версия агрегата.</param>
        /// <param name="relatedEntities">Связанные с событием сущности.</param>
        /// <remarks>
        /// Свойство Timestamp инициализируется текущей датой и временем.
        /// </remarks>
        protected DomainEvent(Guid aggregateId, string eventDescription, int? aggregateVersion, params Type[] relatedEntities)
            : base(aggregateId, eventDescription, aggregateVersion, relatedEntities)
        {
        }
    }

    /// <inheritdoc cref="IDomainEvent"/>
    public abstract class DomainEvent :
        Message,
        IDomainEvent
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DomainEvent"/>.
        /// </summary>
        /// <param name="aggregateId">Идентификатор агрегата.</param>
        /// <param name="eventDescription">Описание события.</param>
        /// <param name="aggregateVersion">Текущая версия агрегата.</param>
        /// <param name="relatedEntities">Связанные с событием сущности.</param>
        /// <remarks>
        /// Свойство Timestamp инициализируется текущей датой и временем.
        /// </remarks>
        protected DomainEvent(Guid aggregateId, string eventDescription, int? aggregateVersion, params Type[] relatedEntities)
            : base(aggregateId)
        {
            EventDescription = eventDescription;
            AggregateVersion = aggregateVersion;
            Timestamp = DateTime.UtcNow;
            EventType = EventType.Domain;
            RelatedEntities = relatedEntities;

            // ReSharper disable once VirtualMemberCallInConstructor
            SetMessageType();
        }

        /// <inheritdoc cref="IDomainEvent.RelatedEntities"/>
        [NotMapped]
        [JsonIgnore] // для System.Text.Json
        [NotExportable]
        [NotImportable]
        [NotSearchable]
        public IEnumerable<Type> RelatedEntities { get; private set; }

        /// <inheritdoc cref="IDomainEvent.AggregateVersion"/>
        [JsonInclude]
        public int? AggregateVersion { get; private set; }

        /// <inheritdoc cref="IEvent.EventDescription"/>
        [Display(Name = "Event description")]
        [JsonInclude]
        public string EventDescription { get; private set; }

        /// <summary>
        /// Дата возникновения события.
        /// </summary>
        [Display(Name = "Date and time of occurrence of the event")]
        [JsonInclude]
        public DateTime Timestamp { get; private set; }

        /// <inheritdoc cref="IEvent.EventType"/>
        [Display(Name = "The event type")]
        [JsonInclude]
        public EventType EventType { get; private set; }

        /// <inheritdoc/>
        protected override void SetMessageType(string messageType = null)
        {
            base.SetMessageType(messageType ?? GetType().GetGenericTypeName());
        }
    }
}