// ------------------------------------------------------------------------------------------------------
// <copyright file="EventLogResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Domain.Entities;
using Uchoose.Utils.Attributes.Mappings;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Enums;

namespace Uchoose.EventLogService.Interfaces.Responses
{
    /// <summary>
    /// Ответ с логами доменного события.
    /// </summary>
    [UseReverseMap(typeof(EventLog))]
    public class EventLogResponse :
        IMapFromTo<EventLog, EventLogResponse>
    {
        /// <summary>
        /// Идентификатор записи в БД.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Данные лога события.
        /// </summary>
        /// <remarks>
        /// Обычно хранится в виде сериализованной json строки.
        /// </remarks>
        public string Data { get; set; }

        /// <summary>
        /// Старые значения.
        /// </summary>
        public string OldValues { get; set; }

        /// <summary>
        /// Новые значения.
        /// </summary>
        public string NewValues { get; set; }

        /// <summary>
        /// Email пользователя, который вызвал событие.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Идентификатор пользователя, который вызвал событие.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Текущая версия агрегата.
        /// </summary>
        /// <remarks>
        /// Присваивается в случае, когда доменное событие было вызвано агрегатом.
        /// </remarks>
        public int? AggregateVersion { get; set; }

        /// <summary>
        /// Описание события.
        /// </summary>
        public string EventDescription { get; set; }

        /// <summary>
        /// Дата возникновения события.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Тип события.
        /// </summary>
        public EventType EventType { get; set; }

        /// <summary>
        /// Тип сообщения.
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// Идентификатор агрегата.
        /// </summary>
        public Guid AggregateId { get; set; }
    }
}