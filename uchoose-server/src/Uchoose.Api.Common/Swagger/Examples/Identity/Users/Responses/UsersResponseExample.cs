// ------------------------------------------------------------------------------------------------------
// <copyright file="UsersResponseExample.cs" company="Life Loop">
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
using Uchoose.UserService.Interfaces.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Identity.Users.Responses
{
    /// <summary>
    /// Пример ответа для <see cref="UsersController.GetAllAsync"/>.
    /// </summary>
    public class UsersResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<UsersResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UsersResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public UsersResponseExample(IStringLocalizer<UsersResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<List<UserResponse>>.Success(
                new()
                {
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
                    new()
                    {
                        Id = Guid.Empty,
                        ExternalId = Guid.Empty.ToString(),
                        Email = "petrov.petr@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+78888888888",
                        PhoneNumberConfirmed = true,
                        UserName = "PetrovPetr",
                        FirstName = _localizer["Petr"],
                        LastName = _localizer["Petrov"],
                        IsActive = true,
                        ProfilePictureDataUrl = _localizer["<Profile picture data URL>"]
                    }
                },
                _localizer["<Message>"]);
        }
    }
}