﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="RemoveRoleExtendedAttributeCommandValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Uchoose.Domain.Identity.Entities;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Commands.Validators.Abstractions;

namespace Uchoose.UseCases.Common.Features.Identity.Roles.Commands.Validators
{
    /// <summary>
    /// Валидатор команды для удаления расширенного атрибута роли пользователя.
    /// </summary>
    internal sealed class RemoveRoleExtendedAttributeCommandValidator : RemoveExtendedAttributeCommandValidator<Guid, UchooseRole>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="RemoveRoleExtendedAttributeCommandValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public RemoveRoleExtendedAttributeCommandValidator(
            IStringLocalizer<RemoveRoleExtendedAttributeCommandValidator> localizer)
            : base(localizer)
        {
        }
    }
}