// ------------------------------------------------------------------------------------------------------
// <copyright file="EventLogsResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Identity;
using Uchoose.Domain.Entities;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Identity.EventLogs.Responses
{
    /// <summary>
    /// Пример ответа для <see cref="EventLogsController.GetAllAsync"/>.
    /// </summary>
    public class EventLogsResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<EventLogsResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="EventLogsResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public EventLogsResponseExample(IStringLocalizer<EventLogsResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<List<EventLog>>.Success(
                new()
                {
                    new(new EventLog(), _localizer["<Event log 1 data>"], (_localizer["<Serialized old values>"], _localizer["<Serialized new values>"]), _localizer["<User email>"], Guid.Empty),
                    new(new EventLog(), _localizer["<Event log 2 data>"], (_localizer["<Serialized old values>"], _localizer["<Serialized new values>"]), _localizer["<User email>"], Guid.Empty)
                },
                _localizer["<Message>"]);
        }
    }
}