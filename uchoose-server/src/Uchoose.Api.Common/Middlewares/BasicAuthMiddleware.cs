// ------------------------------------------------------------------------------------------------------
// <copyright file="BasicAuthMiddleware.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Uchoose.Api.Common.Settings;
using Uchoose.Application.Constants.Permission;
using Uchoose.CurrentUserService.Interfaces;
using Uchoose.TokenService.Interfaces;
using Uchoose.TokenService.Interfaces.Requests;
using Uchoose.Utils.Extensions;

namespace Uchoose.Api.Common.Middlewares
{
    /// <summary>
    /// Обработчик Basic авторизации для доступа к различным ресурсам сервера.
    /// </summary>
    public class BasicAuthMiddleware :
        IMiddleware
    {
        private readonly ITokenService _tokenService;
        private readonly ICurrentUserService _currentUserService;
        private readonly BasicAuthSettings _basicAuthSettings;
        private readonly List<string> _availableBasicAuthClaims;

        /// <summary>
        /// Инициализирует экземпляр <see cref="BasicAuthMiddleware"/>.
        /// </summary>
        /// <param name="tokenService"><see cref="ITokenService"/>.</param>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        /// <param name="basicAuthSettings"><see cref="BasicAuthSettings"/>.</param>
        public BasicAuthMiddleware(
            ITokenService tokenService,
            ICurrentUserService currentUserService,
            IOptionsSnapshot<BasicAuthSettings> basicAuthSettings)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _currentUserService = currentUserService;
            _basicAuthSettings = basicAuthSettings.Value;
            _availableBasicAuthClaims = typeof(Application.Constants.Permission.Permissions.BasicAuth)
                .GetAllPublicConstantValues<string>();
        }

        /// <summary>
        /// Получить учётные данные из заголовка авторизации.
        /// </summary>
        /// <param name="request"><see cref="HttpRequest"/>.</param>
        /// <returns>Возвращает учётные данные из заголовка авторизации.</returns>
        public static (string?, string?) GetCredentialsFromAuthorizationHeader(HttpRequest request)
        {
            string authHeader = request.Headers["Authorization"];
            if (authHeader != null)
            {
                if (AuthenticationHeaderValue.TryParse(authHeader, out var header)
                    && header.Scheme.Equals(AuthenticationSchemes.Basic.ToString(), StringComparison.OrdinalIgnoreCase)
                    && header.Parameter.IsPresent())
                {
                    string[] credentials = header.Parameter.FromBase64ToString(Encoding.UTF8).Split(':', 2);
                    string username = credentials[0];
                    string password = credentials[1];

                    return (username, password);
                }
            }

            return (null, null);
        }

        /// <inheritdoc/>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (_basicAuthSettings.IsEnabled)
            {
                var currentPathSettings = _basicAuthSettings.SecuredPaths.Find(p =>
                        context.Request.Path.StartsWithSegments(p.PathPrefix));

                if (currentPathSettings is { IsEnabled: true } && _availableBasicAuthClaims.Intersect(currentPathSettings.RequiredClaims).Any())
                {
                    // проверяем, не авторизован ли уже user (например, через JWT в Authorization заголовке)
                    if (_currentUserService.GetUserClaims().Where(c => c.Type == ApplicationClaimTypes.Permission).Select(c => c.Value)
                        .Intersect(currentPathSettings.RequiredClaims).Count() == currentPathSettings.RequiredClaims.Count)
                    {
                        await next.Invoke(context).ConfigureAwait(false);
                        return;
                    }

                    (string? userName, string? password) = GetCredentialsFromAuthorizationHeader(context.Request);
                    if (userName.IsPresent() && password.IsPresent())
                    {
                        var tokenRequest = new TokenRequest
                        {
                            Email = userName,
                            Password = password
                        };
                        var tokenResult = await _tokenService.GetTokenAsync(tokenRequest, context.GetRequestIpAddress(), false);
                        if (tokenResult.Succeeded)
                        {
                            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenResult.Data.Token);
                            if (token.Claims.Where(c => c.Type == ApplicationClaimTypes.Permission).Select(c => c.Value)
                                .Intersect(currentPathSettings.RequiredClaims).Count() == currentPathSettings.RequiredClaims.Count)
                            {
                                await next.Invoke(context).ConfigureAwait(false);
                                return;
                            }
                        }
                    }

                    context.Response.Headers["WWW-Authenticate"] = $"{AuthenticationSchemes.Basic} realm=\"Access to the {currentPathSettings.Name ?? "secured path"}.\", charset=\"UTF-8\""; // TODO - локализовать
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                else
                {
                    await next.Invoke(context).ConfigureAwait(false);
                }
            }
            else
            {
                await next.Invoke(context).ConfigureAwait(false);
            }
        }
    }
}