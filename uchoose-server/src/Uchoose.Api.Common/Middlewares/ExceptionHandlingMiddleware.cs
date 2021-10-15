// ------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandlingMiddleware.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Uchoose.Domain.Exceptions;
using Uchoose.Domain.Identity.Exceptions;
using Uchoose.SerializationService.Interfaces;
using Uchoose.Utils.Exceptions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Middlewares
{
    /// <summary>
    /// Голобальный обработчик исключений.
    /// </summary>
    internal class ExceptionHandlingMiddleware :
        IMiddleware
    {
        private readonly IHostEnvironment _env;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IJsonSerializer _jsonSerializer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ExceptionHandlingMiddleware"/>.
        /// </summary>
        /// <param name="env"><see cref="IHostEnvironment"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        public ExceptionHandlingMiddleware(
            IHostEnvironment env,
            ILogger<ExceptionHandlingMiddleware> logger,
            IJsonSerializer jsonSerializer)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
        }

        /// <inheritdoc/>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = MediaTypeNames.Application.Json;

                if (exception is not CustomException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                }

                var responseModel = await ErrorResult<string>.ReturnErrorAsync(exception.Message);
                responseModel.Source = exception.Source;
                responseModel.Exception = exception.Message;
                try
                {
                    if (_env.IsDevelopment())
                    {
                        int? pos = exception.StackTrace?.IndexOf(Environment.NewLine);
                        responseModel.StackTrace = exception.StackTrace?.Trim()
                            .Substring(0, pos != null ? (int)pos - 3 : exception.StackTrace?.Trim().Length ?? 0).Trim();
                    }
                }
                catch
                {
                    // ignored
                }

                // var currentUserService = context.RequestServices.GetService(typeof(ICurrentUserService)) as ICurrentUserService;
                // var additionalProperties = currentUserService?.GetAdditionalPropertiesForLogging();

                switch (exception)
                {
                    case LockedOutException e:
                        response.StatusCode = responseModel.ErrorCode = (int)e.StatusCode;
                        responseModel.Messages = e.ErrorMessages;
                        if (e.LockoutEnd != null)
                        {
                            response.Headers.Add(HeaderNames.RetryAfter, e.LockoutEnd?.ToString(CultureInfo.CurrentCulture));
                            _logger.LogError(exception.Message + " Lockout end: {LockoutEnd}", e.LockoutEnd); // TODO - локализовать
                        }
                        else
                        {
                            _logger.LogError(exception.Message);
                        }

                        break;

                    case BusinessRuleValidationException e:
                        _logger.LogError(exception.Message);
                        response.StatusCode = responseModel.ErrorCode = (int)e.StatusCode;
                        responseModel.Messages = e.ErrorMessages.Union(new List<string> { e.BrokenRule.Message }).ToList();
                        break;

                    case CustomException e:
                        _logger.LogError(exception.Message);
                        response.StatusCode = responseModel.ErrorCode = (int)e.StatusCode;
                        responseModel.Messages = e.ErrorMessages;
                        break;

                    case KeyNotFoundException:
                        _logger.LogError(exception.Message);
                        response.StatusCode = responseModel.ErrorCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        _logger.LogCritical(exception, exception.Message);
                        response.StatusCode = responseModel.ErrorCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                string result = _jsonSerializer.Serialize(responseModel, new()
                {
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await response.WriteAsync(result);
            }
        }
    }
}