// ------------------------------------------------------------------------------------------------------
// <copyright file="ResetPasswordRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.IdentityService.Interfaces.Requests
{
    /// <summary>
    /// Запрос для сброса пароля пользователя.
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// Email.
        /// </summary>
        /// <example>example@example.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        /// <example>********</example>
        public string Password { get; set; }

        /// <summary>
        /// Токен из письма для сброса пароля.
        /// </summary>
        /// <example>Example</example>
        public string Token { get; set; }
    }
}