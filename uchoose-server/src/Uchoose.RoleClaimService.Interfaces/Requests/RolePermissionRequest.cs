// ------------------------------------------------------------------------------------------------------
// <copyright file="RolePermissionRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Uchoose.RoleClaimService.Interfaces.Models;

namespace Uchoose.RoleClaimService.Interfaces.Requests
{
    /// <summary>
    /// Запрос с данными разрешений роли пользователя.
    /// </summary>
    public class RolePermissionRequest
    {
        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Список данных с разрешениями роли.
        /// </summary>
        public IList<RoleClaimModel> RoleClaims { get; set; }
    }
}