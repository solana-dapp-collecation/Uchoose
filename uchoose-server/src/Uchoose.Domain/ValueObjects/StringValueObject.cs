// ------------------------------------------------------------------------------------------------------
// <copyright file="StringValueObject.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Domain.Abstractions;

namespace Uchoose.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение для хранения <see cref="string"/> значения.
    /// </summary>
    public class StringValueObject :
        ValueObject<StringValueObject, string>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="StringValueObject"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        public StringValueObject(string value)
            : base(value)
        {
        }
    }
}