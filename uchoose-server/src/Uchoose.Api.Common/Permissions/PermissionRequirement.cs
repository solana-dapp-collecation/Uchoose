// ------------------------------------------------------------------------------------------------------
// <copyright file="PermissionRequirement.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.AspNetCore.Authorization;

namespace Uchoose.Api.Common.Permissions
{
    /// <summary>
    /// Представляет требование для авторизации.
    /// </summary>
    internal class PermissionRequirement :
        IAuthorizationRequirement
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="PermissionRequirement"/>.
        /// </summary>
        /// <param name="permission">Разрешение.</param>
        public PermissionRequirement(string permission)
        {
            Permission = permission ?? throw new ArgumentNullException(nameof(permission));
        }

        /// <summary>
        /// Разрешение.
        /// </summary>
        public string Permission { get; private set; }
    }
}