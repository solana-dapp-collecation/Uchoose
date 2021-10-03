// ------------------------------------------------------------------------------------------------------
// <copyright file="ModelBuilderExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Uchoose.DataAccess.Interfaces.Settings;
using Uchoose.DataAccess.PostgreSql.Identity.Constants;
using Uchoose.DataAccess.PostgreSql.Identity.Persistence.Configurations;
using Uchoose.DataAccess.PostgreSql.Persistence.Configurations;

namespace Uchoose.DataAccess.PostgreSql.Identity.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="ModelBuilder"/>.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Применить конфигурацию контекста доступа к данным, связанных с Identity.
        /// </summary>
        /// <param name="builder"><see cref="ModelBuilder"/>.</param>
        /// <param name="protectionSettings"><see cref="ProtectionSettings"/>.</param>
        /// <param name="protector"><see cref="IPersonalDataProtector"/>.</param>
        public static void ApplyIdentityConfiguration(
            this ModelBuilder builder,
            ProtectionSettings protectionSettings,
            IPersonalDataProtector protector)
        {
            builder.ApplyConfiguration(new UchooseUserConfiguration(protectionSettings, protector));
            builder.ApplyConfiguration(new UchooseRoleConfiguration());
            builder.ApplyConfiguration(new UchooseRoleClaimConfiguration());
            builder.ApplyConfiguration(new UchooseUserClaimConfiguration());

            builder.Entity<IdentityUserRole<Guid>>(entity =>
            {
                entity.ToTable(PostgreSqlConstants.Schemes.Identity.Tables.UserRolesTableName);

                entity.HasKey(e => new { e.UserId, e.RoleId });
            });
            builder.Entity<IdentityUserLogin<Guid>>(entity =>
            {
                entity.ToTable(PostgreSqlConstants.Schemes.Identity.Tables.UserLoginsTableName);

                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });
            builder.Entity<IdentityUserToken<Guid>>(entity =>
            {
                entity.ToTable(PostgreSqlConstants.Schemes.Identity.Tables.UserTokensTableName);

                entity.HasKey(
                    t => new
                    {
                        t.UserId,
                        t.LoginProvider,
                        t.Name
                    });
            });

            builder.ApplyConfiguration(new UchooseUserExtendedAttributeConfiguration());
            builder.ApplyConfiguration(new UchooseRoleExtendedAttributeConfiguration());
            builder.ApplyConfiguration(new AuditConfiguration());
        }
    }
}