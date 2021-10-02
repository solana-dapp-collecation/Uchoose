// ------------------------------------------------------------------------------------------------------
// <copyright file="GetAuditTrailsRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;

using AutoMapper;
using Uchoose.AuditService.Interfaces.Filters;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Ordering;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Filters;
using Uchoose.Utils.Mappings.Converters;

namespace Uchoose.AuditService.Interfaces.Requests
{
    /// <summary>
    /// Запрос для получения данных аудита.
    /// </summary>
    public class GetAuditTrailsRequest :
        IMapFromTo<AuditTrailsPaginationFilter, GetAuditTrailsRequest>,
        IMapFromTo<ExportAuditTrailsRequest, GetAuditTrailsRequest>,
        IHasDateRange,
        IPaginated,
        ISearchableRequest,
        IOrderableRequest
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <inheritdoc cref="SearchFilter"/>
        public SearchFilter? Search { get; private set; }

        /// <summary>
        /// Начало диапазона даты записи данных аудита.
        /// </summary>
        public DateTime? StartDateRange { get; private set; }

        /// <summary>
        /// Конец диапазона даты записи данных аудита.
        /// </summary>
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
        void IMapFromTo<AuditTrailsPaginationFilter, GetAuditTrailsRequest>.Mapping(Profile profile, bool useReverseMap)
        {
            profile.CreateMap<AuditTrailsPaginationFilter, GetAuditTrailsRequest>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}