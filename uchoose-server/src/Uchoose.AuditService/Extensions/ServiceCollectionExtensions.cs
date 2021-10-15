// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Uchoose.AuditService.Interfaces;
using Uchoose.DataAccess.Interfaces;
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.Utils.Extensions;

namespace Uchoose.AuditService.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///  Добавить сервис аудита сущностей указанного контекста доступа к данным.
        /// </summary>
        /// <typeparam name="TIDbContext">Тип интерфейса контекста доступа к данным аудита.</typeparam>
        /// <typeparam name="TDbContext">Тип контекст доступа к данным аудита.</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddAuditService<TIDbContext, TDbContext>(this IServiceCollection services)
            where TIDbContext : IAuditableDbContext, IDbContextInterface
            where TDbContext : TIDbContext
        {
            services
                .AddAuditServiceMappings()
                .AddAuditServiceValidators()
                .AddTransient<IAuditService<TIDbContext>, AuditService<TIDbContext, TDbContext>>();

            return services;
        }

        /// <summary>
        /// Добавить сопоставления для сервиса <see cref="AuditService"/>.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddAuditServiceMappings(this IServiceCollection services)
        {
            return services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(IAuditService<>)));
        }

        /// <summary>
        /// Добавить валидаторы для сервиса <see cref="AuditService"/>.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddAuditServiceValidators(this IServiceCollection services)
        {
            return services
                .AddPaginationFilterValidators(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(IAuditService<>)))
                .AddExportPaginationFilterValidators(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(IAuditService<>)))
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
                .AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(IAuditService<>)), includeInternalTypes: true);
        }
    }
}