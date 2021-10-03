// ------------------------------------------------------------------------------------------------------
// <copyright file="UchooseUserExtendedAttributeConfiguration.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uchoose.DataAccess.PostgreSql.Identity.Constants;
using Uchoose.Domain.Identity.Entities.ExtendedAttributes;

namespace Uchoose.DataAccess.PostgreSql.Identity.Persistence.Configurations
{
    /// <summary>
    /// Конфигурация модели БД <see cref="UchooseUserExtendedAttribute"/>.
    /// </summary>
    public class UchooseUserExtendedAttributeConfiguration : IEntityTypeConfiguration<UchooseUserExtendedAttribute>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UchooseUserExtendedAttribute> entity)
        {
            entity.ToTable(PostgreSqlConstants.Schemes.Identity.Tables.ExtendedAttributes.UserExtendedAttributesTableName);

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Key).HasMaxLength(PostgreSql.Constants.PostgreSqlConstants.Schemes.Common.Tables.ExtendedAttributes.Lengths.KeyLength);
            entity.Property(e => e.Description).HasMaxLength(PostgreSql.Constants.PostgreSqlConstants.Schemes.Common.Tables.ExtendedAttributes.Lengths.DescriptionLength);
            entity.Property(e => e.Group).HasMaxLength(PostgreSql.Constants.PostgreSqlConstants.Schemes.Common.Tables.ExtendedAttributes.Lengths.GroupLength);
            entity.Property(e => e.ExternalId).HasMaxLength(PostgreSql.Constants.PostgreSqlConstants.Schemes.Common.Tables.ExtendedAttributes.Lengths.ExternalIdLength);
        }
    }
}