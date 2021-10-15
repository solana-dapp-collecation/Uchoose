// ------------------------------------------------------------------------------------------------------
// <copyright file="CurrentUserService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;
using Uchoose.CurrentUserService.Extensions;
using Uchoose.CurrentUserService.Interfaces;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Extensions;

namespace Uchoose.CurrentUserService
{
    /// <inheritdoc cref="ICurrentUserService"/>
    internal sealed class CurrentUserService : ICurrentUserService, IScopedService
    {
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// Инициализирует экземпляр <see cref="CurrentUserService"/>.
        /// </summary>
        /// <param name="accessor"><see cref="IHttpContextAccessor"/>.</param>
        public CurrentUserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <inheritdoc/>
        public string Name => _accessor.HttpContext?.User?.Identity?.Name;

        /// <inheritdoc/>
        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext?.User?.GetUserId() ?? Guid.Empty.ToString()) : Guid.Empty;
        }

        /// <inheritdoc/>
        public string GetUserEmail()
        {
            return IsAuthenticated() ? _accessor.HttpContext?.User?.GetUserEmail() : string.Empty;
        }

        /// <inheritdoc/>
        public bool IsAuthenticated()
        {
            return _accessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }

        /// <inheritdoc/>
        public bool IsInRole(string role)
        {
            return _accessor.HttpContext?.User?.IsInRole(role) ?? false;
        }

        /// <inheritdoc/>
        public IEnumerable<Claim> GetUserClaims()
        {
            return _accessor.HttpContext?.User?.Claims;
        }

        /// <inheritdoc/>
        public HttpContext GetHttpContext()
        {
            return _accessor.HttpContext;
        }

        /// <inheritdoc/>
        public Dictionary<string, string> GetAdditionalPropertiesForLogging()
        {
            var result = new Dictionary<string, string>();

            var userId = GetUserId();
            if (!userId.Equals(default))
            {
                result.Add(nameof(userId), userId.ToString());
            }

            string userEmail = GetUserEmail();
            if (userEmail.IsPresent())
            {
                result.Add(nameof(userEmail), userEmail);
            }

            if (Name.IsPresent())
            {
                result.Add("UserName", Name);
            }

            return result;
        }
    }
}