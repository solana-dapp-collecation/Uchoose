// ------------------------------------------------------------------------------------------------------
// <copyright file="UchooseRoleClaimConfiguration.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uchoose.DataAccess.PostgreSql.Identity.Constants;
using Uchoose.Domain.Identity.Entities;

namespace Uchoose.DataAccess.PostgreSql.Identity.Persistence.Configurations
{
    /// <summary>
    /// Конфигурация модели БД <see cref="UchooseRoleClaim"/>.
    /// </summary>
    public class UchooseRoleClaimConfiguration : IEntityTypeConfiguration<UchooseRoleClaim>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UchooseRoleClaim> entity)
        {
            entity.ToTable(PostgreSqlConstants.Schemes.Identity.Tables.RoleClaimsTableName);

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Description).HasMaxLength(PostgreSqlConstants.Schemes.Identity.Tables.RoleClaims.Lengths.DescriptionLength);
            entity.Property(e => e.Group).HasMaxLength(PostgreSqlConstants.Schemes.Identity.Tables.RoleClaims.Lengths.GroupLength);

            entity.HasOne(d => d.Role)
                .WithMany(p => p.RoleClaims)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}