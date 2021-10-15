// ------------------------------------------------------------------------------------------------------
// <copyright file="BasicAuthFilter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Uchoose.Api.Common.Middlewares;
using Uchoose.Api.Common.Settings;
using Uchoose.Domain.Identity.Entities;
using Uchoose.Domain.Identity.Exceptions;
using Uchoose.Utils.Extensions;

namespace Uchoose.Api.Common.Filters.Auth
{
    /// <summary>
    /// Фильтр для Basic авторизации.
    /// </summary>
    internal class BasicAuthFilter : IAsyncAuthorizationFilter
    {
        private readonly IStringLocalizer<BasicAuthFilter> _localizer;
        private readonly string? _realm;

        /// <summary>
        /// Инициализирует экземпляр <see cref="BasicAuthFilter"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="realm">Описание защищаемого ресурса.</param>
        public BasicAuthFilter(
            IStringLocalizer<BasicAuthFilter> localizer,
            string? realm = null)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            _realm = realm;
        }

        /// <inheritdoc/>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            (string? userName, string? password) = BasicAuthMiddleware.GetCredentialsFromAuthorizationHeader(context.HttpContext.Request);
            if (await IsAuthorizedAsync(context, userName, password))
            {
                return;
            }

            context.HttpContext.Response.Headers["WWW-Authenticate"] = $"{AuthenticationSchemes.Basic} realm=\"{string.Format(_localizer["Access to the {0}"]!, _realm ?? _localizer["secured path"])}.\", charset=\"UTF-8\"";
            throw new IdentityException(_localizer["You are not authorized by Basic authorization."], statusCode: HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// Проверить авторизацию по указанным учётным данным.
        /// </summary>
        /// <param name="context"><see cref="AuthorizationFilterContext"/>.</param>
        /// <param name="userName">UserName.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Возвращает true при успешной авторизации. Иначе - false.</returns>
        private async Task<bool> IsAuthorizedAsync(AuthorizationFilterContext context, string? userName, string? password)
        {
            var basicAuthSettings = context.HttpContext.RequestServices.GetRequiredService<IOptionsSnapshot<BasicAuthSettings>>().Value;
            if (!basicAuthSettings.IsEnabled || !basicAuthSettings.UseBasicAuthAttribute)
            {
                return true;
            }

            if (userName.IsPresent() && password.IsPresent())
            {
                var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<UchooseUser>>();
                var user = await userManager.FindByEmailAsync(userName);
                if (user?.IsActive != true)
                {
                    throw new IdentityException(_localizer["User Not Active. Please contact the administrator."], statusCode: HttpStatusCode.Unauthorized);
                }

                return await userManager.CheckPasswordAsync(user, password);
            }

            context.HttpContext.Response.Headers["WWW-Authenticate"] = $"{AuthenticationSchemes.Basic} realm=\"{string.Format(_localizer["Access to the {0}"]!, _realm ?? _localizer["secured path"])}.\", charset=\"UTF-8\"";
            throw new IdentityException(_localizer["You are not authorized by Basic authorization."], statusCode: HttpStatusCode.Unauthorized);
        }
    }
}