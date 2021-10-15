// ------------------------------------------------------------------------------------------------------
// <copyright file="IdentityUsersModel.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Uchoose.Domain.Identity.Entities;

namespace Uchoose.DataAccess.PostgreSql.Identity.Persistence.SeedData.Models
{
    /// <summary>
    /// Модель представления пользователей, которых необходимо добавить при первичной инициализации БД.
    /// </summary>
    internal sealed class IdentityUsersModel
    {
        /// <summary>
        /// Наименование роли пользователя.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Список пользователей.
        /// </summary>
        public List<UchooseUser> Users { get; set; } = new();
    }
}