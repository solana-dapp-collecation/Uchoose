// ------------------------------------------------------------------------------------------------------
// <copyright file="SearchSpecification.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using AutoMapper.Internal;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Extensions;
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
            Criteria = SearchPredicate(searchFilter);
        }

        /// <summary>
        /// <see cref="ISearchFilter"/>.
        /// </summary>
        public ISearchFilter SearchFilter { get; }

        /// <summary>
        /// Получить предикат для поиска в наборе сущностей по фильтру <paramref name="search"/>.
        /// </summary>
        /// <param name="search"><see cref="ISearchFilter"/>.</param>
        /// <returns>Возвращает предикат для поиска в наборе сущностей по фильтру <paramref name="search"/>.</returns>
        public static Expression<Func<TEntity, bool>> SearchPredicate(ISearchFilter search)
        {
            var predicate = ExpressionExtensions.True<TEntity>();
            if (search?.Fields.Count > 0 && search.Keyword.IsPresent() && typeof(TEntity).GetGenericInterface(typeof(ISearchable<,>)) != null)
            {
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
            }

            return predicate;
        }
    }
}