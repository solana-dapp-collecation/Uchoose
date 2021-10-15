// ------------------------------------------------------------------------------------------------------
// <copyright file="ValueObject.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Domain.Contracts;
using Uchoose.Domain.Exceptions;

namespace Uchoose.Domain.Abstractions
{
    /// <inheritdoc cref="ValueObject{TValueObject}"/>
    /// <typeparam name="TValueObject">Тип объекта-значения.</typeparam>
    /// <typeparam name="TValue">Тип значения объекта-значения.</typeparam>
    public abstract class ValueObject<TValueObject, TValue> :
        ValueObject<TValueObject>,
        IValueObject<TValueObject, TValue>
            where TValueObject : class, IValueObject<TValueObject, TValue>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="ValueObject{TValueObject, TValue}"/>.
        /// </summary>
        protected ValueObject()
        {
            // overload so ValueObjects can be used as EF properties
        }

        /// <summary>
        /// Инициализирует экземпляр <see cref="ValueObject{TValueObject, TValue}"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        protected ValueObject(TValue value)
        {
            Value = value;
        }

        /// <inheritdoc cref="IValueObject{TValueObject, TValue}.Value"/>
        public TValue Value { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <inheritdoc/>
        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        /// <inheritdoc/>
        protected override bool EqualsCore(TValueObject other)
        {
            return Value.Equals(other.Value);
        }
    }

    /// <inheritdoc cref="IValueObject"/>
    /// <typeparam name="TValueObject">Тип объекта-значения.</typeparam>
    public abstract class ValueObject<TValueObject> :
        IValueObject<TValueObject>
            where TValueObject : class, IValueObject<TValueObject>
    {
        private int? _cachedHashCode;

        /// <summary>
        /// Оператор равенства.
        /// </summary>
        /// <param name="obj1">Левый операнд.</param>
        /// <param name="obj2">Правый операнд.</param>
        /// <returns>Возвращает true, если операнды равны. Иначе - false.</returns>
        public static bool operator ==(ValueObject<TValueObject> obj1, ValueObject<TValueObject> obj2)
        {
            if (obj1 is null && obj2 is null)
            {
                return true;
            }

            if (obj1 is null || obj2 is null)
            {
                return false;
            }

            return obj1.Equals(obj2);
        }

        /// <summary>
        /// Оператор неравенства.
        /// </summary>
        /// <param name="obj1">Левый операнд.</param>
        /// <param name="obj2">Правый операнд.</param>
        /// <returns>Возвращает true, если операнды не равны. Иначе - false.</returns>
        public static bool operator !=(ValueObject<TValueObject> obj1, ValueObject<TValueObject> obj2)
        {
            return !(obj1 == obj2);
        }

        /// <inheritdoc/>
        public bool Equals(TValueObject other)
        {
            return EqualsCore(other);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not TValueObject valueObject || GetUnproxiedType(this) != GetUnproxiedType(obj))
            {
                return false;
            }

            return EqualsCore(valueObject);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _cachedHashCode ??= GetHashCodeCore();
        }

        /// <inheritdoc cref="Equals(TValueObject)"/>
        protected abstract bool EqualsCore(TValueObject other);

        /// <summary>
        /// <see cref="GetHashCode"/>.
        /// </summary>
        protected abstract int GetHashCodeCore();

        /// <summary>
        /// Проверить бизнес-правило.
        /// </summary>
        /// <param name="rule">Бизнес-правило.</param>
        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }

        private static Type GetUnproxiedType(object obj)
        {
            const string efCoreProxyNamespace = "Castle.Proxies";

            var type = obj.GetType();
            if (type.Namespace?.Equals(efCoreProxyNamespace) == true)
            {
                return type.BaseType;
            }

            return type;
        }
    }
}