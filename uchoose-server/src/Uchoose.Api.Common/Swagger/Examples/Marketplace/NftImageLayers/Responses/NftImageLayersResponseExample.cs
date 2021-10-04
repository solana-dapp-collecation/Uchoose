// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayersResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
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
    /// Пример ответа для получения списка с данными типов слоя изображения NFT.
    /// </summary>
    public class NftImageLayersResponseExample :
        IExamplesProvider<object>
    {
        private readonly IStringLocalizer<NftImageLayersResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayersResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public NftImageLayersResponseExample(IStringLocalizer<NftImageLayersResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return PaginatedResult<NftImageLayerResponse>.Success(
                new()
                {
                    new(
                        Guid.Empty,
                        _localizer["<Name>"],
                        Guid.Empty,
                        _localizer["<Uri>"],
                        _localizer["<DID>"],
                        false,
                        true),
                    new(
                        Guid.Empty,
                        _localizer["<Name>"],
                        Guid.Empty,
                        _localizer["<Uri>"],
                        _localizer["<DID>"],
                        false,
                        true)
                },
                2,
                1,
                10);
        }
    }
}