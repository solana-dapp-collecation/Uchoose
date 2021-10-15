// ------------------------------------------------------------------------------------------------------
// <copyright file="IAuditableDbContext.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Uchoose.Domain.Entities;

namespace Uchoose.DataAccess.Interfaces.Contexts
{
    /// <summary>
    /// Контекст доступа к данным аудита.
    /// </summary>
    public interface IAuditableDbContext :
        IDbContext
    {
        /// <summary>
        /// <see cref="ChangeTracker"/>.
        /// </summary>
        public ChangeTracker ChangeTracker { get; }

        /// <summary>
        /// Коллекция данных аудита сущностей.
        /// </summary>
        public DbSet<Audit> AuditTrails { get; set; }

        /// <summary>
        /// Сохранить изменения асинхронно с помощью метода базового класса.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Возвращает результат операции.</returns>
        public Task<int> BaseSaveChangesAsync(CancellationToken cancellationToken = new());
    }
}