// ------------------------------------------------------------------------------------------------------
// <copyright file="CacheSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.UseCases.Common.Settings
{
    /// <summary>
    /// Настройки кэширования.
    /// </summary>
    public class CacheSettings :
        ISettings
    {
        /// <summary>
        /// Абсолютный срок действия кэша в часах.
        /// </summary>
        public int AbsoluteExpirationInHours { get; set; }

        /// <summary>
        /// Скользящий срок действия кэша в минутах.
        /// </summary>
        public int SlidingExpirationInMinutes { get; set; }
    }
}