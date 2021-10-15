// ------------------------------------------------------------------------------------------------------
// <copyright file="IExtendedAttributeDbContext.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Uchoose.Domain.Abstractions;
using Uchoose.Utils.Contracts.Common;

namespace Uchoose.DataAccess.Interfaces.Contexts
{
    /// <summary>
    /// Контекст доступа к данным расширенных атрибутов сущности.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    /// <typeparam name="TExtendedAttribute">Тип расширенного атрибута сущности.</typeparam>
    public interface IExtendedAttributeDbContext<TEntityId, TEntity, TExtendedAttribute> : IDbContext
        where TEntity : class, IEntity<TEntityId>
        where TExtendedAttribute : ExtendedAttribute<TEntityId, TEntity>
    {
        /// <summary>
        /// Коллекция данных сущностей.
        /// </summary>
        [NotMapped]
        public DbSet<TEntity> Entities => GetEntities();

        /// <summary>
        /// Коллекция данных расширенных атрибутов.
        /// </summary>
        public DbSet<TExtendedAttribute> ExtendedAttributes { get; set; }

        /// <summary>
        /// Получить коллекцию данных сущностей.
        /// </summary>
        /// <returns>Возвращает коллекцию данных сущностей.</returns>
        protected DbSet<TEntity> GetEntities();
    }
}