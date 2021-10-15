// ------------------------------------------------------------------------------------------------------
// <copyright file="MailRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;

namespace Uchoose.MailService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на отправку email сообщения.
    /// </summary>
    public class MailRequest
    {
        /// <summary>
        /// Адреса получателей.
        /// </summary>
        public string[] To { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Тема письма.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Тело письма.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Адрес отправителя.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Список прикреплённых файлов.
        /// </summary>
        public List<IFormFile> Attachments { get; set; } = new();
    }
}