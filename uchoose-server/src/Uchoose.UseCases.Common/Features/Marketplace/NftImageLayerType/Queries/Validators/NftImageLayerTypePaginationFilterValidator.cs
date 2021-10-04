// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerTypePaginationFilterValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries.Filters;
using Uchoose.Utils.Filters.Validators;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries.Validators
{
    /// <summary>
    /// Валидатор фильтра для получения типов слоёв изображений NFT с пагинацией.
    /// </summary>
    internal sealed class NftImageLayerTypePaginationFilterValidator :
        PaginationFilterValidator<Guid, Domain.Marketplace.Entities.NftImageLayerType, NftImageLayerTypePaginationFilter>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerTypePaginationFilterValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        public NftImageLayerTypePaginationFilterValidator(IStringLocalizer<NftImageLayerTypePaginationFilterValidator> localizer)
            : base(localizer)
        {
        }
    }
}