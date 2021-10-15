// ------------------------------------------------------------------------------------------------------
// <copyright file="MailSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.MailService.Interfaces.Settings
{
    /// <summary>
    /// Настройки email.
    /// </summary>
    public class MailSettings :
        ISettings
    {
        /// <summary>
        /// Адрес отправителя.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Хост сервера.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Порт сервера.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Имя пользователя для подключения к серверу.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Пароль для подключения к серверу.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Отображаемое имя.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Включить верификацию по email.
        /// </summary>
        public bool EnableVerification { get; set; }

        /// <summary>
        /// Использовать аутентификацию по логину и паролю.
        /// </summary>
        public bool EnableAuthentication { get; set; }
    }
}