// ------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace Uchoose.Utils.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly Regex WhitespaceRegex = new(@"\s+");

        /// <summary>
        /// Заменить пробелы.
        /// </summary>
        /// <param name="input">Исходная строка.</param>
        /// <param name="replacement">Строка для замены.</param>
        /// <returns>Возвращает строку с заменёнными на заданный текст пробелами.</returns>
        public static string ReplaceWhitespace(this string input, string replacement)
        {
            return WhitespaceRegex.Replace(input, replacement);
        }

        /// <summary>
        /// Проверить, что строка не является пустой или null.
        /// </summary>
        /// <param name="value">Строковое значение.</param>
        /// <returns>Возвращает true, если строка не является пустой или null. Иначе - false.</returns>
        public static bool IsPresent(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}