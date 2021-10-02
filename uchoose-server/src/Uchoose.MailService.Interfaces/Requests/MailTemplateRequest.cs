// ------------------------------------------------------------------------------------------------------
// <copyright file="MailTemplateRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Uchoose.MailService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на отправку email сообщения из шаблона.
    /// </summary>
    public class MailTemplateRequest
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
        /// Адрес отправителя.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Имя используемого шаблона.
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// Подстановки (замены) в тексте шаблона.
        /// </summary>
        /// <remarks>
        /// Ключ - заменяемый текст, значение - на что заменить.
        /// </remarks>
        public Dictionary<string, string> Substitutions { get; set; } = new();
    }
}