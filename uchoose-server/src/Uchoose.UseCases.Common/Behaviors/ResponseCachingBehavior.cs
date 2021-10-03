// ------------------------------------------------------------------------------------------------------
// <copyright file="ResponseCachingBehavior.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Uchoose.SerializationService.Interfaces;
using Uchoose.UseCases.Common.Settings;
using Uchoose.Utils.Contracts.Caching;
using Uchoose.Utils.Exceptions;
using Uchoose.Utils.Extensions;

namespace Uchoose.UseCases.Common.Behaviors
{
    /// <summary>
    /// Посредник для кэширования ответов для запросов при вызове их обработчиков.
    /// </summary>
    /// <remarks>
    /// Для локализации.
    /// </remarks>
    public class ResponseCachingBehavior
    {
        // for localization
    }

    /// <summary>
    /// Посредник для кэширования ответов для запросов при вызове их обработчиков.
    /// </summary>
    /// <typeparam name="TRequest">Тип запроса.</typeparam>
    /// <typeparam name="TResponse">Тип ответа.</typeparam>
    public class ResponseCachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, ICacheableQuery
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;
        private readonly BehaviorSettings _behaviorSettings;
        private readonly IStringLocalizer<ResponseCachingBehavior> _localizer;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly CacheSettings _cacheSettings;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ResponseCachingBehavior{TRequest,TResponse}"/>.
        /// </summary>
        /// <param name="cache"><see cref="IDistributedCache"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        /// <param name="cacheSettings"><see cref="CacheSettings"/>.</param>
        /// <param name="behaviorSettings"><see cref="BehaviorSettings"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        public ResponseCachingBehavior(
            IDistributedCache cache,
            ILogger<TResponse> logger,
            IOptionsSnapshot<CacheSettings> cacheSettings,
            IOptionsSnapshot<BehaviorSettings> behaviorSettings,
            IStringLocalizer<ResponseCachingBehavior> localizer,
            IJsonSerializer jsonSerializer)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _behaviorSettings = behaviorSettings.Value;
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            _jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
            _cacheSettings = cacheSettings.Value;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            if (!_behaviorSettings.UseResponseCachingBehavior)
            {
                return await next();
            }

            TResponse response;
            if (request.BypassCache)
            {
                _logger.LogInformation(_localizer["Bypassing Cache for key '{CacheKey}'."], request.CacheKey);
                return await next();
            }

            async Task<TResponse> GetResponseAndAddToCache()
            {
                response = await next();
                var slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromHours(_cacheSettings.AbsoluteExpirationInHours);
                if (slidingExpiration <= TimeSpan.Zero)
                {
                    throw new BadRequestException(_localizer["Cache Sliding Expiration must be greater than 0."]);
                }

                var options = new DistributedCacheEntryOptions { SlidingExpiration = slidingExpiration };
                byte[] serializedData = Encoding.Default.GetBytes(_jsonSerializer.Serialize(response));
                await _cache.SetAsync(request.CacheKey, serializedData, options, cancellationToken);
                return response;
            }

            if (request.ReplaceCachedEntry)
            {
                response = await GetResponseAndAddToCache();
                _logger.LogInformation("Replacing Cache entry for key '{CacheKey}'.", request.CacheKey);
            }
            else
            {
                byte[] cachedResponse = request.CacheKey.IsPresent() ? await _cache.GetAsync(request.CacheKey, cancellationToken) : null;
                if (cachedResponse != null)
                {
                    response = _jsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cachedResponse));
                    _logger.LogInformation(_localizer["Fetched from Cache by key '{CacheKey}'."], request.CacheKey);
                }
                else
                {
                    response = await GetResponseAndAddToCache();
                    _logger.LogInformation(_localizer["Added to Cache by key '{CacheKey}'."], request.CacheKey);
                }
            }

            if (request.RefreshCachedEntry)
            {
                await _cache.RefreshAsync(request.CacheKey, cancellationToken);
                _logger.LogInformation("Cache refreshed for key '{CacheKey}'.", request.CacheKey);
            }

            return response;
        }
    }
}