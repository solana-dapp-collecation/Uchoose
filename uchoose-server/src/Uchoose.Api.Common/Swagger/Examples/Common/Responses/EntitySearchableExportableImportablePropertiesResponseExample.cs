// ------------------------------------------------------------------------------------------------------
// <copyright file="EntitySearchableExportableImportablePropertiesResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Common.Responses
{
    /// <summary>
    /// Пример ответа, возвращающего словарь допустимых названий свойств c их описанием для экспорта/импорта.
    /// </summary>
    public class EntitySearchableExportableImportablePropertiesResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<EntitySearchableExportableImportablePropertiesResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="EntitySearchableExportableImportablePropertiesResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public EntitySearchableExportableImportablePropertiesResponseExample(IStringLocalizer<EntitySearchableExportableImportablePropertiesResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<Dictionary<string, string>>.Success(
                new()
                {
                    { _localizer["<Property name 1>"], _localizer["<Property description 1>"] },
                    { _localizer["<Property name 2>"], _localizer["<Property description 2>"] }
                },
                _localizer["<Message>"]);
        }
    }
}