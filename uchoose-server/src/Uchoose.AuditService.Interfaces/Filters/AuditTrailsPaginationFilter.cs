// ------------------------------------------------------------------------------------------------------
// <copyright file="AuditTrailsPaginationFilter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;

using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Filters;

namespace Uchoose.AuditService.Interfaces.Filters
{
    /// <summary>
    /// Фильтр для получения данных аудита с пагинацией.
    /// </summary>
    public class AuditTrailsPaginationFilter :
        PaginationFilter,
        IHasDateRange
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public Guid UserId { get; set; }

        /// <summary>
        /// Начало диапазона даты записи данных аудита.
        /// </summary>
        public DateTime? StartDateRange { get; set; }

        /// <summary>
        /// Конец диапазона даты записи данных аудита.
        /// </summary>
        public DateTime? EndDateRange { get; set; }
    }
}