// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uchoose.AuditService.Extensions;
using Uchoose.DataAccess.Interfaces;
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.DataAccess.Marketplace.Interfaces.Contexts;
using Uchoose.DataAccess.PostgreSql.Extensions;
using Uchoose.DataAccess.PostgreSql.Marketplace.Persistence;
using Uchoose.Domain.Marketplace.Entities;

namespace Uchoose.DataAccess.PostgreSql.Marketplace.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить хранилище данных, связанных с маркетплейсом NFT.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddMarketplacePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddDatabaseContext<MarketplaceDbContext>(configuration)
                .AddScoped<IAuditableDbContext>(provider => provider.GetService<MarketplaceDbContext>())
                .AddScoped<IMarketplaceDbContext>(provider => provider.GetService<MarketplaceDbContext>());
            services.AddExtendedAttributeDbContexts(typeof(MarketplaceDbContext), Assembly.GetAssembly(typeof(NftImageLayer)));
            services.AddTransient<IDatabaseSeeder, MarketplaceDbSeeder>();
            services.AddAuditService<IMarketplaceDbContext, MarketplaceDbContext>();

            return services;
        }
    }
}