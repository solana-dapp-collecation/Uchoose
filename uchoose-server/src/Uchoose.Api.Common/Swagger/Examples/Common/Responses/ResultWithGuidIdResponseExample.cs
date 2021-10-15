// ------------------------------------------------------------------------------------------------------
// <copyright file="ResultWithGuidIdResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Common.Responses
{
    /// <summary>
    /// Пример ответа, возвращающего <see cref="Result{T}"/> где T - Guid идентификатор.
    /// </summary>
    public class ResultWithGuidIdResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<ResultWithGuidIdResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ResultWithGuidIdResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public ResultWithGuidIdResponseExample(IStringLocalizer<ResultWithGuidIdResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<Guid>.Success(Guid.Empty, _localizer["<Message>"]);
        }
    }
}