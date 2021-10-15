// ------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

using Uchoose.Utils.Contracts.Events;
using Uchoose.Utils.Enums;
using Uchoose.Utils.Extensions;

namespace Uchoose.Utils.Abstractions.Common
{
    /// <inheritdoc cref="IApplicationEvent"/>
    public abstract class ApplicationEvent :
        Message,
        IApplicationEvent
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ApplicationEvent"/>.
        /// </summary>
        /// <remarks>
        /// Свойство Timestamp инициализируется текущей датой и временем.
        /// </remarks>
        /// <param name="aggregateId">Идентификатор агрегата.</param>
        /// <param name="eventDescription">Описание события.</param>
        protected ApplicationEvent(Guid aggregateId, string eventDescription = null)
            : base(aggregateId)
        {
            EventDescription = eventDescription;
            Timestamp = DateTime.UtcNow;
            EventType = EventType.Application;

            // ReSharper disable once VirtualMemberCallInConstructor
            SetMessageType();
        }

        /// <inheritdoc cref="IEvent.EventDescription"/>
        [Display(Name = "Event description")]
        public string EventDescription { get; private set; }

        /// <summary>
        /// Дата возникновения события.
        /// </summary>
        [Display(Name = "Date and time of occurrence of the event")]
        public DateTime Timestamp { get; private set; }

        /// <inheritdoc cref="IEvent.EventType"/>
        [Display(Name = "The event type")]
        public EventType EventType { get; private set; }

        /// <inheritdoc/>
        protected override void SetMessageType(string messageType = null)
        {
            base.SetMessageType(messageType ?? GetType().GetGenericTypeName());
        }
    }
}