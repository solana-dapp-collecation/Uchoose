// ------------------------------------------------------------------------------------------------------
// <copyright file="IMarketplaceDbContext.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Uchoose.DataAccess.Interfaces;
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.Domain.Marketplace.Entities;

namespace Uchoose.DataAccess.Marketplace.Interfaces.Contexts
{
    /// <summary>
    /// Контекст доступа к данным, связанным с маркетплейсом NFT.
    /// </summary>
    public interface IMarketplaceDbContext :
        IAuditableDbContext,
        IDbContextInterface
    {
        /// <summary>
        /// Коллекция слоёв изображения NFT.
        /// </summary>
        public DbSet<NftImageLayer> NftImageLayers { get; set; }

        /// <summary>
        /// Коллекция типов слоя изображения NFT.
        /// </summary>
        public DbSet<NftImageLayerType> NftImageLayerTypes { get; set; }
    }
}