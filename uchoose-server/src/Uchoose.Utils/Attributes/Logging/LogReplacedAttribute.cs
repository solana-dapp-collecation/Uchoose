// ------------------------------------------------------------------------------------------------------
// <copyright file="LogReplacedAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Text.RegularExpressions;

namespace Uchoose.Utils.Attributes.Logging
{
    /// <summary>
    /// Атрибут, указывающий, что свойство должно логироваться с заменой значения.
    /// </summary>
    /// <remarks>
    /// Используется в посреднике для логирования запросов при вызове их обработчиков.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class LogReplacedAttribute : Attribute
    {
        private readonly string _pattern;
        private readonly string _replacement;

        /// <summary>
        /// Инициализирует экземпляр <see cref="LogReplacedAttribute"/>.
        /// </summary>
        /// <param name="pattern">Паттерн значения для замены.</param>
        /// <param name="replacement">На что заменить.</param>
        public LogReplacedAttribute(string pattern, string replacement)
        {
            _pattern = pattern;
            _replacement = replacement;
        }

        /// <summary>
        /// <see cref="RegexOptions"/>.
        /// </summary>
        public RegexOptions Options { get; set; }

        /// <summary>
        /// Заменить значение свойства.
        /// </summary>
        /// <param name="propValue">Значение свойства.</param>
        /// <returns>Возвращает заменённое значение свойства.</returns>
        public object? ReplaceValue(object? propValue)
        {
            string? val = propValue as string;

            if (string.IsNullOrWhiteSpace(val))
            {
                return val;
            }

            return Regex.Replace(val, _pattern, _replacement, Options);
        }
    }
}