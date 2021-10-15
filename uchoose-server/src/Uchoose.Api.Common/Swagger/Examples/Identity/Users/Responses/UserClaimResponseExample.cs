// ------------------------------------------------------------------------------------------------------
// <copyright file="UserClaimResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Identity;
using Uchoose.UserClaimService.Interfaces.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Identity.Users.Responses
{
    /// <summary>
    /// Пример ответа для <see cref="UsersController.GetClaimByIdAsync"/>.
    /// </summary>
    public class UserClaimResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<UserClaimResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserClaimResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public UserClaimResponseExample(IStringLocalizer<UserClaimResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<UserClaimResponse>.Success(
                new()
                {
                    Id = default,
                    UserId = Guid.Empty,
                    Type = _localizer["<User claim type>"],
                    Value = _localizer["<User claim value>"],
                    Description = _localizer["<User claim description>"],
                    Group = _localizer["<User claim group>"]
                },
                _localizer["<Message>"]);
        }
    }
}