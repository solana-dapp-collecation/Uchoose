// ------------------------------------------------------------------------------------------------------
// <copyright file="RemoveNftImageLayerCommandValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Commands.Validators
{
    /// <summary>
    /// Валидатор команды для удаления слоя изображения NFT.
    /// </summary>
    internal sealed class RemoveNftImageLayerCommandValidator :
        AbstractValidator<RemoveNftImageLayerCommand>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="RemoveNftImageLayerCommandValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        public RemoveNftImageLayerCommandValidator(IStringLocalizer<RemoveNftImageLayerCommandValidator> localizer)
        {
            RuleFor(request => request.Id)
                .NotEmpty().WithMessage(_ => localizer["The '{PropertyName}' property value cannot be empty."]);
        }
    }
}