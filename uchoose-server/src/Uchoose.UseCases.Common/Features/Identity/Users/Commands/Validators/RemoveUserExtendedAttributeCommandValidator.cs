// ------------------------------------------------------------------------------------------------------
// <copyright file="RemoveUserExtendedAttributeCommandValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Uchoose.Domain.Identity.Entities;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Commands.Validators.Abstractions;

namespace Uchoose.UseCases.Common.Features.Identity.Users.Commands.Validators
{
    /// <summary>
    /// Валидатор команды для удаления расширенного атрибута пользователя.
    /// </summary>
    internal sealed class RemoveUserExtendedAttributeCommandValidator : RemoveExtendedAttributeCommandValidator<Guid, UchooseUser>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="RemoveUserExtendedAttributeCommandValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public RemoveUserExtendedAttributeCommandValidator(
            IStringLocalizer<RemoveUserExtendedAttributeCommandValidator> localizer)
            : base(localizer)
        {
        }
    }
}