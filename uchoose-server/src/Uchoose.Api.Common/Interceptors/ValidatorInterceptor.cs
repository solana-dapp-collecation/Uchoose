// ------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorInterceptor.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Uchoose.Api.Common.Interceptors
{
    /// <summary>
    /// Перехватчик валидации.
    /// </summary>
    public class ValidatorInterceptor :
        IValidatorInterceptor
    {
        private readonly IStringLocalizer<ValidatorInterceptor> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ValidatorInterceptor"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public ValidatorInterceptor(
            IStringLocalizer<ValidatorInterceptor> localizer)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        /// <inheritdoc/>
        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext;
        }

        /// <inheritdoc/>
        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {
            var failures = result.Errors.Where(f => f != null).ToList();
            if (failures.Count != 0)
            {
                var errorMessages = failures.Select(a => a.ErrorMessage).Distinct().ToList();
                throw new UseCases.Common.Exceptions.ValidationException(_localizer, errorMessages);
            }

            return result;
        }
    }
}