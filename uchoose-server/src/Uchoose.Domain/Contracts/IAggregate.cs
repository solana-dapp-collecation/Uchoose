// ------------------------------------------------------------------------------------------------------
// <copyright file="IAggregate.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.Domain.Contracts
{
    /// <inheritdoc cref="IAggregate"/>
    /// <typeparam name="TAggregateId">Тип идентификатора агрегата.</typeparam>
    public interface IAggregate<out TAggregateId> :
        IAggregate,
        IEntity<TAggregateId>
    {
        /// <summary>
        /// Идентификатор агрегата.
        /// </summary>
        public new TAggregateId Id { get; }
    }

    /// <summary>
    /// Агрегат.
    /// </summary>
    public interface IAggregate :
        IHasVersion
    {
        /// <summary>
        /// Текущая версия агрегата.
        /// </summary>
        new int Version { get; }

        /// <summary>
        /// Увеличить версию агрегата на единицу.
        /// </summary>
        void IncrementVersion();
    }
}