// ------------------------------------------------------------------------------------------------------
// <copyright file="AuditTrailsPaginationFilterValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using FluentValidation;
using Microsoft.Extensions.Localization;
using Uchoose.Domain.Entities;
using Uchoose.Utils.Filters.Validators;

namespace Uchoose.AuditService.Interfaces.Filters.Validators
{
    /// <summary>
    /// Валидатор фильтра для получения данных аудита с пагинацией.
    /// </summary>
    internal sealed class AuditTrailsPaginationFilterValidator :
        PaginationFilterValidator<Guid, Audit, AuditTrailsPaginationFilter>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="AuditTrailsPaginationFilterValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public AuditTrailsPaginationFilterValidator(IStringLocalizer<AuditTrailsPaginationFilterValidator> localizer)
            : base(localizer)
        {
            When(request => request.StartDateRange != null && request.EndDateRange != null, () =>
            {
                RuleFor(request => request.EndDateRange)
                    .GreaterThanOrEqualTo(request => request.StartDateRange).WithMessage(_ => localizer["The '{PropertyName}' property with value {PropertyValue} should be greater than or equal to '{ComparisonProperty}' with value {ComparisonValue}."]);
            });
        }
    }
}