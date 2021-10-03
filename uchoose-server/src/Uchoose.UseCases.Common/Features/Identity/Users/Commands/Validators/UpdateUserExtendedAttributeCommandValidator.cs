// ------------------------------------------------------------------------------------------------------
// <copyright file="UpdateUserExtendedAttributeCommandValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Uchoose.Domain.Identity.Entities;
using Uchoose.SerializationService.Interfaces;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Commands.Validators.Abstractions;

namespace Uchoose.UseCases.Common.Features.Identity.Users.Commands.Validators
{
    /// <summary>
    /// Валидатор команды для обновления расширенного атрибута пользователя.
    /// </summary>
    internal sealed class UpdateUserExtendedAttributeCommandValidator : UpdateExtendedAttributeCommandValidator<Guid, UchooseUser>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="UpdateUserExtendedAttributeCommandValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        public UpdateUserExtendedAttributeCommandValidator(
            IStringLocalizer<UpdateUserExtendedAttributeCommandValidator> localizer,
            IJsonSerializer jsonSerializer)
            : base(localizer, jsonSerializer)
        {
        }
    }
}