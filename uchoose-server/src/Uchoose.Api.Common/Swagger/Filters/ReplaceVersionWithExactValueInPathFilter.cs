// ------------------------------------------------------------------------------------------------------
// <copyright file="ReplaceVersionWithExactValueInPathFilter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Uchoose.Api.Common.Swagger.Filters
{
    /// <summary>
    /// Фильтр для удаления версии API из путей (path) в swagger документации.
    /// </summary>
    public class ReplaceVersionWithExactValueInPathFilter :
        IDocumentFilter
    {
        /// <inheritdoc/>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();

            foreach ((string key, var value) in swaggerDoc.Paths)
                paths.Add(key.Replace("v{version}", swaggerDoc.Info.Version), value);

            swaggerDoc.Paths = paths;
        }
    }
}