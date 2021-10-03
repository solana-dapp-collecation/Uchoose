// ------------------------------------------------------------------------------------------------------
// <copyright file="EventLogByDateRangeSpecification.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Domain.Entities;
using Uchoose.EventLogService.Interfaces.Specifications.Base;
using Uchoose.Utils.Extensions;

namespace Uchoose.EventLogService.Specifications
{
    /// <summary>
    /// Спецификация по диапазону дат для <see cref="EventLog"/>.
    /// </summary>
    internal sealed class EventLogByDateRangeSpecification :
        EventLogSpecification
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="EventLogByDateRangeSpecification"/>.
        /// </summary>
        /// <param name="startDateRange">Начало диапазона даты возникновения события.</param>
        /// <param name="endDateRange">Конец диапазона даты возникновения события.</param>
        public EventLogByDateRangeSpecification(DateTime? startDateRange, DateTime? endDateRange)
        {
            // TODO - вынести в общий метод расширения?

            if (startDateRange != null && endDateRange != null)
            {
                Criteria = x => x.Timestamp >= startDateRange && x.Timestamp <= endDateRange;
            }
            else if (startDateRange != null)
            {
                Criteria = x => x.Timestamp >= startDateRange;
            }
            else if (endDateRange != null)
            {
                Criteria = x => x.Timestamp <= endDateRange;
            }
            else
            {
                Criteria = ExpressionExtensions.True<EventLog>();
            }
        }
    }
}