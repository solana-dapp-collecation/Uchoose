// ------------------------------------------------------------------------------------------------------
// <copyright file="IDomainEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Uchoose.Utils.Contracts.Events;

namespace Uchoose.Domain.Contracts
{
    /// <inheritdoc/>
    /// <typeparam name="TEntity">Тип сущности доменного события.</typeparam>
    // ReSharper disable once UnusedTypeParameter
    public interface IDomainEvent<TEntity> :
        IDomainEvent
        where TEntity : IDomainEntity
    {
    }

    /// <summary>
    /// Доменное событие.
    /// </summary>
    public interface IDomainEvent :
        IEvent
    {
        /// <summary>
        /// Связанные с событием сущности.
        /// </summary>
        /// <remarks>
        /// Используется для записи отслеживаемых изменений в логах событий.
        /// Необходимо хранить в этом свойстве типы сущностей, изменения в которых будут отражаться в событии.
        /// </remarks>
        public IEnumerable<Type> RelatedEntities { get; }

        /// <summary>
        /// Текущая версия агрегата.
        /// </summary>
        /// <remarks>
        /// Присваивается в случае, когда доменное событие было вызвано агрегатом.
        /// </remarks>
        public int? AggregateVersion { get; }
    }
}