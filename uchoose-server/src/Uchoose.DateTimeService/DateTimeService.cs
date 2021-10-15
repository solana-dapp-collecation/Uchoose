// ------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.DateTimeService.Interfaces;
using Uchoose.Utils.Contracts.Services;

namespace Uchoose.DateTimeService
{
    /// <inheritdoc cref="IDateTimeService"/>
    internal sealed class DateTimeService :
        IDateTimeService,
        IScopedService
    {
        /// <inheritdoc/>
        public DateTime NowUtc => DateTime.UtcNow;

        /// <inheritdoc/>
        public DateTimeOffset NowUtcOffset => DateTimeOffset.UtcNow;

        /// <inheritdoc/>
        public long NowUtcTimestampMilliseconds => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        /// <inheritdoc/>
        public DateTime NowLocalTime => DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
    }
}