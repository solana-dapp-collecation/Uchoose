// ------------------------------------------------------------------------------------------------------
// <copyright file="QueryableExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Exceptions;
using Uchoose.Utils.Filters.Contracts;
using Uchoose.Utils.Specifications;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Utils.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IQueryable{T}"/>.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Преобразовать к результату операции с пагинацией возвращаемых данных.
        /// </summary>
        /// <typeparam name="T">Тип возвращаемых данных.</typeparam>
        /// <param name="source">Источник данных.</param>
        /// <param name="pageNumber">Текущая страница.</param>
        /// <param name="pageSize">Количество единиц данных на страницу.</param>
        /// <returns>Возвращает <see cref="PaginatedResult{T}"/>.</returns>
        /// <exception cref="CustomException">Возникает, если источник данных равен null.</exception>
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
            where T : class
        {
            if (source == null)
            {
                throw new CustomException(string.Empty);
            }

            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = await source.CountAsync();
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
        }

        /// <summary>
        /// Применить спецификацию к запросу.
        /// </summary>
        /// <typeparam name="TEntity">Тип возвращаемых данных.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="specification"><see cref="ISpecification{TEntity}"/>.</param>
        /// <returns>Возвращает <see cref="IQueryable{T}"/>.</returns>
        public static IQueryable<TEntity> Specify<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class, IEntity
        {
            var queryableResultWithIncludes = specification.Includes
                .Aggregate(query, (current, include) => current.Include(include));

            var secondaryResult = specification.IncludeStrings
                .Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

            return secondaryResult.Where(specification.Criteria);
        }

        /// <summary>
        /// Осуществить фильтрацию сущностей.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности, по которой можно осуществлять поиск.</typeparam>
        /// <param name="query"><see cref="IQueryable{TEntity}"/>.</param>
        /// <param name="search"><see cref="ISearchFilter"/>.</param>
        /// <returns>Возвращает отфильтрованные по <paramref name="search"/> набор сущностей.</returns>
        public static IQueryable<TEntity> Search<TEntity>(this IQueryable<TEntity> query, ISearchFilter search)
            where TEntity : class, IEntity
        {
            var predicate = SearchSpecification<TEntity>.SearchPredicate(search);
            return query.Where(predicate);
        }
    }
}