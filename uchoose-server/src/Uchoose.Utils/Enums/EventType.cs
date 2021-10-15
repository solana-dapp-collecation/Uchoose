// ------------------------------------------------------------------------------------------------------
// <copyright file="EventType.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Utils.Enums
{
    /// <summary>
    /// Тип события.
    /// </summary>
    public enum EventType : byte
    {
        /// <summary>
        /// Неизвестное событие.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Доменное событие.
        /// </summary>
        Domain = 1,

        /// <summary>
        /// Событие приложения.
        /// </summary>
        Application = 2
    }
}