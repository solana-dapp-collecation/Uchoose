// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributePaginationFilterValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using FluentValidation;
using Microsoft.Extensions.Localization;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Filters.Validators;

namespace Uchoose.Domain.Filters.Validators
{
    /// <summary>
    /// Валидатор фильтра расширенного атрибута сущности.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности, по которой можно осуществлять поиск.</typeparam>
    public abstract class ExtendedAttributePaginationFilterValidator<TEntityId, TEntity> :
        AbstractValidator<ExtendedAttributePaginationFilter<TEntityId, TEntity>>,
        IPaginationFilterValidator<TEntityId, TEntity, ExtendedAttributePaginationFilter<TEntityId, TEntity>>
            where TEntity : class, IEntity<TEntityId>, ISearchable<TEntityId, TEntity>
    {
        /// <summary>
        /// Инициализирует экземпляр класса, наследующего <see cref="ExtendedAttributePaginationFilterValidator{TEntityId, TEntity}"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        protected ExtendedAttributePaginationFilterValidator(IStringLocalizer localizer)
        {
            IPaginationFilterValidator<TEntityId, TEntity, ExtendedAttributePaginationFilter<TEntityId, TEntity>>.UseRules(this, localizer);
        }
    }
}