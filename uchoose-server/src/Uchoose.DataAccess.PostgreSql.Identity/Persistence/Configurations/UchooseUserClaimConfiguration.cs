// ------------------------------------------------------------------------------------------------------
// <copyright file="UchooseUserClaimConfiguration.cs" company="Life Loop">
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
    /// Конфигурация модели БД <see cref="UchooseUserClaim"/>.
    /// </summary>
    public class UchooseUserClaimConfiguration : IEntityTypeConfiguration<UchooseUserClaim>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UchooseUserClaim> entity)
        {
            entity.ToTable(PostgreSqlConstants.Schemes.Identity.Tables.UserClaimsTableName);

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Description).HasMaxLength(PostgreSqlConstants.Schemes.Identity.Tables.UserClaims.Lengths.DescriptionLength);
            entity.Property(e => e.Group).HasMaxLength(PostgreSqlConstants.Schemes.Identity.Tables.UserClaims.Lengths.GroupLength);

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserClaims)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}