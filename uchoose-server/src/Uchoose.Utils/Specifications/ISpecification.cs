// ------------------------------------------------------------------------------------------------------
// <copyright file="ISpecification.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Utils.Specifications
{
    /// <summary>
    /// Спецификация.
    /// </summary>
    /// <typeparam name="TEntity">Тип проверяемых сущностей.</typeparam>
    public interface ISpecification<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Критерий для проверки сущностей.
        /// </summary>
        Expression<Func<TEntity, bool>> Criteria { get; }

        /// <summary>
        /// Включённые сущности.
        /// </summary>
        List<Expression<Func<TEntity, object>>> Includes { get; }

        /// <summary>
        /// Включённые строки.
        /// </summary>
        List<string> IncludeStrings { get; }

        /// <summary>
        /// Добавить критерий с логическим "И".
        /// </summary>
        /// <param name="criteria">Критерий.</param>
        /// <returns>Возвращает новый критерий.</returns>
        Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Добавить критерий с логическим "ИЛИ".
        /// </summary>
        /// <param name="criteria">Критерий.</param>
        /// <returns>Возвращает новый критерий.</returns>
        Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Добавить критерий с логическим "НЕ".
        /// </summary>
        /// <returns>Возвращает новый критерий.</returns>
        Expression<Func<TEntity, bool>> Not();

        /// <summary>
        /// Проверить, удовлетворяет ли сущность критерию.
        /// </summary>
        /// <param name="entity">Проверяемая сущность.</param>
        /// <returns>Возвращает true, если сущность соответствует критерию. Иначе - false.</returns>
        bool IsSatisfiedBy(TEntity entity);
    }
}