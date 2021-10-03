// ------------------------------------------------------------------------------------------------------
// <copyright file="ITokenService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Uchoose.TokenService.Interfaces.Requests;
using Uchoose.TokenService.Interfaces.Responses;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.TokenService.Interfaces
{
    /// <summary>
    /// Сервис для работы с авторизационными токенами пользователей.
    /// </summary>
    public interface ITokenService :
        IApplicationService
    {
        /// <summary>
        /// Получить авторизационный токен пользователя.
        /// </summary>
        /// <param name="request">Запрос на получение авторизационного токена пользователя.</param>
        /// <param name="ipAddress">Ip адрес пользователя.</param>
        /// <param name="withRefreshToken">Генерировать refresh токен.</param>
        /// <returns>Возвращает авторизационный токен пользователя.</returns>
        Task<IResult<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress, bool withRefreshToken = true);

        /// <summary>
        /// Обновить авторизационный токен пользователя.
        /// </summary>
        /// <param name="request">Запрос на обновление авторизационного токена пользователя.</param>
        /// <param name="ipAddress">IP адрес.</param>
        /// <returns>Возвращает авторизационный токен пользователя.</returns>
        Task<IResult<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
    }
}