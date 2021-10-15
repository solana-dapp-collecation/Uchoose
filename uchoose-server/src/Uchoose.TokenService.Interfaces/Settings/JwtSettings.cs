// ------------------------------------------------------------------------------------------------------
// <copyright file="JwtSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.TokenService.Interfaces.Settings
{
    /// <summary>
    /// Настройки JWT.
    /// </summary>
    public class JwtSettings :
        ISettings
    {
        /// <summary>
        /// Использовать SSL при отправке токена.
        /// </summary>
        public bool RequireHttpsMetadata { get; set; }

        /// <summary>
        /// Секретный ключ для формирования авторизационного токена.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Издатель токена авторизации.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Валидировать издателя токена авторизации.
        /// </summary>
        public bool ValidateIssuer { get; set; }

        /// <summary>
        /// Потребитель токена авторизации.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Валидировать потребителя токена авторизации.
        /// </summary>
        public bool ValidateAudience { get; set; }

        /// <summary>
        /// Срок действия авторизационного токена в минутах.
        /// </summary>
        public int TokenExpirationInMinutes { get; set; }

        /// <summary>
        /// Срок действия refresh токена в днях.
        /// </summary>
        public int RefreshTokenExpirationInDays { get; set; }
    }
}