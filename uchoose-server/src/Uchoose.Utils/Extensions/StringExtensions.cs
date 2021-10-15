// ------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Text;
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

        /// <summary>
        /// Получить строку в указанной кодировке из base64 строки.
        /// </summary>
        /// <param name="str">Base64 строка.</param>
        /// <param name="enc"><see cref="Encoding"/>.</param>
        /// <returns>Возвращает строку в указанной кодировке из base64 строки.</returns>
        public static string FromBase64ToString(this string str, Encoding enc = null)
        {
            return (enc ?? Encoding.Default).GetString(FromBase64(str));
        }

        /// <summary>
        /// Получить массив байтов из base64 строки.
        /// </summary>
        /// <param name="str">Base64 строка.</param>
        /// <returns>Возвращает массив байтов из base64 строки.</returns>
        public static byte[] FromBase64(this string str)
        {
            return Convert.FromBase64String(str);
        }

        /// <summary>
        /// Получить base64 строку из строки в указанной кодировке.
        /// </summary>
        /// <param name="str">Исходная строка.</param>
        /// <param name="enc"><see cref="Encoding"/>.</param>
        /// <returns>Возвращает base64 строку из строки в указанной кодировке.</returns>
        public static string ToBase64(this string str, Encoding enc = null)
        {
            return ToBase64((enc ?? Encoding.Default).GetBytes(str));
        }

        /// <summary>
        /// Получить base64 строку из массива байтов.
        /// </summary>
        /// <param name="data">Массив байтов.</param>
        /// <returns>Возвращает base64 строку из массива байтов.</returns>
        public static string ToBase64(this byte[] data)
        {
            return Convert.ToBase64String(data);
        }
    }
}