// ------------------------------------------------------------------------------------------------------
// <copyright file="LogEventRequestValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Uchoose.EventLogService.Interfaces.Requests.Validators
{
    /// <summary>
    /// Валидатор запроса на добавление пользовательского события в логи событий.
    /// </summary>
    internal sealed class LogEventRequestValidator : AbstractValidator<LogEventRequest>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="LogEventRequestValidator"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public LogEventRequestValidator(IStringLocalizer<LogEventRequestValidator> localizer)
        {
            RuleFor(request => request.UserId)
                .NotEmpty().WithMessage(_ => localizer["The '{PropertyName}' property value cannot be empty."]);
            RuleFor(request => request.Data)
                .NotEmpty().WithMessage(_ => localizer["The '{PropertyName}' property value cannot be empty."]);
        }
    }
}