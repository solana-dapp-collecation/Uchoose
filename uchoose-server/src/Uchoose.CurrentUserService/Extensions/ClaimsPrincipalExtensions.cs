// ------------------------------------------------------------------------------------------------------
// <copyright file="ClaimsPrincipalExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Security.Claims;

namespace Uchoose.CurrentUserService.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="ClaimsPrincipal"/>.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Получить идентификатор пользователя.
        /// </summary>
        /// <param name="principal"><see cref="ClaimsPrincipal"/>.</param>
        /// <returns>Возвращает идентификатор пользователя.</returns>
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        /// <summary>
        /// Получить email пользователя.
        /// </summary>
        /// <param name="principal"><see cref="ClaimsPrincipal"/>.</param>
        /// <returns>Возвращает email пользователя.</returns>
        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }
    }
}