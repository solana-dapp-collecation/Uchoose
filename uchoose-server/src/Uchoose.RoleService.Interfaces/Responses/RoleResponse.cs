// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Domain.Identity.Entities;
using Uchoose.Utils.Attributes.Mappings;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.RoleService.Interfaces.Responses
{
    /// <summary>
    /// Данные роли пользователя.
    /// </summary>
    [UseReverseMap(typeof(UchooseRole))]
    public class RoleResponse :
        IMapFromTo<UchooseRole, RoleResponse>,
        IHasName,
        IHasDescription
    {
        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }
    }
}