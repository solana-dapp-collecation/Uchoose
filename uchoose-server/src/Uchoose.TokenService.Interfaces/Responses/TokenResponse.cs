// ------------------------------------------------------------------------------------------------------
// <copyright file="TokenResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.TokenService.Interfaces.Responses
{
    /// <summary>
    /// Ответ с данными авторизационного токена пользователя.
    /// </summary>
    /// <param name="Token">Авторизационный токен.</param>
    /// <param name="RefreshToken">Refresh токен.</param>
    /// <param name="RefreshTokenExpiryTime">Дата истечения срока действия refresh токена.</param>
    public record TokenResponse(
        string Token,
        string RefreshToken,
        DateTime RefreshTokenExpiryTime);
}