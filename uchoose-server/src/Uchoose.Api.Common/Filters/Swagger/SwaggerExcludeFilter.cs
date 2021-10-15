// ------------------------------------------------------------------------------------------------------
// <copyright file="SwaggerExcludeFilter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Uchoose.Utils.Attributes.Swagger;

namespace Uchoose.Api.Common.Filters.Swagger
{
    /// <summary>
    /// Фильтр для удаления игнорируемых параметров из swagger документации.
    /// </summary>
    public class SwaggerExcludeFilter : IOperationFilter
    {
        /// <inheritdoc/>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context?.MethodInfo == null)
            {
                return;
            }

            var parameters = context.MethodInfo.GetParameters();
            var properties = parameters.SelectMany(x => x.ParameterType.GetProperties());
            var propertiesToRemove = properties
                .Where(p => p.GetCustomAttribute<SwaggerExcludeAttribute>() != null && p.GetCustomAttribute<FromQueryAttribute>() != null)
                .Select(p => p.Name)
                .ToHashSet(StringComparer.InvariantCultureIgnoreCase);

            foreach (var parameter in operation.Parameters.ToList())
            {
                if (propertiesToRemove.Contains(parameter.Name))
                {
                    operation.Parameters.Remove(parameter);
                }
            }
        }
    }
}