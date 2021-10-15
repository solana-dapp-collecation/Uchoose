// ------------------------------------------------------------------------------------------------------
// <copyright file="ExportEventLogsRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Collections.Generic;

using AutoMapper;
using Uchoose.EventLogService.Interfaces.Filters;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Ordering;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Mappings.Converters;

namespace Uchoose.EventLogService.Interfaces.Requests
{
    /// <summary>
    /// Запрос для экспорта логов событий.
    /// </summary>
    public class ExportEventLogsRequest :
        IMapFromTo<EventLogsExportPaginationFilter, ExportEventLogsRequest>,
        IPaginated,
        IHasDateRange,
        IOrderableRequest,
        IExportableRequest,
        IHasExportableProperties
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string? Email { get; private set; }

        /// <summary>
        /// Тип сообщения.
        /// </summary>
        public string? MessageType { get; private set; }

        /// <summary>
        /// Начало диапазона номера версии агрегата.
        /// </summary>
        /// <example>-1</example>
        public int? StartAggregateVersionRange { get; set; }

        /// <summary>
        /// Конец диапазона номера версии агрегата.
        /// </summary>
        /// <example>100</example>
        public int? EndAggregateVersionRange { get; set; }

        /// <summary>
        /// Начало диапазона даты записи данных аудита.
        /// </summary>
        /// <example>2021-09-01T21:32:19.8396194Z</example>
        public DateTime? StartDateRange { get; private set; }

        /// <summary>
        /// Конец диапазона даты записи данных аудита.
        /// </summary>
        /// <example>2031-09-01T21:32:19.8396194Z</example>
        public DateTime? EndDateRange { get; private set; }

        /// <summary>
        /// Номер страницы.
        /// </summary>
        public int PageNumber { get; private set; }

        /// <summary>
        /// Размер страницы.
        /// </summary>
        public int PageSize { get; private set; }

        /// <inheritdoc/>
        public string[]? OrderBy { get; private set; }

        /// <inheritdoc/>
        public int TitlesRowNumber { get; private set; } = 1;

        /// <inheritdoc/>
        public int TitlesFirstColNumber { get; private set; } = 1;

        /// <inheritdoc/>
        public int DataFirstRowNumber { get; private set; } = 2;

        /// <inheritdoc/>
        public List<string> Properties { get; private set; } = new();

        void IMapFromTo<EventLogsExportPaginationFilter, ExportEventLogsRequest>.Mapping(Profile profile, bool useReverseMap)
        {
            profile.CreateMap<EventLogsExportPaginationFilter, ExportEventLogsRequest>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}