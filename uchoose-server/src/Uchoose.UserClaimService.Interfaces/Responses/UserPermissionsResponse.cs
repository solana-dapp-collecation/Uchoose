// ------------------------------------------------------------------------------------------------------
// <copyright file="UserPermissionsResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Uchoose.UserClaimService.Interfaces.Models;

namespace Uchoose.UserClaimService.Interfaces.Responses
{
    /// <summary>
    /// Ответ со списком разрешений роли.
    /// </summary>
    public class UserPermissionsResponse
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// UserName пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Список разрешений пользователя.
        /// </summary>
        public List<UserClaimModel> UserClaims { get; set; }
    }
}