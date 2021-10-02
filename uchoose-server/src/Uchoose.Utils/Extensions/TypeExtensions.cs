// ------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Uchoose.Utils.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        #region GetGenericTypeName

        /// <summary>
        /// Получить имя generic типа.
        /// </summary>
        /// <param name="type">Тип.</param>
        /// <returns>Возвращает имя generic типа.</returns>
        public static string GetGenericTypeName(this Type type)
        {
            if (type.IsGenericType)
            {
                string genericTypes = string.Join(",", type.GetGenericArguments().Select(GetGenericTypeName).ToArray());
                return $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }

            return type.Name;
        }

        #endregion GetGenericTypeName

        #region GetAllPublicConstantValues

        /// <summary>
        /// Получить список значений публичных констант типа.
        /// </summary>
        /// <typeparam name="T">Тип значений констант.</typeparam>
        /// <param name="type">Тип.</param>
        /// <returns>Возвращает список значений публичных констант типа.</returns>
        public static List<T> GetAllPublicConstantValues<T>(this Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                .Select(x => (T)x.GetRawConstantValue())
                .ToList();
        }

        #endregion GetAllPublicConstantValues

        #region GetNestedClassesStaticStringValues

        /// <summary>
        /// Получить список строковых значений публичных статических членов во вложенных классах типа.
        /// </summary>
        /// <param name="type">Тип.</param>
        /// <returns>Возвращает список строковых значений публичных статических членов во вложенных классах типа.</returns>
        public static List<string> GetNestedClassesStaticStringValues(this Type type)
        {
            var values = new List<string>();
            foreach (var prop in type.GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                object propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                {
                    values.Add(propertyValue.ToString());
                }
            }

            return values;
        }

        #endregion GetNestedClassesStaticStringValues
    }
}