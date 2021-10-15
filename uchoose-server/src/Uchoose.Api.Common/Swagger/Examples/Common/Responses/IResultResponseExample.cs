// ------------------------------------------------------------------------------------------------------
// <copyright file="IResultResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Common.Responses
{
    /// <summary>
    /// Пример ответа, возвращающего <see cref="IResult"/>.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class IResultResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<IResultResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="IResultResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public IResultResponseExample(IStringLocalizer<IResultResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result.Success(_localizer["<Message>"]);
        }
    }
}