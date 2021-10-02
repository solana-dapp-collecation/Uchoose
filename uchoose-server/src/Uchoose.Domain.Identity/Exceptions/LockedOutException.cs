// ------------------------------------------------------------------------------------------------------
// <copyright file="LockedOutException.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;

namespace Uchoose.Domain.Identity.Exceptions
{
    /// <summary>
    /// Исключение, указывающее, что аккаунт пользователя был заблокирован.
    /// </summary>
    public class LockedOutException : IdentityException
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="LockedOutException"/>.
        /// </summary>
        /// <param name="lockoutEnd">Дата снятия блокировки.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="errors">Список ошибок.</param>
        public LockedOutException(DateTimeOffset? lockoutEnd, string message, List<string> errors = default)
            : base(message, errors, HttpStatusCode.Unauthorized)
        {
            LockoutEnd = lockoutEnd;
        }

        /// <summary>
        /// Дата снятия блокировки.
        /// </summary>
        public DateTimeOffset? LockoutEnd { get; }
    }
}