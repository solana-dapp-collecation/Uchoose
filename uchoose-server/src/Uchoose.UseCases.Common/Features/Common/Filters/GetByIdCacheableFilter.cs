// ------------------------------------------------------------------------------------------------------
// <copyright file="GetByIdCacheableFilter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Uchoose.Utils.Attributes.Swagger;
using Uchoose.Utils.Constants.Caching;
using Uchoose.Utils.Contracts.Caching;
using Uchoose.Utils.Contracts.Common;

namespace Uchoose.UseCases.Common.Features.Common.Filters
{
    /// <summary>
    /// Фильтр для получения сущности по её идентификатору с кэшированием.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public class GetByIdCacheableFilter<TEntityId, TEntity> :
        ICacheableQuery
            where TEntity : class, IEntity<TEntityId>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="GetByIdCacheableFilter{TEntityId,TEntity}"/>.
        /// </summary>
        public GetByIdCacheableFilter()
        {
            // необходим для конструирования из query действия контроллера,
            // в котором фильтр выступает в качестве запроса
        }

        /// <summary>
        /// Инициализирует экземпляр <see cref="GetByIdCacheableFilter{TEntityId,TEntity}"/>.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <param name="bypassCache">Не использовать кэш.</param>
        /// <param name="slidingExpiration">Скользящий срок действия кэша в часах.</param>
        public GetByIdCacheableFilter(TEntityId id, bool bypassCache, TimeSpan? slidingExpiration)
        {
            Id = id;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        [FromRoute(Name = "id")]
        public TEntityId Id { get; set; }

        /// <inheritdoc/>
        [FromQuery]
        public bool BypassCache { get; set; }

        /// <inheritdoc/>
        [FromQuery]
        [SwaggerExclude]
        public string CacheKey => CacheKeys.Common.GetEntityByIdCacheKey<TEntityId, TEntity>(Id);

        /// <inheritdoc/>
        [FromQuery]
        public bool RefreshCachedEntry { get; set; }

        /// <inheritdoc/>
        [FromQuery]
        public bool ReplaceCachedEntry { get; set; }

        /// <inheritdoc/>
        [FromQuery]
        [RegularExpression(@"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$")]
        public TimeSpan? SlidingExpiration { get; set; }
    }
}