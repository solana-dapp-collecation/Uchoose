// ------------------------------------------------------------------------------------------------------
// <copyright file="AddNftImageLayerCommandValidator.cs" company="Life Loop">
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
    /// Валидатор команды для добавления слоя изображения NFT.
    /// </summary>
    internal sealed class AddNftImageLayerCommandValidator :
        AbstractValidator<AddNftImageLayerCommand>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="AddNftImageLayerCommandValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        public AddNftImageLayerCommandValidator(IStringLocalizer<AddNftImageLayerCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage(_ => localizer["The '{PropertyName}' property value cannot be empty."]);
            RuleFor(request => request.ArtistDid)
                .NotEmpty().WithMessage(_ => localizer["The '{PropertyName}' property value cannot be empty."]);
        }
    }
}