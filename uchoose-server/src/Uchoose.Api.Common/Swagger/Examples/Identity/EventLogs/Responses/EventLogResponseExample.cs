// ------------------------------------------------------------------------------------------------------
// <copyright file="EventLogResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
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
    /// Пример ответа для <see cref="EventLogsController.GetByIdAsync"/>.
    /// </summary>
    public class EventLogResponseExample :
        IExamplesProvider<object>
    {
        private readonly IStringLocalizer<EventLogResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="EventLogResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public EventLogResponseExample(
            IStringLocalizer<EventLogResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<EventLogResponse>.Success(
                new()
                {
                    Data = _localizer["<Event log data>"],
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
                _localizer["<Message>"]);
        }
    }
}