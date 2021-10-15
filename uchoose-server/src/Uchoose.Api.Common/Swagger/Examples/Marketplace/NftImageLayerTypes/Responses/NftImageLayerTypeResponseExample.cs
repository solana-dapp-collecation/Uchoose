// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerTypeResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Marketplace.NftImageLayerTypes.Responses
{
    /// <summary>
    /// Пример ответа для получения данных типа слоя изображения NFT.
    /// </summary>
    public class NftImageLayerTypeResponseExample :
        IExamplesProvider<object>
    {
        private readonly IStringLocalizer<NftImageLayerTypeResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerTypeResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public NftImageLayerTypeResponseExample(IStringLocalizer<NftImageLayerTypeResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<NftImageLayerTypeResponse>.Success(
                new(
                    Guid.Empty,
                    _localizer["<Name>"],
                    _localizer["<Description>"],
                    false,
                    true),
                _localizer["<Message>"]);
        }
    }
}