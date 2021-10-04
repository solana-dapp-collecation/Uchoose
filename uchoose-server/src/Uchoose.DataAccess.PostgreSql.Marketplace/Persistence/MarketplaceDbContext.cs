// ------------------------------------------------------------------------------------------------------
// <copyright file="MarketplaceDbContext.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Uchoose.CurrentUserService.Interfaces;
using Uchoose.DataAccess.Interfaces.EventLogging;
using Uchoose.DataAccess.Marketplace.Interfaces.Contexts;
using Uchoose.DataAccess.PostgreSql.Marketplace.Constants;
using Uchoose.DataAccess.PostgreSql.Marketplace.Extensions;
using Uchoose.DataAccess.PostgreSql.Persistence.Abstractions;
using Uchoose.DateTimeService.Interfaces;
using Uchoose.Domain.Marketplace.Entities;
using Uchoose.Domain.Settings;
using Uchoose.SerializationService.Interfaces;

namespace Uchoose.DataAccess.PostgreSql.Marketplace.Persistence
{
    /// <inheritdoc cref="IMarketplaceDbContext"/>
    internal sealed class MarketplaceDbContext :
        AuditableDbContext,
        IMarketplaceDbContext
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="MarketplaceDbContext"/>.
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions"/>.</param>
        /// <param name="eventLogger"><see cref="IEventLogger"/>.</param>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        /// <param name="mediator"><see cref="IMediator"/>.</param>
        /// <param name="dateTimeService"><see cref="IDateTimeService"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        /// <param name="entitySettings"><see cref="EntitySettings"/>.</param>
        public MarketplaceDbContext(
            DbContextOptions<MarketplaceDbContext> options,
            IEventLogger eventLogger,
            ICurrentUserService currentUserService,
            IMediator mediator,
            IDateTimeService dateTimeService,
            IJsonSerializer jsonSerializer,
            IOptionsSnapshot<EntitySettings> entitySettings)
                : base(options, eventLogger, currentUserService, mediator, dateTimeService, jsonSerializer, entitySettings)
        {
        }

        /// <inheritdoc/>
        public DbSet<NftImageLayer> NftImageLayers { get; set; }

        /// <inheritdoc/>
        public DbSet<NftImageLayerType> NftImageLayerTypes { get; set; }

        /// <inheritdoc/>
        protected override string Schema => nameof(PostgreSqlConstants.Schemes.Marketplace);

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(Schema);
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            builder.ApplyMarketplaceConfiguration();
        }
    }
}