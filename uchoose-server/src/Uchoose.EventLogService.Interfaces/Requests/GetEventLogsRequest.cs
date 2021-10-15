// ------------------------------------------------------------------------------------------------------
// <copyright file="GetEventLogsRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;

using AutoMapper;
using Uchoose.EventLogService.Interfaces.Filters;
using Uchoose.Utils.Abstractions.Common;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Ordering;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Filters;
using Uchoose.Utils.Mappings.Converters;

namespace Uchoose.EventLogService.Interfaces.Requests
{
    /// <summary>
    /// Запрос для получения логов событий с пагинацией.
    /// </summary>
    public class GetEventLogsRequest :
        IMapFromTo<EventLogsPaginationFilter, GetEventLogsRequest>,
        IMapFromTo<ExportEventLogsRequest, GetEventLogsRequest>,
        IPaginated,
        ISearchableRequest,
        IHasDateRange,
        IOrderableRequest
    {
        /// <inheritdoc cref="IPaginated.PageNumber"/>
        public int PageNumber { get; private set; }

        /// <inheritdoc cref="IPaginated.PageSize"/>
        public int PageSize { get; private set; }

        /// <inheritdoc cref="SearchFilter"/>
        public SearchFilter? Search { get; private set; }

        /// <inheritdoc/>
        public string[]? OrderBy { get; private set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string? Email { get; private set; }

        /// <inheritdoc cref="Message.MessageType"/>
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
        /// Начало диапазона даты возникновения события.
        /// </summary>
        public DateTime? StartDateRange { get; private set; }

        /// <summary>
        /// Конец диапазона даты возникновения события.
        /// </summary>
        public DateTime? EndDateRange { get; private set; }

        /// <inheritdoc/>
        void IMapFromTo<EventLogsPaginationFilter, GetEventLogsRequest>.Mapping(Profile profile, bool useReverseMap)
        {
            profile.CreateMap<EventLogsPaginationFilter, GetEventLogsRequest>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}