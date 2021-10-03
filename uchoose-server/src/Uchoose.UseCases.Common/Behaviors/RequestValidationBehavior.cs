// ------------------------------------------------------------------------------------------------------
// <copyright file="RequestValidationBehavior.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Uchoose.UseCases.Common.Settings;

namespace Uchoose.UseCases.Common.Behaviors
{
    /// <summary>
    /// Посредник для проверки валидации запросов при вызове их обработчиков.
    /// </summary>
    /// <remarks>
    /// Для локализации.
    /// </remarks>
    public class RequestValidationBehavior
    {
        // for localization
    }

    /// <summary>
    /// Посредник для проверки валидации запросов при вызове их обработчиков.
    /// </summary>
    /// <typeparam name="TRequest">Тип запроса.</typeparam>
    /// <typeparam name="TResponse">Тип ответа.</typeparam>
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly BehaviorSettings _behaviorSettings;
        private readonly IStringLocalizer<RequestValidationBehavior> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="RequestValidationBehavior{TRequest,TResponse}"/>.
        /// </summary>
        /// <param name="validators">Коллекция валидаторов.</param>
        /// <param name="behaviorSettings"><see cref="BehaviorSettings"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public RequestValidationBehavior(
            IEnumerable<IValidator<TRequest>> validators,
            IOptionsSnapshot<BehaviorSettings> behaviorSettings,
            IStringLocalizer<RequestValidationBehavior> localizer)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
            _behaviorSettings = behaviorSettings.Value;
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            if (!_behaviorSettings.UseRequestValidationBehavior)
            {
                return await next();
            }

            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    var errorMessages = failures.Select(a => a.ErrorMessage).Distinct().ToList();
                    throw new Exceptions.ValidationException(_localizer, errorMessages);
                }
            }

            return await next();
        }
    }
}