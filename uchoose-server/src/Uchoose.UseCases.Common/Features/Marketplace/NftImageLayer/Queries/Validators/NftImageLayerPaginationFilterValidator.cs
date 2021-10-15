// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerPaginationFilterValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries.Filters;
using Uchoose.Utils.Filters.Validators;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries.Validators
{
    /// <summary>
    /// Валидатор фильтра для получения слоёв изображений NFT с пагинацией.
    /// </summary>
    internal sealed class NftImageLayerPaginationFilterValidator :
        PaginationFilterValidator<Guid, Domain.Marketplace.Entities.NftImageLayer, NftImageLayerPaginationFilter>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerPaginationFilterValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        public NftImageLayerPaginationFilterValidator(IStringLocalizer<NftImageLayerPaginationFilterValidator> localizer)
            : base(localizer)
        {
        }
    }
}