// ------------------------------------------------------------------------------------------------------
// <copyright file="CustomException.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;

namespace Uchoose.Utils.Exceptions
{
    /// <summary>
    /// Пользовательское исключение.
    /// </summary>
    [Serializable]
    public class CustomException : Exception
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="CustomException"/>.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="errors">Список ошибок.</param>
        /// <param name="statusCode">Код состояния http.</param>
        public CustomException(string message, List<string> errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            ErrorMessages = errors;
            StatusCode = statusCode;
        }

        /// <summary>
        /// Инициализирует экземпляр <see cref="CustomException"/>.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="errors">Список ошибок.</param>
        /// <param name="statusCode">Код состояния http.</param>
        /// <param name="args">Аргументы сообщения.</param>
        public CustomException(string message, List<string> errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
            ErrorMessages = errors;
            StatusCode = statusCode;
        }

        /// <summary>
        /// Инициализирует экземпляр <see cref="CustomException"/> при десериализации.
        /// </summary>
        /// <param name="info"><see cref="SerializationInfo"/>.</param>
        /// <param name="context"><see cref="StreamingContext"/>.</param>
        protected CustomException(SerializationInfo info, in StreamingContext context)
            : base(info, context)
        {
            ErrorMessages = (List<string>)info.GetValue(nameof(ErrorMessages), typeof(List<string>));
            StatusCode = (HttpStatusCode)(info.GetValue(nameof(StatusCode), typeof(HttpStatusCode)) ?? HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Список сообщений об ошибках.
        /// </summary>
        // ReSharper disable once MemberInitializerValueIgnored
        public List<string> ErrorMessages { get; } = new();

        /// <summary>
        /// Код состояния http.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Получить данные объекта при его сериализации.
        /// </summary>
        /// <param name="info"><see cref="SerializationInfo"/>.</param>
        /// <param name="context"><see cref="StreamingContext"/>.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(ErrorMessages), ErrorMessages, typeof(List<string>));
            info.AddValue(nameof(StatusCode), StatusCode, typeof(HttpStatusCode));
        }
    }
}