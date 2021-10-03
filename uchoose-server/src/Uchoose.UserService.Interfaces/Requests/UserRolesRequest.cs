// ------------------------------------------------------------------------------------------------------
// <copyright file="UserRolesRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Uchoose.UserService.Interfaces.Models;

namespace Uchoose.UserService.Interfaces.Requests
{
    /// <summary>
    /// Запрос со списком ролей пользователя.
    /// </summary>
    public class UserRolesRequest
    {
        /// <summary>
        /// Список ролей пользователя.
        /// </summary>
        public List<UserRoleModel> UserRoles { get; set; } = new();
    }
}