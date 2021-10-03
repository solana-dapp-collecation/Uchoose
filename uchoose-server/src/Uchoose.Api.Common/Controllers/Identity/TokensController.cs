// ------------------------------------------------------------------------------------------------------
// <copyright file="TokensController.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Identity.Abstractions;
using Uchoose.Api.Common.Swagger.Examples.Identity.Tokens.Responses;
using Uchoose.TokenService.Interfaces;
using Uchoose.TokenService.Interfaces.Requests;
using Uchoose.TokenService.Interfaces.Responses;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Controllers.Identity
{
    /// <summary>
    /// Контроллер для работы с авторизационными токенами пользователей.
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [SwaggerTag("Авторизационные токены пользователей.")]
    internal sealed class TokensController :
        IdentityBaseController
    {
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Инициализирует экземпляр <see cref="TokensController"/>.
        /// </summary>
        /// <param name="tokenService"><see cref="ITokenService"/>.</param>
        public TokensController(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        /// <summary>
        /// Получить авторизационный токен пользователя.
        /// </summary>
        /// <param name="request">Запрос на получение авторизационного токена пользователя.</param>
        /// <returns>Возвращает авторизационный токен пользователя.</returns>
        /// <response code="200">Возвращает авторизационный токен пользователя.</response>
        [MapToApiVersion("1")]
        [HttpPost(Name = "GetToken")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "GetToken",
            Tags = new[] { TokensTag })]
        [ProducesResponseType(typeof(Result<TokenResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TokenResponseExample))]
        public async Task<IActionResult> GetTokenAsync(TokenRequest request)
        {
            var result = await _tokenService.GetTokenAsync(request, HttpContext.GetRequestIpAddress());
            return Ok(result);
        }

        /// <summary>
        /// Обновить авторизационный токен пользователя.
        /// </summary>
        /// <param name="request">Запрос на обновление авторизационного токена пользователя.</param>
        /// <returns>Возвращает обновлённый авторизационный токен пользователя.</returns>
        /// <response code="200">Возвращает обновлённый авторизационный токен пользователя.</response>
        [MapToApiVersion("1")]
        [HttpPost("refresh", Name = "RefreshToken")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "RefreshToken",
            Tags = new[] { TokensTag })]
        [ProducesResponseType(typeof(Result<TokenResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TokenResponseExample))]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request)
        {
            var result = await _tokenService.RefreshTokenAsync(request, HttpContext.GetRequestIpAddress());
            return Ok(result);
        }
    }
}