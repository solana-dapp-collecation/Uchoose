// ------------------------------------------------------------------------------------------------------
// <copyright file="EventLogsResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Identity;
using Uchoose.EventLogService.Interfaces.Responses;
using Uchoose.Utils.Enums;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Identity.EventLogs.Responses
{
    /// <summary>
    /// Пример ответа для <see cref="EventLogsController.GetAllAsync"/>.
    /// </summary>
    public class EventLogsResponseExample :
        IExamplesProvider<object>
    {
        private readonly IStringLocalizer<EventLogsResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="EventLogsResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public EventLogsResponseExample(
            IStringLocalizer<EventLogsResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return PaginatedResult<EventLogResponse>.Success(
                new()
                {
                    new()
                    {
                        Data = _localizer["<Event log 1 data>"],
                        OldValues = _localizer["<Serialized old values>"],
                        NewValues = _localizer["<Serialized new values>"],
                        Email = _localizer["<User email>"],
                        UserId = Guid.Empty,
                        Id = Guid.Empty,
                        AggregateId = Guid.Empty,
                        EventType = EventType.Domain,
                        AggregateVersion = 0,
                        Timestamp = DateTime.MinValue,
                        MessageType = _localizer["<Event type name>"],
                        EventDescription = _localizer["<Event description>"]
                    },
                    new()
                    {
                        Data = _localizer["<Event log 2 data>"],
                        OldValues = _localizer["<Serialized old values>"],
                        NewValues = _localizer["<Serialized new values>"],
                        Email = _localizer["<User email>"],
                        UserId = Guid.Empty,
                        Id = Guid.Empty,
                        AggregateId = Guid.Empty,
                        EventType = EventType.Domain,
                        AggregateVersion = 0,
                        Timestamp = DateTime.MinValue,
                        MessageType = _localizer["<Event type name>"],
                        EventDescription = _localizer["<Event description>"]
                    }
                },
                2,
                1,
                10);
        }
    }
}