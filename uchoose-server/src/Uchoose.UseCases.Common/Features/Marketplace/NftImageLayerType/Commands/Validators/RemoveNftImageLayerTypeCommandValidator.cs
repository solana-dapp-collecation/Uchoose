// ------------------------------------------------------------------------------------------------------
// <copyright file="RemoveNftImageLayerTypeCommandValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Commands.Validators
{
    /// <summary>
    /// Валидатор команды для удаления типа слоя изображения NFT.
    /// </summary>
    internal sealed class RemoveNftImageLayerTypeCommandValidator :
        AbstractValidator<RemoveNftImageLayerTypeCommand>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="RemoveNftImageLayerTypeCommandValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        public RemoveNftImageLayerTypeCommandValidator(IStringLocalizer<RemoveNftImageLayerTypeCommandValidator> localizer)
        {
            RuleFor(request => request.Id)
                .NotEmpty().WithMessage(_ => localizer["The '{PropertyName}' property value cannot be empty."]);
        }
    }
}