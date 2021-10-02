// ------------------------------------------------------------------------------------------------------
// <copyright file="IExportPaginationFilterValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using FluentValidation;
using Microsoft.Extensions.Localization;
using Uchoose.Utils.Attributes.Exporting;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Extensions;

namespace Uchoose.Utils.Filters.Validators
{
    /// <summary>
    /// Валидатор экспорта сущности.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип экспортируемой сущности.</typeparam>
    /// <typeparam name="TFilter">Типа фильтра для экспорта для сущности.</typeparam>
    internal interface IExportPaginationFilterValidator<TEntityId, TEntity, TFilter>
        where TEntity : class, IEntity<TEntityId>, IExportable<TEntityId, TEntity>, new()
        where TFilter : ExportPaginationFilter<TEntityId, TEntity>
    {
        /// <summary>
        /// Использовать правила валидации для фильтра.
        /// </summary>
        /// <param name="validator"><see cref="AbstractValidator{T}"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        static void UseRules(AbstractValidator<TFilter> validator, IStringLocalizer localizer)
        {
            validator.RuleFor(request => request)
                .Must(_ => typeof(TEntity).GetCustomAttribute(typeof(NotExportableAttribute)) == null).WithMessage(_ => string.Format(localizer["The '{0}' entity must be exportable."], typeof(TEntity).GetGenericTypeName()));

            validator.RuleFor(request => request.DataFirstRowNumber)
                .GreaterThan(0).WithMessage(_ => localizer["The '{PropertyName}' property value {PropertyValue} should be greater than {ComparisonValue}."]);
            validator.RuleFor(request => request.DataFirstRowNumber)
                .GreaterThan(request => request.TitlesRowNumber).WithMessage(_ => localizer["The '{PropertyName}' property with value {PropertyValue} should be greater than '{ComparisonProperty}' with value {ComparisonValue}."]);
            validator.RuleFor(request => request.TitlesRowNumber)
                .GreaterThan(0).WithMessage(_ => localizer["The '{PropertyName}' property value {PropertyValue} should be greater than {ComparisonValue}."]);
            validator.RuleFor(request => request.TitlesFirstColNumber)
                .GreaterThan(0).WithMessage(_ => localizer["The '{PropertyName}' property value {PropertyValue} should be greater than {ComparisonValue}."]);
            validator.RuleFor(request => request.Properties)
                .MustContainOnlyPropertyNamesOfExportableEntity<TEntityId, TEntity, TFilter>(localizer);
        }
    }
}