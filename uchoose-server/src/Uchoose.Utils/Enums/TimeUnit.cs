// ------------------------------------------------------------------------------------------------------
// <copyright file="TimeUnit.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace Uchoose.Utils.Enums
{
    /// <summary>
    /// Единица времени.
    /// </summary>
    public enum TimeUnit
    {
        /// <summary>
        /// Секунда.
        /// </summary>
        [Description("секунд(у)")]
        Second = 1,

        /// <summary>
        /// Минута.
        /// </summary>
        [Description("минут(у)")]
        Minute = 60,

        /// <summary>
        /// Час.
        /// </summary>
        [Description("час(ов)")]
        Hour = 3600,

        /// <summary>
        /// День.
        /// </summary>
        [Description("день(дней)")]
        Day = 86400
    }
}