// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleExtendedAttributePaginationFilterValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Uchoose.Domain.Filters.Validators;
using Uchoose.Domain.Identity.Entities;

namespace Uchoose.UseCases.Common.Features.Identity.Roles.Queries.Validators
{
    /// <inheritdoc/>
    internal sealed class RoleExtendedAttributePaginationFilterValidator :
        ExtendedAttributePaginationFilterValidator<Guid, UchooseRole>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="RoleExtendedAttributePaginationFilterValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public RoleExtendedAttributePaginationFilterValidator(IStringLocalizer<RoleExtendedAttributePaginationFilterValidator> localizer)
            : base(localizer)
        {
        }
    }
}