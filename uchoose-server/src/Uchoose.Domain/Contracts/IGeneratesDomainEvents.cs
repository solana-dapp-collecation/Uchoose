// ------------------------------------------------------------------------------------------------------
// <copyright file="IGeneratesDomainEvents.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Uchoose.Domain.Contracts
{
    /// <summary>
    /// Генерирует доменные события.
    /// </summary>
    public interface IGeneratesDomainEvents
    {
        /// <summary>
        /// Коллекция доменных событий.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        /// <summary>
        /// Добавить доменное событие.
        /// </summary>
        /// <param name="domainEvent">Доменное событие.</param>
        public void AddDomainEvent(IDomainEvent domainEvent);

        /// <summary>
        /// Удалить доменное событие.
        /// </summary>
        /// <param name="domainEvent">Доменное событие.</param>
        public void RemoveDomainEvent(IDomainEvent domainEvent);

        /// <summary>
        /// Очистить коллекцию доменных событий.
        /// </summary>
        public void ClearDomainEvents();
    }
}