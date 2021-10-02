// ------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

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
    }
}