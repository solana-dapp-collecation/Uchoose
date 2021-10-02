// ------------------------------------------------------------------------------------------------------
// <copyright file="TimespanJsonConverter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Uchoose.SerializationService.Interfaces.Converters
{
    /// <summary>
    /// Конвертер для <see cref="TimeSpan"/>.
    /// </summary>
    /// <remarks>
    /// System.Text.Json на текущий момент не поддерживает <see cref="TimeSpan"/>.
    /// </remarks>
    // https://github.com/dotnet/corefx/issues/38641.
    public class TimespanJsonConverter : JsonConverter<TimeSpan>
    {
        /// <summary>
        /// Формат: Дни.Часы:Минуты:Секунды:Миллисекунды.
        /// </summary>
        public const string TimeSpanFormatString = @"d\.hh\:mm\:ss\:FFF";

        /// <summary>
        /// Прочитать значение.
        /// </summary>
        /// <param name="reader"><see cref="Utf8JsonReader"/>.</param>
        /// <param name="typeToConvert">Тип для конвертации.</param>
        /// <param name="options"><see cref="JsonSerializerOptions"/>.</param>
        /// <returns>Возвращает <see cref="TimeSpan"/>.</returns>
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? s = reader.GetString();
            if (string.IsNullOrWhiteSpace(s))
            {
                return TimeSpan.Zero;
            }

            if (!TimeSpan.TryParseExact(s, TimeSpanFormatString, null, out var parsedTimeSpan))
            {
                throw new FormatException($"Input timespan is not in an expected format : expected {Regex.Unescape(TimeSpanFormatString)}. Please retrieve this key as a string and parse manually.");
            }

            return parsedTimeSpan;
        }

        /// <summary>
        /// Записать значение.
        /// </summary>
        /// <param name="writer"><see cref="Utf8JsonWriter"/>.</param>
        /// <param name="value">Значение TimeSpan.</param>
        /// <param name="options"><see cref="JsonSerializerOptions"/>.</param>
        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            string timespanFormatted = $"{value.ToString(TimeSpanFormatString)}";
            writer.WriteStringValue(timespanFormatted);
        }
    }
}