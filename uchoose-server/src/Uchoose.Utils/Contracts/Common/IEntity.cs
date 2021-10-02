// ------------------------------------------------------------------------------------------------------
// <copyright file="IEntity.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.Utils.Contracts.Common
{
    /// <inheritdoc cref="IEntity"/>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    public interface IEntity<out TEntityId> :
        IEntity,
        IHasId<TEntityId>
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public new TEntityId Id { get; }
    }

    /// <summary>
    /// Сущность.
    /// </summary>
    public interface IEntity
    {
    }
}