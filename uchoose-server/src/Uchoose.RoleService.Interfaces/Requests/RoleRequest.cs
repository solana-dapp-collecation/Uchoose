// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.RoleService.Interfaces.Requests
{
    /// <summary>
    /// Запрос с данными роли пользователя.
    /// </summary>
    public class RoleRequest :
        IHasName,
        IHasDescription
    {
        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        /// <example>Example</example>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        /// <example>Example</example>
        public string Description { get; set; }
    }
}