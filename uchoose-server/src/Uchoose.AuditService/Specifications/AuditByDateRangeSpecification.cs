// ------------------------------------------------------------------------------------------------------
// <copyright file="AuditByDateRangeSpecification.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.AuditService.Interfaces.Specifications.Base;
using Uchoose.Domain.Entities;
using Uchoose.Utils.Extensions;

namespace Uchoose.AuditService.Specifications
{
    /// <summary>
    /// Спецификация по диапазону дат для <see cref="Audit"/>.
    /// </summary>
    internal sealed class AuditByDateRangeSpecification :
        AuditSpecification
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="AuditByDateRangeSpecification"/>.
        /// </summary>
        /// <param name="startDateRange">Начало диапазона даты записи данных аудита.</param>
        /// <param name="endDateRange">Конец диапазона даты записи данных аудита.</param>
        public AuditByDateRangeSpecification(DateTime? startDateRange, DateTime? endDateRange)
        {
            if (startDateRange != null && endDateRange != null)
            {
                Criteria = x => x.DateTime >= startDateRange && x.DateTime <= endDateRange;
            }
            else if (startDateRange != null)
            {
                Criteria = x => x.DateTime >= startDateRange;
            }
            else if (endDateRange != null)
            {
                Criteria = x => x.DateTime <= endDateRange;
            }
            else
            {
                Criteria = ExpressionExtensions.True<Audit>();
            }
        }
    }
}