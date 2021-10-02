// ------------------------------------------------------------------------------------------------------
// <copyright file="IPaginationFilterValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using FluentValidation;
using Microsoft.Extensions.Localization;
using Uchoose.Utils.Attributes.Searching;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Extensions;

namespace Uchoose.Utils.Filters.Validators
{
    /// <summary>
    /// Валидатор фильтра сущности.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности, по которой можно осуществлять поиск.</typeparam>
    /// <typeparam name="TFilter">Типа фильтра для сущности.</typeparam>
    public interface IPaginationFilterValidator<TEntityId, TEntity, TFilter>
        where TEntity : class, IEntity<TEntityId>, ISearchable<TEntityId, TEntity>
        where TFilter : PaginationFilter
    {
        /// <summary>
        /// Использовать правила валидации для фильтра.
        /// </summary>
        /// <param name="validator"><see cref="AbstractValidator{T}"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        static void UseRules(AbstractValidator<TFilter> validator, IStringLocalizer localizer)
        {
            validator.RuleFor(request => request)
                .Must(_ => typeof(TEntity).GetCustomAttribute(typeof(NotSearchableAttribute)) == null).WithMessage(_ => string.Format(localizer["The '{0}' entity must be searchable."], typeof(TEntity).GetGenericTypeName()));

            validator.RuleFor(request => request.PageNumber)
                .GreaterThan(0).WithMessage(localizer["The '{PropertyName}' property must be greater than 0."]);
            validator.RuleFor(request => request.PageSize)
                .GreaterThan(0).WithMessage(localizer["The '{PropertyName}' property must be greater than 0."]);
            validator.RuleFor(request => request.OrderBy)
                .MustContainCorrectOrderingsFor(typeof(TEntity), localizer);

            validator.When(request => request.Search?.Fields.Count > 0 && request.Search.Keyword.IsPresent(), () =>
            {
                validator.RuleFor(request => request.Search.Fields)
                    .MustContainOnlyPropertyNamesOfSearchableEntity<TEntityId, TEntity, TFilter>(localizer);
            });
        }
    }
}