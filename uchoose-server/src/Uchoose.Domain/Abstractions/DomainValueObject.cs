// ------------------------------------------------------------------------------------------------------
// <copyright file="DomainValueObject.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Uchoose.Domain.Attributes;
using Uchoose.Domain.Contracts;

namespace Uchoose.Domain.Abstractions
{
    /// <summary>
    /// Доменный объект-значение.
    /// </summary>
    // https://github.com/jhewlett/ValueObject
    public abstract class DomainValueObject :
        IValueObject,
        IEquatable<DomainValueObject>
    {
        private List<PropertyInfo>? _properties;
        private List<FieldInfo>? _fields;

        /// <summary>
        /// Оператор равенства.
        /// </summary>
        /// <param name="obj1">Левый операнд.</param>
        /// <param name="obj2">Правый операнд.</param>
        /// <returns>Возвращает true, если операнды равны. Иначе - false.</returns>
        public static bool operator ==(DomainValueObject? obj1, DomainValueObject? obj2)
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
        public static bool operator !=(DomainValueObject? obj1, DomainValueObject? obj2)
        {
            return !(obj1 == obj2);
        }

        /// <inheritdoc/>
        public bool Equals(DomainValueObject? obj)
        {
            return Equals(obj as object);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return GetProperties().All(p => PropertiesAreEqual(obj, p))
                && GetFields().All(f => FieldsAreEqual(obj, f));
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked // allow overflow
            {
                int hash = 17;
                foreach (var prop in GetProperties())
                {
                    object? value = prop.GetValue(this, null);
                    hash = HashValue(hash, value);
                }

                foreach (var field in GetFields())
                {
                    object? value = field.GetValue(this);
                    hash = HashValue(hash, value);
                }

                return hash;
            }
        }

        private static int HashValue(int seed, object? value)
        {
            int currentHash = value?.GetHashCode() ?? 0;
            return (seed * 23) + currentHash;
        }

        /// <summary>
        /// Сравнить свойства на эквивалентность.
        /// </summary>
        /// <param name="obj">Объект для сравнения.</param>
        /// <param name="p"><see cref="PropertyInfo"/>.</param>
        /// <returns>Возвращает true, если свойства эквивалентны. Иначе - false.</returns>
        private bool PropertiesAreEqual(object? obj, PropertyInfo p)
        {
            return Equals(p.GetValue(this, null), p.GetValue(obj, null));
        }

        /// <summary>
        /// Сравнить поля на эквивалентность.
        /// </summary>
        /// <param name="obj">Объект для сравнения.</param>
        /// <param name="f"><see cref="FieldInfo"/>.</param>
        /// <returns>Возвращает true, если поля эквивалентны. Иначе - false.</returns>
        private bool FieldsAreEqual(object? obj, FieldInfo f)
        {
            return Equals(f.GetValue(this), f.GetValue(obj));
        }

        /// <summary>
        /// Получить коллекцию свойств объекта-значения.
        /// </summary>
        /// <returns>Возвращает коллекцию свойств объекта-значения.</returns>
        private IEnumerable<PropertyInfo> GetProperties()
        {
            return _properties ??= GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                .ToList();
        }

        /// <summary>
        /// Получить коллекцию полей объекта-значения.
        /// </summary>
        /// <returns>Возвращает коллекцию полей объекта-значения.</returns>
        private IEnumerable<FieldInfo> GetFields()
        {
            return _fields ??= GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                .ToList();
        }
    }
}