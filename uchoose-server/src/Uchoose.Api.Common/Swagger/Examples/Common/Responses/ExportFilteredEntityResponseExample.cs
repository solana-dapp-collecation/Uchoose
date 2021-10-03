// ------------------------------------------------------------------------------------------------------
// <copyright file="ExportFilteredEntityResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Common.Responses
{
    /// <summary>
    /// Пример ответа, возвращающего <see cref="Result{T}"/> где T - base64 строка с экспортированными данными.
    /// </summary>
    public class ExportFilteredEntityResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<ExportFilteredEntityResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ExportFilteredEntityResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public ExportFilteredEntityResponseExample(IStringLocalizer<ExportFilteredEntityResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<string>.Success(_localizer["<Base64 data>"], _localizer["<Message>"]);
        }
    }
}