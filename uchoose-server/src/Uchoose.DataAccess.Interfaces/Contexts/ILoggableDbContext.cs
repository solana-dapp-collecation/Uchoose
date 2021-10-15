// ------------------------------------------------------------------------------------------------------
// <copyright file="ILoggableDbContext.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Uchoose.Domain.Entities;

namespace Uchoose.DataAccess.Interfaces.Contexts
{
    /// <summary>
    /// Контекст доступа к логируемым данным.
    /// </summary>
    public interface ILoggableDbContext :
        IDbContext
    {
        /// <summary>
        /// Коллекция логов доменных событий.
        /// </summary>
        public DbSet<EventLog> EventLogs { get; set; }
    }
}