// ------------------------------------------------------------------------------------------------------
// <copyright file="GetNftImageLayerTypesQuery.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using AutoMapper;
using MediatR;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries.Filters;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries.Responses;
using Uchoose.Utils.Contracts.Common;
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
    /// Запрос на получение типов слоёв изображений NFT с пагинацеией.
    /// </summary>
    public class GetNftImageLayerTypesQuery :
        IRequest<PaginatedResult<NftImageLayerTypeResponse>>,
        IMapFromTo<NftImageLayerTypePaginationFilter, GetNftImageLayerTypesQuery>,
        IPaginated,
        ILoggable,
        ISearchableRequest,
        IHasIsActive<bool?>,
        IHasIsReadOnly<bool?>,
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

        /// <inheritdoc/>
        public bool? IsReadOnly { get; private set; }

        /// <inheritdoc/>
        public bool? IsActive { get; private set; }

        /// <inheritdoc/>
        void IMapFromTo<NftImageLayerTypePaginationFilter, GetNftImageLayerTypesQuery>.Mapping(Profile profile, bool useReverseMap)
        {
            profile.CreateMap<NftImageLayerTypePaginationFilter, GetNftImageLayerTypesQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}