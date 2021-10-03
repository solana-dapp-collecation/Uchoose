// ------------------------------------------------------------------------------------------------------
// <copyright file="ResultWithStringGuidIdResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Common.Responses
{
    /// <summary>
    /// Пример ответа, возвращающего <see cref="Result{T}"/> где T - Guid идентификатор в виде строки.
    /// </summary>
    public class ResultWithStringGuidIdResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<ResultWithStringGuidIdResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ResultWithStringGuidIdResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public ResultWithStringGuidIdResponseExample(IStringLocalizer<ResultWithStringGuidIdResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<string>.Success(Guid.Empty.ToString(), _localizer["<Message>"]);
        }
    }
}