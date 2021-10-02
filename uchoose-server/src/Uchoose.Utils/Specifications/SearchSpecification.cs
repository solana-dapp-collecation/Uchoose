// ------------------------------------------------------------------------------------------------------
// <copyright file="SearchSpecification.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Filters.Contracts;

namespace Uchoose.Utils.Specifications
{
    /// <summary>
    /// Спецификация для поиска.
    /// </summary>
    /// <typeparam name="TEntity">Тип специфицируемой сущности.</typeparam>
    public sealed class SearchSpecification<TEntity> :
        UchooseSpecification<TEntity>
            where TEntity : class, IEntity
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="SearchSpecification{TEntity}"/>.
        /// </summary>
        /// <param name="searchFilter"><see cref="ISearchFilter"/>.</param>
        public SearchSpecification(ISearchFilter searchFilter)
        {
            SearchFilter = searchFilter;
            Criteria = _ => true;
        }

        /// <summary>
        /// <see cref="ISearchFilter"/>.
        /// </summary>
        public ISearchFilter SearchFilter { get; }
    }
}