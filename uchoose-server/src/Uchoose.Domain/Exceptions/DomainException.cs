// ------------------------------------------------------------------------------------------------------
// <copyright file="DomainException.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;

using Uchoose.Utils.Exceptions;

namespace Uchoose.Domain.Exceptions
{
    /// <summary>
    /// Исключение уровня домена.
    /// </summary>
    public class DomainException :
        CustomException
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="DomainException"/>.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="errors">Список ошибок.</param>
        /// <param name="statusCode">Код состояния http.</param>
        public DomainException(string message, List<string> errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message, errors, statusCode)
        {
        }

        /// <summary>
        /// Инициализирует экземпляр <see cref="DomainException"/>.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="errors">Список ошибок.</param>
        /// <param name="statusCode">Код состояния http.</param>
        /// <param name="args">Аргументы сообщения.</param>
        public DomainException(string message, List<string> errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, params object[] args)
            : base(message, errors, statusCode, args)
        {
        }
    }
}