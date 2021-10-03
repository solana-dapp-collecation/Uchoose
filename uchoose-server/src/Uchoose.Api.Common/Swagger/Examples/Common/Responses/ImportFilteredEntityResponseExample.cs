// ------------------------------------------------------------------------------------------------------
// <copyright file="ImportFilteredEntityResponseExample.cs" company="Life Loop">
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
    /// Пример ответа, возвращающего количество импортированных данных.
    /// </summary>
    public class ImportFilteredEntityResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<ImportFilteredEntityResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ImportFilteredEntityResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public ImportFilteredEntityResponseExample(IStringLocalizer<ImportFilteredEntityResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<int>.Success(10, _localizer["<Message>"]);
        }
    }
}