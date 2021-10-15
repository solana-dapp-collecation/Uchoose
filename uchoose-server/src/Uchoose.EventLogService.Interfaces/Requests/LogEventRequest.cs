// ------------------------------------------------------------------------------------------------------
// <copyright file="LogEventRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using AutoMapper;
using Uchoose.Domain.Entities;
using Uchoose.Utils.Attributes.Mappings;
using Uchoose.Utils.Contracts.Mappings;

namespace Uchoose.EventLogService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на добавление пользовательского события в логи событий.
    /// </summary>
    [UseReverseMap(typeof(EventLog))]
    public class LogEventRequest :
        IMapFromTo<EventLog, LogEventRequest>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="LogEventRequest"/>.
        /// </summary>
        public LogEventRequest()
        {
        }

        /// <summary>
        /// Инициализирует экземпляр <see cref="LogEventRequest"/>.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, вызвавшего событие.</param>
        /// <param name="data">Данные события.</param>
        /// <param name="email">Email пользователя, вызвавшего событие.</param>
        /// <param name="eventDescription">Описание события.</param>
        public LogEventRequest(Guid userId, string data, string email, string eventDescription = null)
        {
            UserId = userId;
            Data = data;
            Email = email;
            EventDescription = eventDescription;
        }

        /// <summary>
        /// Идентификатор пользователя, вызвавшего событие.
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public Guid UserId { get; set; }

        /// <summary>
        /// Данные события.
        /// </summary>
        /// <example>Example</example>
        public string Data { get; set; }

        /// <summary>
        /// Email пользователя, вызвавшего событие.
        /// </summary>
        /// <example>example@example.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Описание события.
        /// </summary>
        /// <example>Example</example>
        public string EventDescription { get; set; }

        void IMapFromTo<EventLog, LogEventRequest>.Mapping(Profile profile, bool useReverseMap)
        {
            profile.CreateMap<LogEventRequest, EventLog>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.AggregateId, opt => opt.MapFrom(source => source.UserId));
        }
    }
}