// ------------------------------------------------------------------------------------------------------
// <copyright file="SmsSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.SmsService.Interfaces.Settings
{
    /// <summary>
    /// Настройки СМС.
    /// </summary>
    public class SmsSettings :
        ISettings
    {
        /// <summary>
        /// Идентификатор для подключения к сервису СМС.
        /// </summary>
        public string SmsAccountIdentification { get; set; }

        /// <summary>
        /// Пароль для подключения к сервису СМС.
        /// </summary>
        public string SmsAccountPassword { get; set; }

        /// <summary>
        /// Телефон отправителя.
        /// </summary>
        public string SmsAccountFrom { get; set; }

        /// <summary>
        /// Включить верификацию по СМС.
        /// </summary>
        public bool EnableVerification { get; set; }
    }
}