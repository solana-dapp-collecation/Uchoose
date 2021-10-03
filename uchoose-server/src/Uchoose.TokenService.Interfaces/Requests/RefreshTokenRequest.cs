// ------------------------------------------------------------------------------------------------------
// <copyright file="RefreshTokenRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.TokenService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на обновление авторизационного токена пользователя.
    /// </summary>
    public class RefreshTokenRequest
    {
        /// <summary>
        /// Авторизационный токен пользователя.
        /// </summary>
        /// <example>Example</example>
        public string Token { get; set; }

        /// <summary>
        /// Refresh токен.
        /// </summary>
        /// <example>Example</example>
        public string RefreshToken { get; set; }
    }
}