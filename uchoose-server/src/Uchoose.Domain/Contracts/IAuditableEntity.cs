// ------------------------------------------------------------------------------------------------------
// <copyright file="IAuditableEntity.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.Domain.Contracts
{
    /// <summary>
    /// Отслеживаемая сущность.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    public interface IAuditableEntity<out TEntityId> :
        IAuditableEntity,
        IEntity<TEntityId>
    {
    }

    /// <summary>
    /// Отслеживаемая сущность.
    /// </summary>
    public interface IAuditableEntity :
        IEntity,
        IHasCreatedOn,
        IHasLastModifiedOn
    {
        /// <summary>
        /// Идентификатор пользователя, который создал сущность.
        /// </summary>
        Guid CreatedBy { get; set; }

        /// <summary>
        /// Идентификатор пользователя, который последним изменил сущность.
        /// </summary>
        Guid LastModifiedBy { get; set; }
    }
}