// ------------------------------------------------------------------------------------------------------
// <copyright file="ImportNftImageLayerTypesCommandValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Uchoose.UseCases.Common.Features.Common.Commands.Validators;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Commands.Validators
{
    /// <summary>
    /// Валидатор команды для импорта типов слоёв изображений NFT из файла.
    /// </summary>
    internal sealed class ImportNftImageLayerTypesCommandValidator :
        ImportEntitiesCommandValidator<Guid, Domain.Marketplace.Entities.NftImageLayerType, ImportNftImageLayerTypesCommand>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="ImportNftImageLayerTypesCommandValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        public ImportNftImageLayerTypesCommandValidator(IStringLocalizer<ImportNftImageLayerTypesCommandValidator> localizer)
            : base(localizer)
        {
        }
    }
}