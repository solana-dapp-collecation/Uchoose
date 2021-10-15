﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="AddRoleExtendedAttributeCommandValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Uchoose.Domain.Identity.Entities;
using Uchoose.SerializationService.Interfaces;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Commands.Validators.Abstractions;

namespace Uchoose.UseCases.Common.Features.Identity.Roles.Commands.Validators
{
    /// <summary>
    /// Валидатор команды для добавления расширенного атрибута роли пользователя.
    /// </summary>
    internal sealed class AddRoleExtendedAttributeCommandValidator : AddExtendedAttributeCommandValidator<Guid, UchooseRole>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="AddRoleExtendedAttributeCommandValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        public AddRoleExtendedAttributeCommandValidator(
            IStringLocalizer<AddRoleExtendedAttributeCommandValidator> localizer,
            IJsonSerializer jsonSerializer)
            : base(localizer, jsonSerializer)
        {
        }
    }
}