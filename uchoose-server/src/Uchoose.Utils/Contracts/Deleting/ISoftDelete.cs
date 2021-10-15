// ------------------------------------------------------------------------------------------------------
// <copyright file="ISoftDelete.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.Utils.Contracts.Deleting
{
    /// <summary>
    /// Интерфейс, указывающий на то, что сущность является "soft delete" (мягкое удаление).
    /// </summary>
    /// <remarks>
    /// Такая сущность не будет фактически удалена, а будет помечена как удалённая.
    /// </remarks>
    public interface ISoftDelete :
        IHasDeletedOn
    {
        /// <summary>
        /// Идентификатор пользователя, который удалил сущность.
        /// </summary>
        public Guid? DeletedBy { get; set; }
    }
}