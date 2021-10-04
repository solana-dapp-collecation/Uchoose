// ------------------------------------------------------------------------------------------------------
// <copyright file="GetNftImageLayersQuery.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using AutoMapper;
using MediatR;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries.Filters;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries.Responses;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Logging;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Ordering;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Filters;
using Uchoose.Utils.Mappings.Converters;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries
{
    /// <summary>
    /// Запрос на получение слоёв изображений NFT с пагинацеией.
    /// </summary>
    public class GetNftImageLayersQuery :
        IRequest<PaginatedResult<NftImageLayerResponse>>,
        IMapFromTo<NftImageLayerPaginationFilter, GetNftImageLayersQuery>,
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
        void IMapFromTo<NftImageLayerPaginationFilter, GetNftImageLayersQuery>.Mapping(Profile profile, bool useReverseMap)
        {
            profile.CreateMap<NftImageLayerPaginationFilter, GetNftImageLayersQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}