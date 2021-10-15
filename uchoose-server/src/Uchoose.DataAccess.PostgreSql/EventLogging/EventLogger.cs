// ------------------------------------------------------------------------------------------------------
// <copyright file="EventLogger.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Uchoose.CurrentUserService.Interfaces;
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.DataAccess.Interfaces.EventLogging;
using Uchoose.Domain.Entities;
using Uchoose.SerializationService.Interfaces;
using Uchoose.Utils.Contracts.Events;

namespace Uchoose.DataAccess.PostgreSql.EventLogging
{
    /// <inheritdoc cref="IEventLogger"/>.
    internal class EventLogger :
        IEventLogger
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ILoggableDbContext _context;
        private readonly IJsonSerializer _jsonSerializer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="EventLogger"/>.
        /// </summary>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        /// <param name="context"><see cref="ILoggableDbContext"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        public EventLogger(
            ICurrentUserService currentUserService,
            ILoggableDbContext context,
            IJsonSerializer jsonSerializer)
        {
            _currentUserService = currentUserService;
            _context = context;
            _jsonSerializer = jsonSerializer;
        }

        /// <inheritdoc/>
        public async Task<int> SaveAsync<TEvent>(TEvent @event, (string oldValues, string newValues) changes)
            where TEvent : IEvent
        {
            if (@event is EventLog eventLog)
            {
                await _context.EventLogs.AddAsync(eventLog);
                return await _context.SaveChangesAsync();
            }
            else
            {
                string serializedData = _jsonSerializer.Serialize(@event, @event.GetType());

                string userEmail = _currentUserService.GetUserEmail();
                if (string.IsNullOrWhiteSpace(userEmail))
                {
                    userEmail = "Anonymous";
                }

                var userId = _currentUserService.GetUserId();
                var thisEvent = new EventLog(
                    @event,
                    serializedData,
                    changes,
                    string.IsNullOrWhiteSpace(userEmail) ? _currentUserService.Name : userEmail,
                    userId);
                await _context.EventLogs.AddAsync(thisEvent);
                return await _context.SaveChangesAsync();
            }
        }
    }
}