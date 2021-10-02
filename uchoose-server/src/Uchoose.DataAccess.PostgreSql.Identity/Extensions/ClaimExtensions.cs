// ------------------------------------------------------------------------------------------------------
// <copyright file="ClaimExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Uchoose.Application.Constants.Permission;
using Uchoose.Domain.Identity.Entities;

namespace Uchoose.DataAccess.PostgreSql.Identity.Extensions
{
    /// <summary>
    /// Методы расширения для разрешений роли.
    /// </summary>
    public static class ClaimExtensions
    {
        /// <summary>
        /// Добавить разрешение роли пользователя.
        /// </summary>
        /// <param name="roleManager"><see cref="RoleManager{TRole}"/>.</param>
        /// <param name="role">Роль пользователя.</param>
        /// <param name="permission">Разрешение.</param>
        /// <returns>Возвращает <see cref="IdentityResult"/>.</returns>
        public static async Task<IdentityResult> AddPermissionClaimAsync(this RoleManager<UchooseRole> roleManager, UchooseRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission))
            {
                return await roleManager.AddClaimAsync(role, new(ApplicationClaimTypes.Permission, permission));
            }

            return IdentityResult.Failed();
        }
    }
}