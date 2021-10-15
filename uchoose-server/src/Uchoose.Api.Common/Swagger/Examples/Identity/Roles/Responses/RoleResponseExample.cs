// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Identity;
using Uchoose.RoleService.Interfaces.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Identity.Roles.Responses
{
    /// <summary>
    /// Пример ответа для <see cref="RolesController.GetByIdAsync"/>.
    /// </summary>
    public class RoleResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<RoleResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="RoleResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public RoleResponseExample(IStringLocalizer<RoleResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<RoleResponse>.Success(
                new()
                {
                    Name = _localizer["<Role name>"],
                    Description = _localizer["<Role description>"],
                    Id = Guid.Empty
                },
                _localizer["<Message>"]);
        }
    }
}