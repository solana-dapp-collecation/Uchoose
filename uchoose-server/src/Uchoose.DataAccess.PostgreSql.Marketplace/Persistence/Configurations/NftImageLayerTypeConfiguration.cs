// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerTypeConfiguration.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uchoose.Domain.Marketplace.Entities;

namespace Uchoose.DataAccess.PostgreSql.Marketplace.Persistence.Configurations
{
    /// <summary>
    /// Конфигурация модели БД <see cref="NftImageLayerType"/>.
    /// </summary>
    public class NftImageLayerTypeConfiguration :
        IEntityTypeConfiguration<NftImageLayerType>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<NftImageLayerType> entity)
        {
        }
    }
}