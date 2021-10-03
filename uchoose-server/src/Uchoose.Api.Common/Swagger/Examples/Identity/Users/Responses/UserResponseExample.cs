// ------------------------------------------------------------------------------------------------------
// <copyright file="UserResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
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
    /// Пример ответа для <see cref="UsersController.GetByIdAsync"/>.
    /// </summary>
    public class UserResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<UserResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public UserResponseExample(IStringLocalizer<UserResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<UserResponse>.Success(
                new()
                {
                    Id = Guid.Empty,
                    ExternalId = Guid.Empty.ToString(),
                    Email = "ivanov.ivan@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "+79999999999",
                    PhoneNumberConfirmed = true,
                    UserName = "IvanovIvan",
                    FirstName = _localizer["Ivan"],
                    LastName = _localizer["Ivanov"],
                    IsActive = true,
                    ProfilePictureDataUrl = _localizer["<Profile picture data URL>"]
                },
                _localizer["<Message>"]);
        }
    }
}