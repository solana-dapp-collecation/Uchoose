// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerTypesResponseExample.cs" company="Life Loop">
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
    /// Пример ответа для получения списка с данными типов слоя изображения NFT.
    /// </summary>
    public class NftImageLayerTypesResponseExample :
        IExamplesProvider<object>
    {
        private readonly IStringLocalizer<NftImageLayerTypesResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerTypesResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public NftImageLayerTypesResponseExample(IStringLocalizer<NftImageLayerTypesResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return PaginatedResult<NftImageLayerTypeResponse>.Success(
                new()
                {
                    new(
                        Guid.Empty,
                        _localizer["<Name>"],
                        _localizer["<Description>"],
                        false,
                        true),
                    new(
                        Guid.Empty,
                        _localizer["<Name>"],
                        _localizer["<Description>"],
                        false,
                        true)
                },
                2,
                1,
                10);
        }
    }
}