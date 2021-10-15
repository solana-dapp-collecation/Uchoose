// ------------------------------------------------------------------------------------------------------
// <copyright file="Aggregate.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Uchoose.Domain.Contracts;
using Uchoose.Utils.Attributes.Ordering;

namespace Uchoose.Domain.Abstractions
{
    /// <inheritdoc cref="IAggregate"/>
    public abstract class Aggregate<TEntityId> :
        DomainEntity<TEntityId>,
        IAggregate<TEntityId>
    {
        /// <summary>
        /// Инициализирует экземпляр класса, наследующего <see cref="Aggregate{TEntityId}"/>.
        /// </summary>
        protected Aggregate()
        {
            Version = -1;
        }

        /// <inheritdoc cref="IAggregate.Version"/>
        [ExportDefaultOrder(-99)]
        [Display(Name = "Aggregate Version")]
        public int Version { get; protected set; }

        /// <inheritdoc/>
        public void IncrementVersion()
        {
            Version++;
        }

        /// <summary>
        /// Загрузить агрегат.
        /// </summary>
        /// <remarks>
        /// Позволяет восстанавливать состояние агрегата из истории его доменных событий.
        /// </remarks>
        /// <param name="eventsHistory">Коллекция <see cref="IDomainEvent"/>.</param>
        public void Load(IEnumerable<IDomainEvent> eventsHistory)
        {
            foreach (var @event in eventsHistory)
            {
                Apply(@event);
            }
        }

        /// <summary>
        /// Применить доменное событие.
        /// </summary>
        /// <param name="event"><see cref="IDomainEvent"/>.</param>
        protected abstract void Apply(IDomainEvent @event);

        /// <summary>
        /// Применить доменное событие к агрегату.
        /// </summary>
        /// <param name="event">Применяемое доменное событие.</param>
        protected virtual void When(IDomainEvent @event)
        {
            // пустой метод необходим для случаев, когда переопределённых
            // методов нет, или событие не принадлежит сущности
        }
    }

    /// <inheritdoc/>
    public abstract class Aggregate : Aggregate<Guid>
    {
        /// <inheritdoc/>
        public override Guid GenerateNewId()
        {
            return Guid.NewGuid();
        }
    }
}