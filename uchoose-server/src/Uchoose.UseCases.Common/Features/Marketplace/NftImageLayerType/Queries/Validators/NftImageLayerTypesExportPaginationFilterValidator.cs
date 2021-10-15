// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerTypesExportPaginationFilterValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries.Filters;
using Uchoose.Utils.Filters.Validators;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries.Validators
{
    /// <summary>
    /// Валидатор фильтра для экспорта в файл типов слоёв изображений NFT с пагинацией.
    /// </summary>
    internal sealed class NftImageLayerTypesExportPaginationFilterValidator :
        ExportPaginationFilterValidator<Guid, Domain.Marketplace.Entities.NftImageLayerType, NftImageLayerTypesExportPaginationFilter>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerTypesExportPaginationFilterValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        public NftImageLayerTypesExportPaginationFilterValidator(IStringLocalizer<NftImageLayerTypesExportPaginationFilterValidator> localizer)
            : base(localizer)
        {
        }
    }
}