// ------------------------------------------------------------------------------------------------------
// <copyright file="SoftDeleteExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Utils.Contracts.Deleting;

namespace Uchoose.Utils.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="ISoftDelete"/>
    /// </summary>
    public static class SoftDeleteExtensions
    {
        #region IsSoftDeleted

        /// <summary>
        /// Проверить, является ли <see cref="ISoftDelete"/> сущность удалённой.
        /// </summary>
        /// <param name="entity"><see cref="ISoftDelete"/>.</param>
        /// <returns>Возвращает true, если <see cref="ISoftDelete"/> сущность является удалённой. Иначе - false.</returns>
        public static bool IsSoftDeleted(this ISoftDelete entity)
        {
            return entity.IsDeleted;
        }

        #endregion IsSoftDeleted

        #region SetBySoftDeleted

        /// <summary>
        /// Удалить <see cref="ISoftDelete"/> сущность.
        /// </summary>
        /// <param name="entity"><see cref="ISoftDelete"/>.</param>
        /// <param name="userId">Идентификатор пользователя, который удалил сущность.</param>
        public static void SetBySoftDeleted(this ISoftDelete entity, Guid userId)
        {
            if (userId != Guid.Empty)
            {
                entity.DeletedBy = userId;
            }

            entity.DeletedOn = DateTime.UtcNow;
        }

        #endregion SetBySoftDeleted

        #region SetBySoftUnDeleted

        /// <summary>
        /// Восстановить удалённую <see cref="ISoftDelete"/> сущность.
        /// </summary>
        /// <param name="entity"><see cref="ISoftDelete"/>.</param>
        public static void SetBySoftUnDeleted(this ISoftDelete entity)
        {
            entity.DeletedBy = null;
            entity.DeletedOn = null;
        }

        #endregion SetBySoftUnDeleted
    }
}