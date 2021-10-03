// ------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceBehavior.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Uchoose.CurrentUserService.Interfaces;
using Uchoose.UseCases.Common.Settings;
using Uchoose.Utils.Contracts.Performance;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Performance;

namespace Uchoose.UseCases.Common.Behaviors
{
    /// <summary>
    /// Посредник для замера производительности запросов до получения ответа при вызове их обработчиков.
    /// </summary>
    /// <remarks>
    /// Для локализации.
    /// </remarks>
    public class PerformanceBehavior
    {
        // for localization
    }

    /// <summary>
    /// Посредник для замера производительности запросов до получения ответа при вызове их обработчиков.
    /// </summary>
    /// <typeparam name="TRequest">Тип запроса.</typeparam>
    /// <typeparam name="TResponse">Тип ответа.</typeparam>
    public class PerformanceBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
            where TRequest : notnull, IPerformanceMeasurableRequest
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
        private readonly BehaviorSettings _behaviorSettings;
        private readonly IStringLocalizer<PerformanceBehavior> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="PerformanceBehavior{TRequest,TResponse}"/>.
        /// </summary>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        /// <param name="behaviorSettings"><see cref="BehaviorSettings"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public PerformanceBehavior(
            ICurrentUserService currentUserService,
            ILogger<PerformanceBehavior<TRequest, TResponse>> logger,
            IOptionsSnapshot<BehaviorSettings> behaviorSettings,
            IStringLocalizer<PerformanceBehavior> localizer)
        {
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _behaviorSettings = behaviorSettings.Value;
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            if (!_behaviorSettings.UsePerformanceBehavior)
            {
                return await next();
            }

            var stopwatch = ValueStopwatch.StartNew();
            var response = await next();
            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            string requestTypeName = typeof(TRequest).GetGenericTypeName();
            if (elapsedMilliseconds > request.ElapsedMillisecondsLimit)
            {
                _logger
                    .LogWarning(
                        _localizer["Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) by UserId - {UserId}"],
                        requestTypeName,
                        elapsedMilliseconds,
                        _currentUserService.GetUserId());
            }
            else if (request.LogIfLimitNotReached)
            {
                _logger
                    .LogInformation(
                        _localizer["Request: {Name} ({ElapsedMilliseconds} milliseconds) by UserId - {UserId}"],
                        requestTypeName,
                        elapsedMilliseconds,
                        _currentUserService.GetUserId());
            }

            return response;
        }
    }
}