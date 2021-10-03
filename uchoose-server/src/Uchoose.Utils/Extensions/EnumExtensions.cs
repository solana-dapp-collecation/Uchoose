// ------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Uchoose.Utils.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="Enum"/>.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Конвертировать значение этого экземпляра к строке, хранимой в атрибуте <see cref="DescriptionAttribute"/>.
        /// </summary>
        /// <param name="enumValue">Экземпляр перечисления.</param>
        /// <returns>Возвращает текст из <see cref="DescriptionAttribute"/>.</returns>
        public static string ToDescriptionString(this Enum enumValue)
        {
            if (enumValue == null)
            {
                return string.Empty;
            }

            var attributes = (DescriptionAttribute[])enumValue.GetType()
                .GetField(enumValue.ToString())?
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes?.Length > 0)
            {
                return attributes[0].Description;
            }

            string result = enumValue.ToString();
            result = Regex.Replace(result, "([a-z])([A-Z])", "$1 $2");
            result = Regex.Replace(result, "([A-Za-z])([0-9])", "$1 $2");
            result = Regex.Replace(result, "([0-9])([A-Za-z])", "$1 $2");
            result = Regex.Replace(result, "(?<!^)(?<! )([A-Z][a-z])", " $1");

            return result;
        }

        /// <summary>
        /// Получить список строк, хранимых в атрибуте <see cref="DescriptionAttribute"/> и разделённых <paramref name="separator"/>.
        /// </summary>
        /// <param name="enumValue">Экземпляр перечисления.</param>
        /// <param name="separator">Разделитель.</param>
        /// <returns>Возвращает список строк, хранимых в атрибуте <see cref="DescriptionAttribute"/> и разделённых <paramref name="separator"/>.</returns>
        public static List<string> GetDescriptionList(this Enum enumValue, char separator = ',')
        {
            string result = enumValue.ToDescriptionString();
            return result.Split(separator).ToList();
        }
    }
}