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
    public class UchooseSpecification<TEntity>
        : ISpecification<TEntity>
            where TEntity : class, IEntity
    {
        /// <inheritdoc/>
        public Expression<Func<TEntity, bool>> Criteria { get; protected set; }

        /// <inheritdoc/>
        public List<Expression<Func<TEntity, object>>> Includes { get; } = new();

        /// <inheritdoc/>
        public List<string> IncludeStrings { get; } = new();

        /// <summary>
        /// Выполняет неявное преобразование из <see cref="UchooseSpecification{TEntity}"/>
        /// в LINQ выражение (критерий).
        /// </summary>
        /// <param name="spec"><see cref="UchooseSpecification{TEntity}"/>.</param>
        public static implicit operator Expression<Func<TEntity, bool>>(UchooseSpecification<TEntity> spec)
            => spec?.Criteria ?? throw new ArgumentNullException(nameof(spec));

        /// <summary>
        /// Выполняет неявное преобразование из <see cref="UchooseSpecification{TEntity}"/>
        /// в функцию для проверки удовлетворения сущностью критерия.
        /// </summary>
        /// <param name="spec"><see cref="UchooseSpecification{TEntity}"/>.</param>
        public static implicit operator Func<TEntity, bool>(UchooseSpecification<TEntity> spec)
            => spec.IsSatisfiedBy;

        /// <summary>
        /// Оператор false.
        /// </summary>
        /// <param name="_"><see cref="UchooseSpecification{TEntity}"/>.</param>
        /// <returns>Возвращает false.</returns>
        public static bool operator false(UchooseSpecification<TEntity> _) => false;

        /// <summary>
        /// Оператор true.
        /// </summary>
        /// <param name="_"><see cref="UchooseSpecification{TEntity}"/>.</param>
        /// <returns>Возвращает true.</returns>
        public static bool operator true(UchooseSpecification<TEntity> _) => true;

        /// <summary>
        /// Оператор логического "И".
        /// </summary>
        /// <param name="spec1"><see cref="UchooseSpecification{TEntity}"/>.</param>
        /// <param name="spec2"><see cref="UchooseSpecification{TEntity}"/>.</param>
        /// <returns>Возвращает спецификацию с критерием, полученным в результате применения к критериям операндов операции логического "И".</returns>
        public static UchooseSpecification<TEntity> operator &(UchooseSpecification<TEntity> spec1, UchooseSpecification<TEntity> spec2)
            => new() { Criteria = spec1.And(spec2.Criteria) };

        /// <summary>
        /// Оператор логического "ИЛИ".
        /// </summary>
        /// <param name="spec1"><see cref="UchooseSpecification{TEntity}"/>.</param>
        /// <param name="spec2"><see cref="UchooseSpecification{TEntity}"/>.</param>
        /// <returns>Возвращает спецификацию с критерием, полученным в результате применения к критериям операндов операции логического "ИЛИ".</returns>
        public static UchooseSpecification<TEntity> operator |(UchooseSpecification<TEntity> spec1, UchooseSpecification<TEntity> spec2)
            => new() { Criteria = spec1.Or(spec2.Criteria) };

        /// <summary>
        /// Оператор логического "НЕ".
        /// </summary>
        /// <param name="spec"><see cref="UchooseSpecification{TEntity}"/>.</param>
        /// <returns>Возвращает спецификацию с критерием, полученным в результате применения к критерию операнда операции логического "НЕ".</returns>
        public static UchooseSpecification<TEntity> operator !(UchooseSpecification<TEntity> spec)
            => new() { Criteria = spec.Not() };

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