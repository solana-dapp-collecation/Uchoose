// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
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
    /// Пример ответа для <see cref="RolesController.GetClaimByIdAsync"/>.
    /// </summary>
    public class RoleClaimResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<RoleClaimResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="RoleClaimResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public RoleClaimResponseExample(IStringLocalizer<RoleClaimResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<RoleClaimResponse>.Success(
                new()
                {
                    Id = default,
                    RoleId = Guid.Empty,
                    Type = _localizer["<Role claim type>"],
                    Value = _localizer["<Role claim value>"],
                    Description = _localizer["<Role claim description>"],
                    Group = _localizer["<Role claim group>"]
                },
                _localizer["<Message>"]);
        }
    }
}