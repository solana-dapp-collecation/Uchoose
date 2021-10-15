// ------------------------------------------------------------------------------------------------------
// <copyright file="RolePermissionsResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Identity;
using Uchoose.RoleClaimService.Interfaces.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Identity.Roles.Responses
{
    /// <summary>
    /// Пример ответа для <see cref="RolesController.GetPermissionsByRoleIdAsync"/>.
    /// </summary>
    public class RolePermissionsResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<RolePermissionsResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="RolePermissionsResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public RolePermissionsResponseExample(IStringLocalizer<RolePermissionsResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<RolePermissionsResponse>.Success(
                new()
                {
                    RoleId = Guid.Empty,
                    RoleName = _localizer["<Role name>"],
                    RoleClaims = new()
                    {
                        new()
                        {
                            Id = default,
                            RoleId = Guid.Empty,
                            Type = _localizer["<Role claim type>"],
                            Value = _localizer["<Role claim value>"],
                            Description = _localizer["<Role claim description>"],
                            Group = _localizer["<Role claim group>"],
                            Selected = false
                        }
                    }
                },
                _localizer["<Message>"]);
        }
    }
}