// ------------------------------------------------------------------------------------------------------
// <copyright file="TokenRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Uchoose.TokenService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на получение авторизационного токена пользователя.
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// Email пользователя.
        /// </summary>
        /// <example>example@example.com</example>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        /// <example>********</example>
        [Required]
        public string Password { get; set; }
    }
}