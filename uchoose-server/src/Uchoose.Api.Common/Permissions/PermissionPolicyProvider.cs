// ------------------------------------------------------------------------------------------------------
// <copyright file="PermissionPolicyProvider.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Uchoose.Application.Constants.Permission;

namespace Uchoose.Api.Common.Permissions
{
    /// <summary>
    /// Провайдер политик разрешений.
    /// </summary>
    internal class PermissionPolicyProvider :
        IAuthorizationPolicyProvider
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="PermissionPolicyProvider"/>.
        /// </summary>
        /// <param name="options"><see cref="AuthorizationOptions"/>.</param>
        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new(options);
        }

        /// <summary>
        /// Провайдер политики авторизации.
        /// </summary>
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        /// <inheritdoc/>
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        /// <inheritdoc/>
        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(ApplicationClaimTypes.Permission, StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirement(policyName));
                return Task.FromResult(policy.Build());
            }

            return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }

        /// <inheritdoc/>
        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy>(null);
    }
}