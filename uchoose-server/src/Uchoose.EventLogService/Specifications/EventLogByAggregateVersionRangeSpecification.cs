// ------------------------------------------------------------------------------------------------------
// <copyright file="EventLogByAggregateVersionRangeSpecification.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Domain.Entities;
using Uchoose.EventLogService.Interfaces.Specifications.Base;
using Uchoose.Utils.Extensions;

namespace Uchoose.EventLogService.Specifications
{
    /// <summary>
    /// Спецификация по диапазону версий агрегата для <see cref="EventLog"/>.
    /// </summary>
    internal sealed class EventLogByAggregateVersionRangeSpecification : EventLogSpecification
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="EventLogByAggregateVersionRangeSpecification"/>.
        /// </summary>
        /// <param name="startAggregateVersionRange">Начало диапазона номера версии агрегата.</param>
        /// <param name="endAggregateVersionRange">Конец диапазона номера версии агрегата.</param>
        public EventLogByAggregateVersionRangeSpecification(int? startAggregateVersionRange, int? endAggregateVersionRange)
        {
            if (startAggregateVersionRange != null && endAggregateVersionRange != null)
            {
                Criteria = x => x.AggregateVersion >= startAggregateVersionRange && x.AggregateVersion <= endAggregateVersionRange;
            }
            else if (startAggregateVersionRange != null)
            {
                Criteria = x => x.AggregateVersion >= startAggregateVersionRange;
            }
            else if (endAggregateVersionRange != null)
            {
                Criteria = x => x.AggregateVersion <= endAggregateVersionRange;
            }
            else
            {
                Criteria = ExpressionExtensions.True<EventLog>();
            }
        }
    }
}