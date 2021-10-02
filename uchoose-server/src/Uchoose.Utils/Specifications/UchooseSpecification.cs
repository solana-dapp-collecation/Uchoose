// ------------------------------------------------------------------------------------------------------
// <copyright file="UchooseSpecification.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Extensions;

namespace Uchoose.Utils.Specifications
{
    /// <inheritdoc cref="ISpecification{TEntity}"/>
    /// <typeparam name="TEntity">Тип специфицируемой сущности.</typeparam>
    public abstract class UchooseSpecification<TEntity>
        : ISpecification<TEntity>
            where TEntity : class, IEntity
    {
        /// <inheritdoc/>
        public Expression<Func<TEntity, bool>> Criteria { get; set; }

        /// <inheritdoc/>
        public List<Expression<Func<TEntity, object>>> Includes { get; } = new();

        /// <inheritdoc/>
        public List<string> IncludeStrings { get; } = new();

        /// <inheritdoc/>
        public Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> criteria)
        {
            return Criteria = Criteria == null ? criteria : Criteria.And(criteria);
        }

        /// <inheritdoc/>
        public Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> criteria)
        {
            return Criteria = Criteria == null ? criteria : Criteria.Or(criteria);
        }

        /// <inheritdoc/>
        public Expression<Func<TEntity, bool>> Not()
        {
            return Criteria = Criteria?.Not();
        }

        /// <inheritdoc/>
        public bool IsSatisfiedBy(TEntity entity)
        {
            var predicate = Criteria.Compile();
            return predicate(entity);
        }

        /// <summary>
        /// Добавить включенную сущность.
        /// </summary>
        /// <param name="includeExpression">Выражение для добавления сущности.</param>
        protected virtual void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        /// <summary>
        /// Добавить включенную строку.
        /// </summary>
        /// <param name="includeString">Выражение для добавления строки.</param>
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}