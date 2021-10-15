// ------------------------------------------------------------------------------------------------------
// <copyright file="UserClaimsResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Identity;
using Uchoose.UserClaimService.Interfaces.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Identity.Users.Responses
{
    /// <summary>
    /// Пример ответа для <see cref="UsersController.GetAllClaimsAsync"/>.
    /// </summary>
    public class UserClaimsResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<UserClaimsResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserClaimsResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public UserClaimsResponseExample(IStringLocalizer<UserClaimsResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<List<UserClaimResponse>>.Success(
                new()
                {
                    new()
                    {
                        Id = default,
                        UserId = Guid.Empty,
                        Type = _localizer["<User claim type 1>"],
                        Value = _localizer["<User claim value 1>"],
                        Description = _localizer["<User claim description 1>"],
                        Group = _localizer["<User claim group 1>"]
                    },
                    new()
                    {
                        Id = default,
                        UserId = Guid.Empty,
                        Type = _localizer["<User claim type 2>"],
                        Value = _localizer["<User claim value 2>"],
                        Description = _localizer["<User claim description 2>"],
                        Group = _localizer["<User claim group 2>"]
                    }
                },
                _localizer["<Message>"]);
        }
    }
}