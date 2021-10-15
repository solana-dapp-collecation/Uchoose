// ------------------------------------------------------------------------------------------------------
// <copyright file="ExportNftImageLayerTypesQuery.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Collections.Generic;

using AutoMapper;
using MediatR;
using Uchoose.ExcelService.Interfaces.Requests;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries.Filters;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Logging;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Ordering;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Filters;
using Uchoose.Utils.Mappings.Converters;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries
{
    /// <summary>
    /// Запрос на экспорт типов слоёв изображений NFT с пагинацеией.
    /// </summary>
    public class ExportNftImageLayerTypesQuery :
        IRequest<Result<string>>,
        IMapFromTo<NftImageLayerTypesExportPaginationFilter, ExportNftImageLayerTypesQuery>,
        IMapFromTo<ExportRequest<Guid, Domain.Marketplace.Entities.NftImageLayerType>, ExportNftImageLayerTypesQuery>,
        IPaginated,
        ILoggable,
        ISearchableRequest,
        IHasIsActive<bool?>,
        IHasIsReadOnly<bool?>,
        IOrderableRequest,
        IExportableRequest,
        IHasExportableProperties
    {
        /// <inheritdoc cref="IPaginated.PageNumber"/>
        public int PageNumber { get; private set; }

        /// <inheritdoc cref="IPaginated.PageSize"/>
        public int PageSize { get; private set; }

        /// <inheritdoc cref="SearchFilter"/>
        public SearchFilter? Search { get; private set; }

        /// <inheritdoc/>
        public string[]? OrderBy { get; private set; }

        /// <inheritdoc/>
        public bool? IsReadOnly { get; private set; }

        /// <inheritdoc/>
        public bool? IsActive { get; private set; }

        /// <inheritdoc/>
        public int TitlesRowNumber { get; private set; }

        /// <inheritdoc/>
        public int TitlesFirstColNumber { get; private set; }

        /// <inheritdoc/>
        public int DataFirstRowNumber { get; private set; }

        /// <inheritdoc/>
        public List<string> Properties { get; private set; } = new();

        /// <inheritdoc/>
        void IMapFromTo<NftImageLayerTypesExportPaginationFilter, ExportNftImageLayerTypesQuery>.Mapping(Profile profile, bool useReverseMap)
        {
            profile.CreateMap<NftImageLayerTypesExportPaginationFilter, ExportNftImageLayerTypesQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }

        /// <inheritdoc/>
        void IMapFromTo<ExportRequest<Guid, Domain.Marketplace.Entities.NftImageLayerType>, ExportNftImageLayerTypesQuery>.Mapping(Profile profile, bool useReverseMap)
        {
            // меняем порядок сопоставления
            profile.CreateMap<ExportNftImageLayerTypesQuery, ExportRequest<Guid, Domain.Marketplace.Entities.NftImageLayerType>>();
        }
    }
}