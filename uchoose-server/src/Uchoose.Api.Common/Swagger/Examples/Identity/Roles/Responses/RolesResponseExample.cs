// ------------------------------------------------------------------------------------------------------
// <copyright file="RolesResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Identity;
using Uchoose.RoleService.Interfaces.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Identity.Roles.Responses
{
    /// <summary>
    /// Пример ответа для <see cref="RolesController.GetAllAsync"/>.
    /// </summary>
    public class RolesResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<RolesResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="RolesResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public RolesResponseExample(IStringLocalizer<RolesResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<List<RoleResponse>>.Success(
                new()
                {
                    new()
                    {
                        Name = _localizer["<Role name 1>"],
                        Description = _localizer["<Role description 1>"],
                        Id = Guid.Empty
                    },
                    new()
                    {
                        Name = _localizer["<Role name 2>"],
                        Description = _localizer["<Role description 2>"],
                        Id = Guid.Empty
                    }
                },
                _localizer["<Message>"]);
        }
    }
}