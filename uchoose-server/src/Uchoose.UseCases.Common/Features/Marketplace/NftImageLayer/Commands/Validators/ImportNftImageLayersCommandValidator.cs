// ------------------------------------------------------------------------------------------------------
// <copyright file="ImportNftImageLayersCommandValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Uchoose.UseCases.Common.Features.Common.Commands.Validators;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Commands.Validators
{
    /// <summary>
    /// Валидатор команды для импорта слоёв изображений NFT из файла.
    /// </summary>
    internal sealed class ImportNftImageLayersCommandValidator :
        ImportEntitiesCommandValidator<Guid, Domain.Marketplace.Entities.NftImageLayer, ImportNftImageLayersCommand>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="ImportNftImageLayersCommandValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        public ImportNftImageLayersCommandValidator(IStringLocalizer<ImportNftImageLayersCommandValidator> localizer)
            : base(localizer)
        {
        }
    }
}