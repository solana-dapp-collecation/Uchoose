// ------------------------------------------------------------------------------------------------------
// <copyright file="EventLog.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;

using Microsoft.Extensions.Localization;
using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Contracts;
using Uchoose.Utils.Attributes.Importing;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Events;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Searching;

namespace Uchoose.Domain.Entities
{
    /// <summary>
    /// Лог доменного события.
    /// </summary>
    [NotImportable]
    public class EventLog :
        DomainEvent,
        IEntity<Guid>,
        IExportable<Guid, EventLog>,
        ISearchable<Guid, EventLog>
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EventLog"/>.
        /// </summary>
        /// <remarks>
        /// Свойство Id инициализируется новым значением GUID.
        /// </remarks>
        /// <param name="event">Событие.</param>
        /// <param name="data">Данные лога события.</param>
        /// <param name="changes">Изменения.</param>
        /// <param name="email">Email пользователя, который вызвал событие.</param>
        /// <param name="userId">Идентификатор пользователя, который вызвал событие.</param>
        public EventLog(IEvent @event, string data, (string oldValues, string newValues) changes, string email, Guid userId)
            : base(@event.AggregateId, @event.EventDescription, (@event as IDomainEvent)?.AggregateVersion)
        {
            Id = Guid.NewGuid();
            Data = data;
            Email = email;
            (string oldValues, string newValues) = changes;
            OldValues = oldValues;
            NewValues = newValues;
            UserId = userId;

            // ReSharper disable once VirtualMemberCallInConstructor
            SetMessageType(@event.MessageType);
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DomainEvent"/>.
        /// </summary>
        public EventLog()
            : base(Guid.Empty, null, null)
        {
        }

        /// <summary>
        /// Идентификатор записи в БД.
        /// </summary>
        [Display(Name = "Database entry identifier")]
        [JsonInclude]
        public Guid Id { get; private set; }

        /// <summary>
        /// Данные лога события.
        /// </summary>
        /// <remarks>
        /// Обычно хранится в виде сериализованной json строки.
        /// </remarks>
        [Display(Name = "Event log data")]
        [JsonInclude]
        public string Data { get; private set; }

        /// <summary>
        /// Старые значения.
        /// </summary>
        [Display(Name = "Old Values")]
        [JsonInclude]
        public string OldValues { get; private set; }

        /// <summary>
        /// Новые значения.
        /// </summary>
        [Display(Name = "New Values")]
        [JsonInclude]
        public string NewValues { get; private set; }

        /// <summary>
        /// Email пользователя, который вызвал событие.
        /// </summary>
        [Display(Name = "Email of the user who triggered the event")]
        [JsonInclude]
        public string Email { get; private set; }

        /// <summary>
        /// Идентификатор пользователя, который вызвал событие.
        /// </summary>
        [Display(Name = "The identifier of the user who triggered the event")]
        [JsonInclude]
        public Guid UserId { get; private set; }

        /// <inheritdoc/>
        public Dictionary<string, Func<EventLog, (object Object, int Order)>> GetDefaultExportMappers(IStringLocalizer localizer)
        {
            return new()
            {
                { localizer["Id"], item => (item.Id, 1) },
                { localizer["Data"], item => (item.Data, 2) },
                { localizer["Date Time (Local)"], item => (DateTime.SpecifyKind(item.Timestamp, DateTimeKind.Utc).ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture), 3) },
                { localizer["Date Time (UTC)"], item => (item.Timestamp.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture), 4) },
                { localizer["Email"], item => (item.Email, 5) },
                { localizer["Old Values"], item => (item.OldValues, 6) },
                { localizer["New Values"], item => (item.NewValues, 7) },
                { localizer["User Id"], item => (item.UserId, 8) }
            };
        }
    }
}