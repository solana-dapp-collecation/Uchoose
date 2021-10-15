// ------------------------------------------------------------------------------------------------------
// <copyright file="IImportEntitiesCommandValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using FluentValidation;
using Microsoft.Extensions.Localization;
using Uchoose.Utils.Attributes.Importing;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Extensions;

namespace Uchoose.UseCases.Common.Features.Common.Commands.Validators
{
    /// <summary>
    /// Валидатор импорта сущности.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    /// <typeparam name="TImportCommand">Типа команды импорта для сущности.</typeparam>
    // ReSharper disable once UnusedTypeParameter
    internal interface IImportEntitiesCommandValidator<TEntityId, TEntity, TImportCommand>
        where TEntity : class, IEntity<TEntityId>
        where TImportCommand : ImportEntitiesCommand
    {
        /// <summary>
        /// Использовать правила валидации для фильтра.
        /// </summary>
        /// <param name="validator"><see cref="AbstractValidator{T}"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        static void UseRules(AbstractValidator<TImportCommand> validator, IStringLocalizer localizer)
        {
            validator.RuleFor(request => request)
                .Must(_ => typeof(TEntity).GetCustomAttribute(typeof(NotImportableAttribute)) == null).WithMessage(_ => string.Format(localizer["The '{0}' entity must be importable."], typeof(TEntity).GetGenericTypeName()));
            validator.RuleFor(request => request.DataFirstRowNumber)
                .GreaterThan(0).WithMessage(_ => localizer["The '{PropertyName}' property value {PropertyValue} should be greater than {ComparisonValue}."]);
            validator.When(request => request.TitlesRowNumber > 0, () =>
            {
                validator.RuleFor(request => request.DataFirstRowNumber)
                    .GreaterThan(request => request.TitlesRowNumber).WithMessage(_ => localizer["The {PropertyName} property value {PropertyValue} should be greater than '{ComparisonProperty}' with value {ComparisonValue}."]);
            });
            validator.When(request => request.DataLastRowNumber != null && request.DataFirstRowNumber > 0, () =>
            {
                validator.RuleFor(request => request.DataLastRowNumber)
                    .GreaterThan(request => request.DataFirstRowNumber).WithMessage(_ => localizer["The '{PropertyName}' property value {PropertyValue} should be greater than '{ComparisonProperty}' with value {ComparisonValue}."]);
            });
            validator.RuleFor(request => request.TitlesRowNumber)
                .GreaterThan(0).WithMessage(_ => localizer["The '{PropertyName}' property value {PropertyValue} should be greater than {ComparisonValue}."]);
            validator.RuleFor(request => request.TitlesFirstColNumber)
                .GreaterThan(0).WithMessage(_ => localizer["The '{PropertyName}' property value {PropertyValue} should be greater than {ComparisonValue}."]);
            validator.When(request => request.TitlesLastColNumber != null && request.TitlesFirstColNumber > 0, () =>
            {
                validator.RuleFor(request => request.TitlesLastColNumber)
                    .GreaterThan(request => request.TitlesFirstColNumber).WithMessage(_ => localizer["The '{PropertyName}' property with value {PropertyValue} should be greater than '{ComparisonProperty}' with value {ComparisonValue}."]);
            });
            validator.RuleFor(request => request.Data)
                .NotEmpty().WithMessage(_ => localizer["The '{PropertyName}' property value cannot be empty."]);
        }
    }
}