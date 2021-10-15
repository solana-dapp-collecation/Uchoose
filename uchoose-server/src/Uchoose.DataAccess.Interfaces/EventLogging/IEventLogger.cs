// ------------------------------------------------------------------------------------------------------
// <copyright file="IEventLogger.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Uchoose.Utils.Contracts.Events;

namespace Uchoose.DataAccess.Interfaces.EventLogging
{
    /// <summary>
    /// Логгер событий.
    /// </summary>
    public interface IEventLogger
    {
        /// <summary>
        /// Сохранить событие.
        /// </summary>
        /// <typeparam name="TEvent">Тип события.</typeparam>
        /// <param name="event">Событие.</param>
        /// <param name="changes">Изменения.</param>
        /// <returns>Возвращает <see cref="Task"/>.</returns>
        Task<int> SaveAsync<TEvent>(TEvent @event, (string oldValues, string newValues) changes)
            where TEvent : IEvent;
    }
}