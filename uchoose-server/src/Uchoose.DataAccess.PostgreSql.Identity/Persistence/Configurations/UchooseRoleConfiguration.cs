// ------------------------------------------------------------------------------------------------------
// <copyright file="UchooseRoleConfiguration.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uchoose.DataAccess.PostgreSql.Identity.Constants;
using Uchoose.Domain.Identity.Entities;

namespace Uchoose.DataAccess.PostgreSql.Identity.Persistence.Configurations
{
    /// <summary>
    /// Конфигурация модели БД <see cref="UchooseRole"/>.
    /// </summary>
    public class UchooseRoleConfiguration : IEntityTypeConfiguration<UchooseRole>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UchooseRole> entity)
        {
            entity.ToTable(PostgreSqlConstants.Schemes.Identity.Tables.RolesTableName);

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasMaxLength(PostgreSql.Constants.PostgreSqlConstants.Schemes.Common.Lengths.GuidStringIdLength);
            entity.Property(e => e.Description).HasMaxLength(PostgreSqlConstants.Schemes.Identity.Tables.Roles.Lengths.DescriptionLength);
        }
    }
}