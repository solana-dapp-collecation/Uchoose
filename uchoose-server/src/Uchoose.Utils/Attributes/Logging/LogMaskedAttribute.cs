// ------------------------------------------------------------------------------------------------------
// <copyright file="LogMaskedAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;

// ReSharper disable ReplaceSubstringWithRangeIndexer
namespace Uchoose.Utils.Attributes.Logging
{
    /// <summary>
    /// Атрибут, указывающий, что свойство должно логироваться в маскированном виде.
    /// </summary>
    /// <remarks>
    /// Используется в посреднике для логирования запросов при вызове их обработчиков.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class LogMaskedAttribute : Attribute
    {
        /// <summary>
        /// Символ для маскирования по умолчанию.
        /// </summary>
        public const char DefaultMaskChar = '*';

        /// <summary>
        /// Инициализирует экземпляр <see cref="LogMaskedAttribute"/>.
        /// </summary>
        /// <param name="maskChar">Символ для маскирования.</param>
        /// <param name="showFirst">Показать первые n символов.</param>
        /// <param name="showLast">Показать последние n символов.</param>
        /// <param name="preserveLength">Сохранить длину исходного значения.</param>
        public LogMaskedAttribute(
            char maskChar = DefaultMaskChar,
            int showFirst = 0,
            int showLast = 0,
            bool preserveLength = false)
        {
            MaskChar = maskChar;
            ShowFirst = showFirst;
            ShowLast = showLast;
            PreserveLength = preserveLength;
        }

        /// <summary>
        /// Символ для маскирования.
        /// </summary>
        public char MaskChar { get; }

        /// <summary>
        /// Показать первые n символов.
        /// </summary>
        public int ShowFirst { get; set; }

        /// <summary>
        /// Показать последние n символов.
        /// </summary>
        public int ShowLast { get; set; }

        /// <summary>
        /// Сохранить длину исходного значения.
        /// </summary>
        public bool PreserveLength { get; set; }

        /// <summary>
        /// Маскировать значение свойства.
        /// </summary>
        /// <param name="propValue">Значение свойства.</param>
        /// <returns>Возвращает маскированное значение свойства.</returns>
        public object? MaskValue(object? propValue)
        {
            string? val = propValue as string;

            if (string.IsNullOrWhiteSpace(val))
            {
                return val;
            }

            if (ShowFirst == 0 && ShowLast == 0)
            {
                if (PreserveLength)
                {
                    return new string(MaskChar, val.Length);
                }

                return new string(MaskChar, 3);
            }

            if (ShowFirst > 0 && ShowLast == 0)
            {
                string first = val.Substring(0, Math.Min(ShowFirst, val.Length));

                if (!PreserveLength || !IsDefaultMask())
                {
                    return first + new string(MaskChar, 3);
                }

                string mask = string.Empty;
                if (ShowFirst <= val.Length)
                {
                    mask = new string(MaskChar, val.Length - ShowFirst);
                }

                return first + mask;
            }

            if (ShowFirst == 0 && ShowLast > 0)
            {
#pragma warning disable IDE0057 // Use range operator
                string last = ShowLast > val.Length ? val : val.Substring(val.Length - ShowLast);
#pragma warning restore IDE0057 // Use range operator

                if (!PreserveLength || !IsDefaultMask())
                {
                    return new string(MaskChar, 3) + last;
                }

                string mask = string.Empty;
                if (ShowLast <= val.Length)
                {
                    mask = new string(MaskChar, val.Length - ShowLast);
                }

                return mask + last;
            }

            if (ShowFirst > 0 && ShowLast > 0)
            {
                if (ShowFirst + ShowLast >= val.Length)
                {
                    return val;
                }

                string first = val.Substring(0, ShowFirst);
#pragma warning disable IDE0057 // Use range operator
                string last = val.Substring(val.Length - ShowLast);
#pragma warning restore IDE0057 // Use range operator

                if (!PreserveLength || !IsDefaultMask())
                {
                    return first + new string(MaskChar, 3) + last;
                }

                return first + new string(MaskChar, val.Length - ShowLast - ShowFirst) + last;
            }

            return propValue;
        }

        /// <summary>
        /// Проверить, используется ли символ для маскирования по умолчанию.
        /// If true PreserveLength is ignored.
        /// </summary>
        /// <remarks>
        /// Если true, то <see cref="PreserveLength"/> не применяется.
        /// </remarks>
        /// <returns>Возвращает true, если используется символ для маскирования по умолчанию. Иначе - false.</returns>
        internal bool IsDefaultMask()
        {
            return MaskChar == DefaultMaskChar;
        }
    }
}