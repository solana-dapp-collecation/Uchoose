// ------------------------------------------------------------------------------------------------------
// <copyright file="ValueStopwatch.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;

using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.Utils.Performance
{
    /// <summary>
    /// <see cref="Stopwatch"/> без аллокаций.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://www.meziantou.net/how-to-measure-elapsed-time-without-allocating-a-stopwatch.htm">How to measure elapsed time without allocating a Stopwatch.</seealso>
    /// </remarks>
    public readonly struct ValueStopwatch
        : IHasIsActive
    {
        private static readonly double TimestampToTicks = TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency;

        private readonly long _startTimestamp;

        private ValueStopwatch(long startTimestamp) => _startTimestamp = startTimestamp;

        /// <summary>
        /// Является активным.
        /// </summary>
        public bool IsActive => _startTimestamp != 0;

        /// <summary>
        /// Общее затраченное время, измеренное текущим экземпляром.
        /// </summary>
        public TimeSpan Elapsed => new(GetElapsedDateTimeTicks());

        /// <summary>
        /// Общее затраченное время в миллисекундах, измеренное текущим экземпляром.
        /// </summary>
        public long ElapsedMilliseconds => GetElapsedDateTimeTicks() / TimeSpan.TicksPerMillisecond;

        /// <summary>
        /// Создать новый экземпляр и начать отсчёт времени.
        /// </summary>
        /// <returns>Возвращает <see cref="ValueStopwatch"/>.</returns>
        public static ValueStopwatch StartNew() => new(Stopwatch.GetTimestamp());

        /// <summary>
        /// Получить прошедшее время с момента запуска.
        /// </summary>
        /// <returns>Возвращает прошедшее время с момента запуска.</returns>
        /// <exception cref="InvalidOperationException">Возникает, если <see cref="ValueStopwatch"/> не был инициализирован.</exception>
        public long GetElapsedDateTimeTicks()
        {
            // Start timestamp can't be zero in an initialized ValueStopwatch. It would have to be literally the first thing executed when the machine boots to be 0.
            // So it being 0 is a clear indication of default(ValueStopwatch)
            if (!IsActive)
            {
                throw new InvalidOperationException("An uninitialized, or 'default', ValueStopwatch cannot be used to get elapsed time.");
            }

            long end = Stopwatch.GetTimestamp();
            long timestampDelta = end - _startTimestamp;
            return (long)(TimestampToTicks * timestampDelta);
        }
    }
}