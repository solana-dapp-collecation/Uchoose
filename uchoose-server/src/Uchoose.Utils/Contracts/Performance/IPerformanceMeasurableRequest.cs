// ------------------------------------------------------------------------------------------------------
// <copyright file="IPerformanceMeasurableRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Utils.Contracts.Performance
{
    /// <summary>
    /// Запрос, для которого измеряется производительность.
    /// </summary>
    /// <remarks>
    /// Используется в посреднике для измерения производительности запросов при вызове их обработчиков.
    /// </remarks>
    public interface IPerformanceMeasurableRequest
    {
        /// <summary>
        /// Предел нормальной производительности запроса до получения ответа в миллисекундах.
        /// </summary>
        /// <remarks>
        /// Если запрос выполняется дольше, то логируется предупреждение о долгом запросе.
        /// </remarks>
        long ElapsedMillisecondsLimit { get; }

        /// <summary>
        /// Логировать данные о производительности запрос, если предел не достигнут.
        /// </summary>
        bool LogIfLimitNotReached { get; }
    }
}