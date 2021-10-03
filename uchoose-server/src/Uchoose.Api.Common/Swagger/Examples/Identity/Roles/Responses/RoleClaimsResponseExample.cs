// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimsResponseExample.cs" company="Life Loop">
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
using Uchoose.RoleClaimService.Interfaces.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Identity.Roles.Responses
{
    /// <summary>
    /// Пример ответа для <see cref="RolesController.GetAllClaimsAsync"/>.
    /// </summary>
    public class RoleClaimsResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<RoleClaimsResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="RoleClaimsResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public RoleClaimsResponseExample(IStringLocalizer<RoleClaimsResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<List<RoleClaimResponse>>.Success(
                new()
                {
                    new()
                    {
                        Id = default,
                        RoleId = Guid.Empty,
                        Type = _localizer["<Role claim type 1>"],
                        Value = _localizer["<Role claim value 1>"],
                        Description = _localizer["<Role claim description 1>"],
                        Group = _localizer["<Role claim group 1>"]
                    },
                    new()
                    {
                        Id = default,
                        RoleId = Guid.Empty,
                        Type = _localizer["<Role claim type 2>"],
                        Value = _localizer["<Role claim value 2>"],
                        Description = _localizer["<Role claim description 2>"],
                        Group = _localizer["<Role claim group 2>"]
                    }
                },
                _localizer["<Message>"]);
        }
    }
}