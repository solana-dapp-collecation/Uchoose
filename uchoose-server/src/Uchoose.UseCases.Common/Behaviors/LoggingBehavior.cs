// ------------------------------------------------------------------------------------------------------
// <copyright file="LoggingBehavior.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper.Internal;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Uchoose.CurrentUserService.Interfaces;
using Uchoose.UseCases.Common.Settings;
using Uchoose.Utils.Attributes.Logging;
using Uchoose.Utils.Contracts.Logging;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Behaviors
{
    /// <summary>
    /// Посредник для логирования запросов и ответов при вызове их обработчиков.
    /// </summary>
    /// <remarks>
    /// Для локализации.
    /// </remarks>
    public class LoggingBehavior
    {
        // for localization
    }

    /// <summary>
    /// Посредник для логирования запросов и ответов при вызове их обработчиков.
    /// </summary>
    /// <typeparam name="TRequest">Тип запроса.</typeparam>
    /// <typeparam name="TResponse">Тип ответа.</typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull // where TResponse : IResult<T> where T : class
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly BehaviorSettings _behaviorSettings;
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IStringLocalizer<LoggingBehavior> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="LoggingBehavior{TRequest,TResponse}"/>.
        /// </summary>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        /// <param name="behaviorSettings"><see cref="BehaviorSettings"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public LoggingBehavior(
            ICurrentUserService currentUserService,
            IOptionsSnapshot<BehaviorSettings> behaviorSettings,
            ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            IStringLocalizer<LoggingBehavior> localizer)
        {
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _behaviorSettings = behaviorSettings.Value;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            if (!_behaviorSettings.UseLoggingBehavior)
            {
                return await next();
            }

            if (request is ILoggable loggableRequest)
            {
                if (loggableRequest.GetType().GetCustomAttribute<NotLoggedAttribute>() == null)
                {
                    var loggedRequestProperties = GetLoggedPropertiesValues(request);
                    _logger.LogInformation(_localizer["Handling {RequestTypeName} with properties {RequestProperties} by UserId - {UserId}"], typeof(TRequest).GetGenericTypeName(), loggedRequestProperties, _currentUserService.GetUserId());
                }
            }

            var response = await next();

            var responseType = response?.GetType();
            if (response is ILoggable
                || (responseType?.IsGenericType == true && responseType.IsSubclassOf(typeof(Result)) && responseType.GenericTypeArguments.All(x => typeof(ILoggable).IsAssignableFrom(x))))
            {
                if (responseType != null && responseType.GetCustomAttribute<NotLoggedAttribute>() == null)
                {
                    var loggedResponseProperties = GetLoggedPropertiesValues(response);
                    _logger.LogInformation(_localizer["Handled {ResponseTypeName} with properties {ResponseProperties} by UserId - {UserId}"], typeof(TResponse).GetGenericTypeName(), loggedResponseProperties, _currentUserService.GetUserId());
                }
            }

            return response;
        }

        #region GetLoggedPropertiesValues

        /// <summary>
        /// Получить словарь свойств и их значений, которые будут залогированы.
        /// </summary>
        /// <param name="value">Значение для логирования.</param>
        /// <returns>Возвращает словарь свойств и их значений, которые будут залогированы.</returns>
        private static Dictionary<string, object?> GetLoggedPropertiesValues(object? value)
        {
            var result = new Dictionary<string, object?>();
            if (value == null)
            {
                return result;
            }

            var type = value.GetType();
            if (type.GetCustomAttribute<NotLoggedAttribute>() == null)
            {
                foreach (var typeProperty in new List<PropertyInfo>(type.GetProperties()))
                {
                    if (typeProperty.GetCustomAttribute<NotLoggedAttribute>() == null)
                    {
                        object? propValue = typeProperty.GetValue(value, null);
                        var propType = typeProperty.PropertyType;
                        if (propType.IsGenericType && propType.IsEnumerableType() && propType.GenericTypeArguments.All(x => typeof(ILoggable).IsAssignableFrom(x)))
                        {
                            if (propValue is IEnumerable<object> propCollection)
                            {
                                result.Add(typeProperty.Name, propCollection.Select(GetLoggedPropertiesValues).ToList());
                            }
                        }
                        else if (typeof(ILoggable).IsAssignableFrom(propType))
                        {
                            result.Add(typeProperty.Name, GetLoggedPropertiesValues(propValue));
                        }
                        else
                        {
                            var notLoggedAttribute = typeProperty.GetCustomAttribute<NotLoggedAttribute>();
                            if (notLoggedAttribute == null)
                            {
                                var replacedAttribute = typeProperty.GetCustomAttribute<LogReplacedAttribute>();
                                if (replacedAttribute != null)
                                {
                                    propValue = replacedAttribute.ReplaceValue(propValue);
                                }

                                var maskedAttribute = typeProperty.GetCustomAttribute<LogMaskedAttribute>();
                                if (maskedAttribute != null)
                                {
                                    propValue = maskedAttribute.MaskValue(propValue);
                                }

                                result.Add(typeProperty.Name, propValue);
                            }
                        }
                    }
                }
            }

            return result;
        }

        #endregion GetLoggedPropertiesValues
    }
}