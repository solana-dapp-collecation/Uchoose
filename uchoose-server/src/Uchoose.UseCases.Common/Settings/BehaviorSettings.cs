// ------------------------------------------------------------------------------------------------------
// <copyright file="BehaviorSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.UseCases.Common.Settings
{
    /// <summary>
    /// Настройки посредников для запросов при вызове их обработчиков.
    /// </summary>
    public class BehaviorSettings :
        ISettings
    {
        /// <summary>
        /// Использовать посредник для проверки валидации запросов при вызове их обработчиков.
        /// </summary>
        public bool UseRequestValidationBehavior { get; set; }

        /// <summary>
        /// Использовать посредник для логирования запросов и ответов при вызове их обработчиков.
        /// </summary>
        public bool UseLoggingBehavior { get; set; }

        /// <summary>
        /// Использовать посредник для кэширования ответов для запросов при вызове их обработчиков.
        /// </summary>
        public bool UseResponseCachingBehavior { get; set; }

        /// <summary>
        /// Использовать посредник для замера производительности запросов до получения ответа при вызове их обработчиков.
        /// </summary>
        public bool UsePerformanceBehavior { get; set; }
    }
}