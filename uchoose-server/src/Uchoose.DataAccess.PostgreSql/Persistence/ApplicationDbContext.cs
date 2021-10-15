// ------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationDbContext.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Data;

using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.Domain.Entities;

namespace Uchoose.DataAccess.PostgreSql.Persistence
{
    /// <inheritdoc cref="IApplicationDbContext"/>
    internal sealed class ApplicationDbContext :
        DbContext,
        IApplicationDbContext,
        IDataProtectionKeyContext
    {
        /// <summary>
        /// Схема БД, используемая по умолчанию.
        /// </summary>
        private const string Schema = "Application";

        /// <summary>
        /// Инициализирует экземпляр <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions"/>.</param>
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <inheritdoc/>
        public IDbConnection Connection => Database.GetDbConnection();

        /// <inheritdoc/>
        public bool HasChanges => ChangeTracker.HasChanges();

        /// <inheritdoc/>
        public DbSet<EventLog> EventLogs { get; set; }

        /// <summary>
        /// Коллекция ключей для защиты данных.
        /// </summary>
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);

            // modelBuilder.ApplyApplicationConfiguration();
        }
    }
}