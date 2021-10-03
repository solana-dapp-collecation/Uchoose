// ------------------------------------------------------------------------------------------------------
// <copyright file="ICacheableQuery.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.Utils.Contracts.Caching
{
    /// <summary>
    /// Кэшируемый запрос.
    /// </summary>
    /// <remarks>
    /// Используется в посреднике для кэширования запросов при вызове их обработчиков.
    /// </remarks>
    public interface ICacheableQuery
    {
        /// <summary>
        /// Не использовать кэш.
        /// </summary>
        bool BypassCache { get; }

        /// <summary>
        /// Уникальный ключ кэша, по которому хранится значение в хранилище.
        /// </summary>
        string CacheKey { get; }

        /// <summary>
        /// Сбросить скользящий срок действия кэша (если доступно) для данного запроса.
        /// </summary>
        bool RefreshCachedEntry { get; }

        /// <summary>
        /// Принудительно заменить кэшируемую запись значением из хранилища.
        /// </summary>
        bool ReplaceCachedEntry { get; }

        /// <summary>
        /// Скользящий срок действия кэша в часах.
        /// </summary>
        TimeSpan? SlidingExpiration { get; }
    }
}