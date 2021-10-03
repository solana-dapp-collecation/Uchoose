// ------------------------------------------------------------------------------------------------------
// <copyright file="SwaggerLanguageHeaderOperationFilter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Uchoose.Api.Common.Swagger.Filters
{
    /// <summary>
    /// Фильтр для добавления заголовка с выбором языка в swagger документации.
    /// </summary>
    public class SwaggerLanguageHeaderOperationFilter :
        IOperationFilter
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Инициализирует экземпляр <see cref="SwaggerLanguageHeaderOperationFilter"/>.
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/>.</param>
        public SwaggerLanguageHeaderOperationFilter(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new()
            {
                Name = "Accept-Language",
                Description = "Supported languages", // TODO - локализовать?
                In = ParameterLocation.Header,
                Required = false,
                Schema = new()
                {
                    Type = "string",
                    Enum = (_serviceProvider
                            .GetService(typeof(IOptions<RequestLocalizationOptions>)) as IOptions<RequestLocalizationOptions>)?
                        .Value?
                        .SupportedCultures?.Select(c => new OpenApiString(c.TwoLetterISOLanguageName)).ToList<IOpenApiAny>(),
                }
            });
        }
    }
}