// ------------------------------------------------------------------------------------------------------
// <copyright file="SmsRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.SmsService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на отправку СМС сообщения.
    /// </summary>
    public class SmsRequest
    {
        /// <summary>
        /// Номер телефона получателя.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Сообщение.
        /// </summary>
        public string Message { get; set; }
    }
}