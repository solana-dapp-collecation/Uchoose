// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Marketplace.NftImageLayers.Responses
{
    /// <summary>
    /// Пример ответа для получения данных слоя изображения NFT.
    /// </summary>
    public class NftImageLayerResponseExample :
        IExamplesProvider<object>
    {
        private readonly IStringLocalizer<NftImageLayerResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public NftImageLayerResponseExample(IStringLocalizer<NftImageLayerResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<NftImageLayerResponse>.Success(
                new(
                    Guid.Empty,
                    _localizer["<Name>"],
                    Guid.Empty,
                    _localizer["<Uri>"],
                    _localizer["<DID>"],
                    false,
                    true),
                _localizer["<Message>"]);
        }
    }
}