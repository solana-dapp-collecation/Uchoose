// ------------------------------------------------------------------------------------------------------
// <copyright file="ResultWithIntIdResponseExample.cs" company="Life Loop">
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
    /// Пример ответа, возвращающего <see cref="Result{T}"/> где T - int идентификатор.
    /// </summary>
    public class ResultWithIntIdResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<ResultWithIntIdResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ResultWithIntIdResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public ResultWithIntIdResponseExample(IStringLocalizer<ResultWithIntIdResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<int>.Success(0, _localizer["<Message>"]);
        }
    }
}