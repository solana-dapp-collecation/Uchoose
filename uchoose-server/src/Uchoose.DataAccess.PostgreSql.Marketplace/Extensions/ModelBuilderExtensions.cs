// ------------------------------------------------------------------------------------------------------
// <copyright file="ModelBuilderExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Uchoose.DataAccess.PostgreSql.Marketplace.Persistence.Configurations;
using Uchoose.DataAccess.PostgreSql.Persistence.Configurations;

namespace Uchoose.DataAccess.PostgreSql.Marketplace.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="ModelBuilder"/>.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Применить конфигурацию контекста доступа к данным, связанных с маркетплейсом NFT.
        /// </summary>
        /// <param name="builder"><see cref="ModelBuilder"/>.</param>
        public static void ApplyMarketplaceConfiguration(this ModelBuilder builder)
        {
            // TODO - добавить другие конфигурации

            builder.ApplyConfiguration(new NftImageLayerTypeConfiguration());
            builder.ApplyConfiguration(new AuditConfiguration());
        }
    }
}