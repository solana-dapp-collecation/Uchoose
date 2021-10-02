// ------------------------------------------------------------------------------------------------------
// <copyright file="PaginationFilterValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using FluentValidation;
using Microsoft.Extensions.Localization;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Searching;

namespace Uchoose.Utils.Filters.Validators
{
    /// <inheritdoc cref="IPaginationFilterValidator{TEntityId, TEntity, TFilter}"/>
    public abstract class PaginationFilterValidator<TEntityId, TEntity, TFilter> :
        AbstractValidator<TFilter>,
        IPaginationFilterValidator<TEntityId, TEntity, TFilter>
        where TEntity : class, IEntity<TEntityId>, ISearchable<TEntityId, TEntity>
        where TFilter : PaginationFilter
    {
        /// <summary>
        /// Инициализирует экземпляр класса, наследующего <see cref="PaginationFilterValidator{TEntityId, TEntity, TFilter}"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        protected PaginationFilterValidator(IStringLocalizer localizer)
        {
            IPaginationFilterValidator<TEntityId, TEntity, TFilter>.UseRules(this, localizer);
        }
    }
}