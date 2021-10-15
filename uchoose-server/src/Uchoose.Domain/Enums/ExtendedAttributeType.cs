// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeType.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Domain.Enums
{
    /// <summary>
    /// Тип расширенного атрибута.
    /// </summary>
    public enum ExtendedAttributeType : byte
    {
        /// <summary>
        /// Значение с плавающей точкой.
        /// </summary>
        Decimal = 1,

        /// <summary>
        /// Текстовое значение.
        /// </summary>
        Text = 2,

        /// <summary>
        /// Значение даты и времени.
        /// </summary>
        DateTime = 3,

        /// <summary>
        /// Значение в виде json строки.
        /// </summary>
        Json = 4,

        /// <summary>
        /// Логическое значение.
        /// </summary>
        Boolean = 5,

        /// <summary>
        /// Целочисленное значение.
        /// </summary>
        Integer = 6
    }
}