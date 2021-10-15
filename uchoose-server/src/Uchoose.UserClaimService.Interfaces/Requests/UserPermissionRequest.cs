// ------------------------------------------------------------------------------------------------------
// <copyright file="UserPermissionRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Uchoose.UserClaimService.Interfaces.Models;

namespace Uchoose.UserClaimService.Interfaces.Requests
{
    /// <summary>
    /// Запрос с данными разрешений пользователя.
    /// </summary>
    public class UserPermissionRequest
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public Guid UserId { get; set; }

        /// <summary>
        /// Список данных с разрешениями пользователя.
        /// </summary>
        public IList<UserClaimModel> UserClaims { get; set; }
    }
}