// ------------------------------------------------------------------------------------------------------
// <copyright file="UserRoleModel.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.UserService.Interfaces.Models
{
    /// <summary>
    /// Модель роли пользователя.
    /// </summary>
    public class UserRoleModel
    {
        /// <summary>
        /// Идентификатор роли пользователя.
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Наименование роли пользователя.
        /// </summary>
        /// <example>Example</example>
        public string RoleName { get; set; }

        /// <summary>
        /// Роль добавлена к пользователю.
        /// </summary>
        /// <example>false</example>
        public bool Selected { get; set; }
    }
}