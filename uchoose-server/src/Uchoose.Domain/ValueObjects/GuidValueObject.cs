// ------------------------------------------------------------------------------------------------------
// <copyright file="GuidValueObject.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Domain.Abstractions;

namespace Uchoose.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение для хранения <see cref="Guid"/> значения.
    /// </summary>
    public class GuidValueObject :
        ValueObject<GuidValueObject, Guid>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="GuidValueObject"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        public GuidValueObject(Guid value)
            : base(value)
        {
        }
    }
}