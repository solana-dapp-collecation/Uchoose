// ------------------------------------------------------------------------------------------------------
// <copyright file="ToggleUserStatusRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.UserService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на изменение статуса пользователя.
    /// </summary>
    public class ToggleUserStatusRequest
    {
        /// <summary>
        /// Активировать пользователя.
        /// </summary>
        public bool ActivateUser { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }
    }
}