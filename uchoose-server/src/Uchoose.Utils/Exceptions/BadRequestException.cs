// ------------------------------------------------------------------------------------------------------
// <copyright file="BadRequestException.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;

namespace Uchoose.Utils.Exceptions
{
    /// <summary>
    /// Исключение, указывающее, что был отправлен некорректный запрос.
    /// </summary>
    public class BadRequestException :
        CustomException
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="BadRequestException"/>.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="errors">Список ошибок.</param>
        public BadRequestException(string message, List<string> errors = default)
            : base(message, errors, HttpStatusCode.BadRequest)
        {
        }

        /// <summary>
        /// Инициализирует экземпляр <see cref="BadRequestException"/>.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="errors">Список ошибок.</param>
        /// <param name="args">Аргументы сообщения.</param>
        public BadRequestException(string message, List<string> errors = default, params object[] args)
            : base(message, errors, HttpStatusCode.BadRequest, args)
        {
        }
    }
}