// ------------------------------------------------------------------------------------------------------
// <copyright file="UserPermissionsResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Identity;
using Uchoose.Api.Common.Swagger.Examples.Identity.Roles.Responses;
using Uchoose.UserClaimService.Interfaces.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Identity.Users.Responses
{
    /// <summary>
    /// Пример ответа для <see cref="UsersController.GetPermissionsByUserIdAsync"/>.
    /// </summary>
    public class UserPermissionsResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<UserPermissionsResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="RolePermissionsResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public UserPermissionsResponseExample(IStringLocalizer<UserPermissionsResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<UserPermissionsResponse>.Success(
                new()
                {
                    UserId = Guid.Empty,
                    UserName = _localizer["<User name>"],
                    UserClaims = new()
                    {
                        new()
                        {
                            Id = default,
                            UserId = Guid.Empty,
                            Type = _localizer["<User claim type>"],
                            Value = _localizer["<User claim value>"],
                            Description = _localizer["<User claim description>"],
                            Group = _localizer["<User claim group>"],
                            Selected = false
                        }
                    }
                },
                _localizer["<Message>"]);
        }
    }
}