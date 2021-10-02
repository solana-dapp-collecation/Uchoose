// ------------------------------------------------------------------------------------------------------
// <copyright file="IVersionableByEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Domain.Contracts
{
    /// <inheritdoc cref="IVersionableByEvent"/>
    /// <typeparam name="TAggregateId">Тип идентификатора агрегата.</typeparam>
    /// <typeparam name="TAggregate">Тип агрегата.</typeparam>
    /// <typeparam name="TEvent">Тип события.</typeparam>
    public interface IVersionableByEvent<TAggregateId, out TAggregate, in TEvent> :
        IVersionableByEvent
        where TAggregate : class, IAggregate<TAggregateId>, IVersionableByEvent<TAggregateId, TAggregate, TEvent>
        where TEvent : class, IDomainEvent, new()
    {
        /// <summary>
        /// Применить доменное событие к агрегату.
        /// </summary>
        /// <param name="event">Применяемое доменное событие.</param>
        public void When(TEvent @event);
    }

    /// <summary>
    /// Агрегат, версионируемый в зависимости от доменного события.
    /// </summary>
    public interface IVersionableByEvent
    {
    }
}