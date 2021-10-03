// ------------------------------------------------------------------------------------------------------
// <copyright file="CacheKeys.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Extensions;

namespace Uchoose.Utils.Constants.Caching
{
    /// <summary>
    /// Константы ключей кэша.
    /// </summary>
    public static class CacheKeys
    {
        /// <summary>
        /// Общие.
        /// </summary>
        public static class Common
        {
            /// <summary>
            /// Получить ключ кэша, по которому хранится значение с сущностью.
            /// </summary>
            /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
            /// <typeparam name="TEntity">Тип сущности.</typeparam>
            /// <param name="id">Идентификатор сущности.</param>
            /// <returns>Возвращает ключ кэша, по которому хранится значение с сущностью.</returns>
            public static string GetEntityByIdCacheKey<TEntityId, TEntity>(TEntityId id)
                where TEntity : class, IEntity<TEntityId>
            {
                return $"GetEntity-{typeof(TEntity).GetGenericTypeName()}-{id}";
            }

            ///// <summary>
            ///// Получить ключ кэша, по которому хранится значение с набором сущностей.
            ///// </summary>
            ///// <param name="payload">Дополнительная строка (например, сериализованный фильтр из запроса).</param>
            ///// <returns>Возвращает ключ кэша, по которому хранится значение с набором сущностей.</returns>
            // public static string GetEntitiesCacheKey<TEntityId, TEntity>(string payload) where TEntity : class, IEntity<TEntityId>
            // {
            //    return $"GetEntities-{typeof(TEntity).GetGenericTypeName()}-{payload}";
            // }
        }
    }
}