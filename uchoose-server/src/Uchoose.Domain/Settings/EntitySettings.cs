// ------------------------------------------------------------------------------------------------------
// <copyright file="EntitySettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Deleting;

namespace Uchoose.Domain.Settings
{
    /// <summary>
    /// Настройки сущностей.
    /// </summary>
    public class EntitySettings :
        ISettings
    {
        /// <summary>
        /// "soft delete" (мягкое удаление) включено.
        /// </summary>
        /// <remarks>
        /// Используется для применения <see cref="ISoftDelete"/>.
        /// </remarks>
        public bool SoftDeleteEnabled { get; set; } = true;
    }
}