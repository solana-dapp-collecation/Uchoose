// ------------------------------------------------------------------------------------------------------
// <copyright file="EntityExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

using Uchoose.Utils.Attributes.Ordering;
using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Utils.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IEntity"/>.
    /// </summary>
    public static class EntityExtensions
    {
        #region GetImportExportOrderAttributeValue

        /// <summary>
        /// Получить значение относительного порядка из атрибута <see cref="ExportDefaultOrderAttribute"/> свойства.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entity">Сущность.</param>
        /// <param name="propertyName">Имя свойства сущности.</param>
        /// <returns>Возвращает значение относительного порядка из атрибута <see cref="ExportDefaultOrderAttribute"/>.</returns>
        public static int GetImportExportOrderAttributeValue<TEntity>(this TEntity entity, string propertyName)
            where TEntity : IEntity
        {
            return entity.GetType().GetProperty(propertyName)?.GetCustomAttribute<ExportDefaultOrderAttribute>()?.Value ?? 1;
        }

        #endregion GetImportExportOrderAttributeValue

        #region HasDefaultId

        /// <summary>
        /// Проверить, содержит ли сущность идентификатор со значением по умолчанию.
        /// </summary>
        /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
        /// <param name="entity">Проверяемая сущность.</param>
        /// <returns>Возвращает true, если сущность содержит идентификатор со значением по умолчанию. Иначе - false.</returns>
        public static bool HasDefaultId<TEntityId>(this IEntity<TEntityId> entity)
        {
            if (EqualityComparer<TEntityId>.Default.Equals(entity.Id, default))
            {
                return true;
            }

            // workaround для EF Core, когда выставляет минимальные значения при присоединении к контексту доступа к данным
            if (typeof(TEntityId) == typeof(int))
            {
                return Convert.ToInt32(entity.Id) <= 0;
            }

            if (typeof(TEntityId) == typeof(long))
            {
                return Convert.ToInt64(entity.Id) <= 0;
            }

            return false;
        }

        #endregion HasDefaultId
    }
}