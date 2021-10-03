// ------------------------------------------------------------------------------------------------------
// <copyright file="AddNftImageLayerTypeCommandValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Commands.Validators
{
    /// <summary>
    /// Валидатор команды для добавления типа слоя изображения NFT.
    /// </summary>
    internal sealed class AddNftImageLayerTypeCommandValidator :
        AbstractValidator<AddNftImageLayerTypeCommand>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="AddNftImageLayerTypeCommandValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        public AddNftImageLayerTypeCommandValidator(IStringLocalizer<AddNftImageLayerTypeCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage(_ => localizer["The '{PropertyName}' property value cannot be empty."]);
        }
    }
}