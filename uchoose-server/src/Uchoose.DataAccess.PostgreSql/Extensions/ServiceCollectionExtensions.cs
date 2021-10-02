// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.DataAccess.Interfaces.EventLogging;
using Uchoose.DataAccess.Interfaces.Settings;
using Uchoose.DataAccess.PostgreSql.EventLogging;
using Uchoose.DataAccess.PostgreSql.Persistence;
using Uchoose.Domain.Abstractions;
using Uchoose.Utils.Extensions;

namespace Uchoose.DataAccess.PostgreSql.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region AddApplicationPersistence

        /// <summary>
        /// Добавить хранилище данных, связанных с приложением.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddApplicationPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDatabaseContext<ApplicationDbContext>(configuration)
                .AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>())
                .AddScoped<ILoggableDbContext>(provider => provider.GetService<ApplicationDbContext>())
                .AddScoped<IEventLogger, EventLogger>()
                .AddUchooseDataProtection(configuration);
        }

        #endregion AddApplicationPersistence

        #region AddDatabaseContext

        /// <summary>
        /// Добавить контекст доступа к данным БД.
        /// </summary>
        /// <typeparam name="TDbContext">Тип контекста доступа к данным.</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddDatabaseContext<TDbContext>(this IServiceCollection services, IConfiguration configuration)
            where TDbContext : DbContext
        {
            return services.AddPostgreSql<TDbContext>(configuration);
        }

        #endregion AddDatabaseContext

        #region AddExtendedAttributeDbContexts

        /// <summary>
        /// Добавить контексты доступа к данным расширенных атрибутов.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="implementationType">Тип контекста доступа к данным.</param>
        /// <param name="assemblies">Сборки с классами расширенных атрибутов, которые используются для построения контекста доступа к данным.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddExtendedAttributeDbContexts(this IServiceCollection services, Type implementationType, params Assembly[] assemblies)
        {
            var extendedAttributeTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                    .Select(t => new
                    {
                        BaseGenericType = t.BaseType,
                        CurrentType = t
                    })
                    .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(ExtendedAttribute<,>)))

                .ToList();

            foreach (var extendedAttributeType in extendedAttributeTypes)
            {
                var extendedAttributeTypeGenericArguments = extendedAttributeType.BaseGenericType.GetGenericArguments().ToList();
                var serviceType = typeof(IExtendedAttributeDbContext<,,>).MakeGenericType(extendedAttributeTypeGenericArguments[0], extendedAttributeTypeGenericArguments[1], extendedAttributeType.CurrentType);
                services.AddScoped(serviceType, provider => provider.GetService(implementationType));
            }

            return services;
        }

        #endregion AddExtendedAttributeDbContexts

        #region AddUchooseDataProtection

        /// <summary>
        /// Добавить защиту персональных данных.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddUchooseDataProtection(this IServiceCollection services, IConfiguration configuration)
        {
            var protectionSettings = configuration.GetSettings<ProtectionSettings>();

            // https://docs.microsoft.com/en-US/aspnet/core/security/data-protection/configuration/overview?view=aspnetcore-5.0
            var dataProtectionBuilder = services
                .AddDataProtection()
                .SetApplicationName(protectionSettings.ApplicationDiscriminator)
                .SetDefaultKeyLifetime(TimeSpan.FromDays(protectionSettings.DefaultKeyLifetimeInDays))
                .PersistKeysToDbContext<ApplicationDbContext>();

            if (protectionSettings.ProtectKeysWithCertificate)
            {
                using var store = new X509Store(StoreName.Root);
                store.Open(OpenFlags.ReadOnly);

                var certs =
                    store.Certificates.Find(X509FindType.FindBySubjectName, protectionSettings.CertificateSubjectName, false);
                if (certs.Count > 0)
                {
                    var certificate = certs[0];
                    Log.Information("Security certificate for data protection founded: {CertificateSubjectName} {CertificateFriendlyName}...", certificate.SubjectName.Name, certificate.FriendlyName); // TODO - локализовать
                    dataProtectionBuilder
                        .ProtectKeysWithCertificate(certificate);

                    // .UnprotectKeysWithAnyCertificate();
                }
                else
                {
                    Log.Warning("Security certificate for data protection not found!"); // TODO - локализовать
                }
            }

            return services;
        }

        #endregion AddUchooseDataProtection

        #region AddPostgreSql

        /// <summary>
        /// Добавить контекст доступа к данным в PostgreSql.
        /// </summary>
        /// <typeparam name="TDbContext">Тип контекста доступа к данным.</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddPostgreSql<TDbContext>(this IServiceCollection services, IConfiguration configuration)
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(m => m.UseNpgsql(configuration.GetConnectionString("UchooseDb"), e => e.MigrationsAssembly(typeof(TDbContext).Assembly.FullName)));
            services.AddHangfire(x => x.UsePostgreSqlStorage(configuration.GetConnectionString("HangFire")));
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
            dbContext.Database.Migrate();
            Log.Logger.Information("{DbContext} automatic migration complete.", typeof(TDbContext).GetGenericTypeName()); // TODO - локализовать

            return services;
        }

        #endregion AddPostgreSql
    }
}