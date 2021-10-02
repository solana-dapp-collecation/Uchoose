// ------------------------------------------------------------------------------------------------------
// <copyright file="UserConstants.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Application.Constants.User
{
    /// <summary>
    /// Константы пользователей.
    /// </summary>
    public static class UserConstants
    {
        // TODO - вынести в appsettings

        /// <summary>
        /// Пароль по умолчанию.
        /// </summary>
        public const string DefaultPassword = "123Pa$$word!";

        /// <summary>
        /// Идентификатор пользователя системы.
        /// </summary>
        /// <remarks>
        /// Используется, например, при создании новых пользователей.
        /// </remarks>
        public const string SystemUserId = "A73714E3-63E8-497C-9D60-310BC1EE8120";
    }
}