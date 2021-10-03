// ------------------------------------------------------------------------------------------------------
// <copyright file="GetNftImageLayerTypeByIdQuery.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using MediatR;
using Uchoose.UseCases.Common.Features.Common.Filters;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries.Responses;
using Uchoose.Utils.Contracts.Caching;
using Uchoose.Utils.Contracts.Logging;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Performance;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries
{
    /// <summary>
    /// Запрос на получение типа слоя изображения NFT по его идентификатору.
    /// </summary>
    public class GetNftImageLayerTypeByIdQuery :
        IRequest<Result<NftImageLayerTypeResponse>>,
        IMapFromTo<GetByIdCacheableFilter<Guid, Domain.Marketplace.Entities.NftImageLayerType>, GetNftImageLayerTypeByIdQuery>,
        ICacheableQuery,
        IPerformanceMeasurableRequest,
        ILoggable
    {
        /// <summary>
        /// Идентификатор зоны доступа по аккредитации.
        /// </summary>
        public Guid Id { get; protected set; }

        /// <inheritdoc/>
        public bool BypassCache { get; protected set; }

        /// <inheritdoc/>
        public string CacheKey { get; protected set; }

        /// <inheritdoc/>
        public bool RefreshCachedEntry { get; protected set; }

        /// <inheritdoc/>
        public bool ReplaceCachedEntry { get; protected set; }

        /// <inheritdoc/>
        public TimeSpan? SlidingExpiration { get; protected set; }

        /// <inheritdoc/>
        public long ElapsedMillisecondsLimit => 400;

        /// <inheritdoc/>
        public bool LogIfLimitNotReached => false;
    }
}