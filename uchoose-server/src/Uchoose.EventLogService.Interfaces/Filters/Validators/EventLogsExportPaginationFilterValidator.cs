// ------------------------------------------------------------------------------------------------------
// <copyright file="EventLogsExportPaginationFilterValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using FluentValidation;
using Microsoft.Extensions.Localization;
using Uchoose.Domain.Entities;
using Uchoose.Utils.Filters.Validators;

namespace Uchoose.EventLogService.Interfaces.Filters.Validators
{
    /// <summary>
    /// Валидатор фильтра для экспорта в файл логов событий с пагинацией.
    /// </summary>
    internal sealed class EventLogsExportPaginationFilterValidator :
        ExportPaginationFilterValidator<Guid, EventLog, EventLogsExportPaginationFilter>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="EventLogsExportPaginationFilterValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public EventLogsExportPaginationFilterValidator(IStringLocalizer<EventLogsExportPaginationFilterValidator> localizer)
            : base(localizer)
        {
            When(request => request.StartDateRange != null && request.EndDateRange != null, () =>
            {
                RuleFor(request => request.EndDateRange)
                    .GreaterThanOrEqualTo(request => request.StartDateRange).WithMessage(_ => localizer["The '{PropertyName}' property with value {PropertyValue} should be greater than or equal to '{ComparisonProperty}' with value {ComparisonValue}."]);
            });

            When(request => request.StartAggregateVersionRange != null, () =>
            {
                RuleFor(request => request.StartAggregateVersionRange)
                    .GreaterThanOrEqualTo(-1).WithMessage(_ => localizer["The '{PropertyName}' property value {PropertyValue} should be greater than or equal to {ComparisonValue}."]);
            });
            When(request => request.EndAggregateVersionRange != null, () =>
            {
                RuleFor(request => request.EndAggregateVersionRange)
                    .GreaterThanOrEqualTo(-1).WithMessage(_ => localizer["The '{PropertyName}' property value {PropertyValue} should be greater than or equal to {ComparisonValue}."]);
            });
            When(request => request.StartAggregateVersionRange != null && request.EndAggregateVersionRange != null, () =>
            {
                RuleFor(request => request.EndAggregateVersionRange)
                    .GreaterThanOrEqualTo(request => request.StartAggregateVersionRange).WithMessage(_ => localizer["The '{PropertyName}' property with value {PropertyValue} should be greater than or equal to '{ComparisonProperty}' with value {ComparisonValue}."]);
            });
        }
    }
}