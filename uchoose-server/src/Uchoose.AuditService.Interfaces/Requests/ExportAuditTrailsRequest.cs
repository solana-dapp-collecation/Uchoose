// ------------------------------------------------------------------------------------------------------
// <copyright file="ExportAuditTrailsRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Collections.Generic;

using AutoMapper;
using Uchoose.AuditService.Interfaces.Filters;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Ordering;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Mappings.Converters;

namespace Uchoose.AuditService.Interfaces.Requests
{
    /// <summary>
    /// Запрос для экспорта данных аудита.
    /// </summary>
    public class ExportAuditTrailsRequest :
        IMapFromTo<AuditTrailsExportPaginationFilter, ExportAuditTrailsRequest>,
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

        /// <summary>
        /// Наименование схемы БД.
        /// </summary>
        public string SchemaName { get; private set; } = string.Empty;

        /// <summary>
        /// Установить наименование схемы БД.
        /// </summary>
        /// <param name="schemaName">Наименование схемы БД.</param>
        public void SetSchemaName(string schemaName)
        {
            SchemaName = schemaName;
        }

        void IMapFromTo<AuditTrailsExportPaginationFilter, ExportAuditTrailsRequest>.Mapping(Profile profile, bool useReverseMap)
        {
            profile.CreateMap<AuditTrailsExportPaginationFilter, ExportAuditTrailsRequest>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}