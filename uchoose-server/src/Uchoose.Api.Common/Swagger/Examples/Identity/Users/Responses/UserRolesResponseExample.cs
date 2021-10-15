// ------------------------------------------------------------------------------------------------------
// <copyright file="UserRolesResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Identity;
using Uchoose.UserService.Interfaces.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Identity.Users.Responses
{
    /// <summary>
    /// Пример ответа для <see cref="UsersController.GetRolesAsync"/>.
    /// </summary>
    public class UserRolesResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<UserRolesResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserRolesResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public UserRolesResponseExample(IStringLocalizer<UserRolesResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<UserRolesResponse>.Success(
                new()
                {
                    UserRoles = new()
                    {
                        new()
                        {
                            RoleId = Guid.Empty,
                            RoleName = _localizer["<Role name 1>"],
                            Selected = true
                        },
                        new()
                        {
                            RoleId = Guid.Empty,
                            RoleName = _localizer["<Role name 2>"],
                            Selected = false
                        }
                    }
                },
                _localizer["<Message>"]);
        }
    }
}