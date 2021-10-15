// ------------------------------------------------------------------------------------------------------
// <copyright file="RolePermissionsResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Uchoose.RoleClaimService.Interfaces.Models;

namespace Uchoose.RoleClaimService.Interfaces.Responses
{
    /// <summary>
    /// Ответ со списком разрешений роли.
    /// </summary>
    public class RolePermissionsResponse
    {
        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Наименование роли.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Список разрешений роли.
        /// </summary>
        public List<RoleClaimModel> RoleClaims { get; set; }
    }
}