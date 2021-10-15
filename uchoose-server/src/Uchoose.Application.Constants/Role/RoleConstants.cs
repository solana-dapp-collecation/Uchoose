// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleConstants.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace Uchoose.Application.Constants.Role
{
    /// <summary>
    /// Константы ролей пользователей.
    /// </summary>
    public static class RoleConstants
    {
        /// <summary>
        /// Главный администратор.
        /// </summary>
        [Description("Главный администратор")]
        public const string SuperAdmin = nameof(SuperAdmin);

        /// <summary>
        /// Администратор.
        /// </summary>
        [Description("Администратор")]
        public const string Admin = nameof(Admin);

        /// <summary>
        /// Менеджер.
        /// </summary>
        [Description("Менеджер")]
        public const string Manager = nameof(Manager);

        /// <summary>
        /// Художник.
        /// </summary>
        [Description("Художник")]
        public const string Artist = nameof(Artist);

        /// <summary>
        /// Партнёр.
        /// </summary>
        [Description("Партнёр")]
        public const string Partner = nameof(Partner);
    }
}