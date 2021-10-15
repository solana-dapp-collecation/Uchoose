// ------------------------------------------------------------------------------------------------------
// <copyright file="IDateTimeService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Utils.Contracts.Services;

namespace Uchoose.DateTimeService.Interfaces
{
    /// <summary>
    /// Сервис для работы с датой и временем.
    /// </summary>
    public interface IDateTimeService :
        IInfrastructureService
    {
        /// <summary>
        /// Текущие дата и время в UTC.
        /// </summary>
        DateTime NowUtc { get; }

        /// <summary>
        /// Текущий момент времени относительно даты и времени в UTC.
        /// </summary>
        DateTimeOffset NowUtcOffset { get; }

        /// <summary>
        /// Количество миллисекунд, истекших с 1970-01-01T00:00:00.000Z.
        /// </summary>
        long NowUtcTimestampMilliseconds { get; }

        /// <summary>
        /// Текущие локальные дата и время.
        /// </summary>
        DateTime NowLocalTime { get; }
    }
}