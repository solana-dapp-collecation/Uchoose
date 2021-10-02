// ------------------------------------------------------------------------------------------------------
// <copyright file="QueryableExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Searching;
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

            if (specification is SearchSpecification<TEntity> { SearchFilter: { } } searchSpecification)
            {
                secondaryResult = secondaryResult.Search(searchSpecification.SearchFilter);
            }

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
            if (search?.Fields.Count > 0 && search.Keyword.IsPresent() && typeof(TEntity).GetGenericInterface(typeof(ISearchable<,>)) != null)
            {
                var predicate = ExpressionExtensions.True<TEntity>();
                var properties = typeof(TEntity)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => search.Fields
                        .Any(field => string.Equals(p.Name, field, StringComparison.CurrentCultureIgnoreCase)));

                foreach (var propertyInfo in properties)
                {
                    var parameter = Expression.Parameter(typeof(TEntity), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var propertyAsObject = Expression.Convert(property, typeof(object));
                    var nullCheck = Expression.NotEqual(propertyAsObject, Expression.Constant(null, typeof(object)));
                    var propertyAsString = Expression.Call(property, nameof(ToString), null, null);
                    var keywordExpression = Expression.Constant(search.Keyword);
                    var contains = propertyInfo.PropertyType == typeof(string)
                        ? Expression.Call(property, nameof(string.Contains), null, keywordExpression)
                        : Expression.Call(propertyAsString, nameof(string.Contains), null, keywordExpression);
                    var lambda = Expression.Lambda(Expression.AndAlso(nullCheck, contains), parameter);

                    predicate = predicate.And((Expression<Func<TEntity, bool>>)lambda);
                }

                return query.Where(predicate);
            }

            return query;
        }
    }
}