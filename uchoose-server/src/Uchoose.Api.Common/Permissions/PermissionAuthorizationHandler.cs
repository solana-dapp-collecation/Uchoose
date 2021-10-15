// ------------------------------------------------------------------------------------------------------
// <copyright file="PermissionAuthorizationHandler.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Uchoose.Application.Constants.Permission;

namespace Uchoose.Api.Common.Permissions
{
    /// <summary>
    /// Обработчик разрешений для авторизации.
    /// </summary>
    internal class PermissionAuthorizationHandler :
        AuthorizationHandler<PermissionRequirement>
    {
        private const string LocalAuthorityIssuer = "LOCAL AUTHORITY";

        /// <summary>
        /// Инициализирует экземпляр <see cref="PermissionAuthorizationHandler"/>.
        /// </summary>
        // ReSharper disable once EmptyConstructor
        public PermissionAuthorizationHandler()
        {
        }

        /// <inheritdoc/>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (!context.User.Claims.Any())
            {
                await Task.CompletedTask;
            }

            var permissions = context.User.Claims
                .Where(x => x.Type == ApplicationClaimTypes.Permission && x.Value == requirement.Permission && x.Issuer == LocalAuthorityIssuer);
            if (permissions.Any())
            {
                context.Succeed(requirement);
                await Task.CompletedTask;
            }
        }
    }
}