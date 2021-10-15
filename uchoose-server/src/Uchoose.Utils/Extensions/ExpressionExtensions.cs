// ------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Utils.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="Expression"/>.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Получить предикат True.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <returns>Возвращает предикат True.</returns>
        public static Expression<Func<TEntity, bool>> True<TEntity>()
            where TEntity : class, IEntity
        {
            return _ => true;
        }

        /// <summary>
        /// Получить предикат False.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <returns>Возвращает предикат False.</returns>
        public static Expression<Func<TEntity, bool>> False<TEntity>()
            where TEntity : class, IEntity
        {
            return _ => false;
        }

        /// <summary>
        /// Добавить критерий с логическим "И".
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="left">Исходный критерий.</param>
        /// <param name="right">Добавляемый критерий.</param>
        /// <returns>Возвращает скомбинированный с помощью логического "И" критерий.</returns>
        public static Expression<Func<TEntity, bool>> And<TEntity>(this Expression<Func<TEntity, bool>> left, Expression<Func<TEntity, bool>> right)
            where TEntity : class, IEntity
        {
            var p = left.Parameters[0];
            var visitor = new SubstExpressionVisitor
            {
                Subst = { [right.Parameters[0]] = p }
            };

            Expression body = Expression.AndAlso(left.Body, visitor.Visit(right.Body));
            return Expression.Lambda<Func<TEntity, bool>>(body, p);
        }

        /// <summary>
        /// Добавить критерий с логическим "ИЛИ".
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="left">Исходный критерий.</param>
        /// <param name="right">Добавляемый критерий.</param>
        /// <returns>Возвращает скомбинированный с помощью логического "ИЛИ" критерий.</returns>
        public static Expression<Func<TEntity, bool>> Or<TEntity>(this Expression<Func<TEntity, bool>> left, Expression<Func<TEntity, bool>> right)
            where TEntity : class, IEntity
        {
            var p = left.Parameters[0];
            var visitor = new SubstExpressionVisitor
            {
                Subst = { [right.Parameters[0]] = p }
            };

            Expression body = Expression.OrElse(left.Body, visitor.Visit(right.Body));
            return Expression.Lambda<Func<TEntity, bool>>(body, p);
        }

        /// <summary>
        /// Добавить критерий с логическим "НЕ".
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="criteria">Исходный критерий.</param>
        /// <returns>Возвращает скомбинированный с помощью логического "ИЛИ" критерий.</returns>
        public static Expression<Func<TEntity, bool>> Not<TEntity>(this Expression<Func<TEntity, bool>> criteria)
            where TEntity : class, IEntity
        {
            var p = criteria.Parameters[0];
            Expression body = Expression.Not(criteria.Body);
            return Expression.Lambda<Func<TEntity, bool>>(body, p);
        }

        /// <summary>
        /// Посетитель для определения операции.
        /// </summary>
        internal class SubstExpressionVisitor : ExpressionVisitor
        {
            /// <summary>
            /// Соответствие.
            /// </summary>
            public Dictionary<Expression, Expression> Subst { get; set; } = new();

            /// <summary>
            /// Посетить параметр.
            /// </summary>
            /// <param name="node"><see cref="ParameterExpression"/>.</param>
            /// <returns>Возвращает <see cref="Expression"/>.</returns>
            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (Subst.TryGetValue(node, out var newValue))
                {
                    return newValue;
                }

                return node;
            }
        }
    }
}