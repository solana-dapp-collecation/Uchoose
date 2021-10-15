// ------------------------------------------------------------------------------------------------------
// <copyright file="AuditConfiguration.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uchoose.DataAccess.PostgreSql.Constants;
using Uchoose.Domain.Entities;

namespace Uchoose.DataAccess.PostgreSql.Persistence.Configurations
{
    /// <summary>
    /// Конфигурация модели БД <see cref="Audit"/>.
    /// </summary>
    public class AuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Audit> entity)
        {
            entity.ToTable(PostgreSqlConstants.Schemes.Common.Tables.AuditTrailsTableName);

            entity.HasKey(e => e.Id);

            entity.Property(e => e.EntityName).HasMaxLength(PostgreSqlConstants.Schemes.Common.Tables.AuditTrails.Lengths.EntityNameLength);
            entity.Property(e => e.Type).HasMaxLength(PostgreSqlConstants.Schemes.Common.Tables.AuditTrails.Lengths.TypeLength);
        }
    }
}