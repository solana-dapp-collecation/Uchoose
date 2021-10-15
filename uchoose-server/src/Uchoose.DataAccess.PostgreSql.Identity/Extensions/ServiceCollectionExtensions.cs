// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uchoose.AuditService.Extensions;
using Uchoose.DataAccess.Identity.Interfaces.Contexts;
using Uchoose.DataAccess.Interfaces;
using Uchoose.DataAccess.Interfaces.Settings;
using Uchoose.DataAccess.PostgreSql.Extensions;
using Uchoose.DataAccess.PostgreSql.Identity.Persistence;
using Uchoose.DataAccess.PostgreSql.Identity.Settings;
using Uchoose.DataAccess.PostgreSql.Protection;
using Uchoose.Domain.Identity.Entities;
using Uchoose.Utils.Extensions;

// ReSharper disable RedundantAssignment

namespace Uchoose.DataAccess.PostgreSql.Identity.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить и настроить хранилище данных, связанных с Identity.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddIdentityPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSettings<ProtectionSettings>(configuration);
            services.AddScoped<IPersonalDataProtector, PersonalDataProtector>();

            services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddDatabaseContext<IdentityDbContext>(configuration)
                .AddScoped<IIdentityDbContext>(provider => provider.GetService<IdentityDbContext>())
                .AddSettings<IdentitySettings>(configuration)
                .AddIdentity<UchooseUser, UchooseRole>(options =>
                {
                    var identitySettings = configuration.GetSettings<IdentitySettings>();
                    options = identitySettings.Options;
                })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddExtendedAttributeDbContexts(typeof(IdentityDbContext), Assembly.GetAssembly(typeof(UchooseUser)));
            services.AddTransient<IDatabaseSeeder, IdentityDbSeeder>();
            services.AddAuditService<IIdentityDbContext, IdentityDbContext>();

            return services;
        }
    }
}