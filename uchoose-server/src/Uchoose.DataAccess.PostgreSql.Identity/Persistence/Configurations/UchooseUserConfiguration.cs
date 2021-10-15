// ------------------------------------------------------------------------------------------------------
// <copyright file="UchooseUserConfiguration.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uchoose.DataAccess.Interfaces.Settings;
using Uchoose.DataAccess.PostgreSql.Identity.Constants;
using Uchoose.DataAccess.PostgreSql.Identity.Extensions;
using Uchoose.Domain.Identity.Entities;

namespace Uchoose.DataAccess.PostgreSql.Identity.Persistence.Configurations
{
    /// <summary>
    /// Конфигурация модели БД <see cref="UchooseUser"/>.
    /// </summary>
    public class UchooseUserConfiguration : IEntityTypeConfiguration<UchooseUser>
    {
        private readonly ProtectionSettings _protectionSettings;
        private readonly IPersonalDataProtector _protector;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UchooseUserConfiguration"/>.
        /// </summary>
        /// <param name="protectionSettings"><see cref="ProtectionSettings"/>.</param>
        /// <param name="protector"><see cref="IPersonalDataProtector"/>.</param>
        public UchooseUserConfiguration(
            ProtectionSettings protectionSettings,
            IPersonalDataProtector protector)
        {
            _protectionSettings = protectionSettings;
            _protector = protector;
        }

        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UchooseUser> entity)
        {
            entity.ToTable(PostgreSqlConstants.Schemes.Identity.Tables.UsersTableName);
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.NormalizedUserName).IsUnique().HasDatabaseName("UserNameIndex");
            entity.HasIndex(e => e.NormalizedEmail).IsUnique().HasDatabaseName("EmailIndex");
            entity.HasIndex(e => e.PhoneNumber).IsUnique().HasDatabaseName("PhoneIndex");
            entity.HasIndex(e => e.ExternalId).IsUnique().HasDatabaseName("ExternalIdIndex");

            entity.Property(e => e.ConcurrencyStamp).IsConcurrencyToken();

            entity.Property(e => e.Id).HasMaxLength(PostgreSql.Constants.PostgreSqlConstants.Schemes.Common.Lengths.GuidStringIdLength);
            entity.Property(e => e.UserName).HasMaxLength(PostgreSqlConstants.Schemes.Identity.Tables.Users.Lengths.UserNameLength);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(PostgreSqlConstants.Schemes.Identity.Tables.Users.Lengths.UserNameLength);
            entity.Property(e => e.Email).HasMaxLength(PostgreSqlConstants.Schemes.Identity.Tables.Users.Lengths.EmailLength);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(PostgreSqlConstants.Schemes.Identity.Tables.Users.Lengths.EmailLength);
            entity.Property(e => e.PhoneNumber).HasMaxLength(PostgreSqlConstants.Schemes.Identity.Tables.Users.Lengths.PhoneNumberLength);
            entity.Property(e => e.ExternalId).HasMaxLength(PostgreSqlConstants.Schemes.Identity.Tables.Users.Lengths.ExternalIdLength);
            entity.Property(e => e.FirstName).HasMaxLength(PostgreSqlConstants.Schemes.Identity.Tables.Users.Lengths.FirstNameLength);
            entity.Property(e => e.LastName).HasMaxLength(PostgreSqlConstants.Schemes.Identity.Tables.Users.Lengths.LastNameLength);
            entity.Property(e => e.MiddleName).HasMaxLength(PostgreSqlConstants.Schemes.Identity.Tables.Users.Lengths.MiddleNameLength);

            entity.ProtectProperties(_protector, _protectionSettings);

            entity.HasMany<UchooseUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
            entity.HasMany<IdentityUserLogin<Guid>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            entity.HasMany<IdentityUserToken<Guid>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
        }
    }
}