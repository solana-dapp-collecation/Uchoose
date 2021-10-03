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
using Uchoose.Domain.Entities;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Identity.EventLogs.Responses
{
    /// <summary>
    /// Пример ответа для <see cref="EventLogsController.GetByIdAsync"/>.
    /// </summary>
    public class EventLogResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<EventLogResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="EventLogResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public EventLogResponseExample(IStringLocalizer<EventLogResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<EventLog>.Success(
                new(new EventLog(), _localizer["<Event log data>"], (_localizer["<Serialized old values>"], _localizer["<Serialized new values>"]), _localizer["<User email>"], Guid.Empty),
                _localizer["<Message>"]);
        }
    }
}