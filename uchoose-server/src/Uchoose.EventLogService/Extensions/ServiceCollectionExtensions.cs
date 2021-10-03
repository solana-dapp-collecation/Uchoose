// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Uchoose.EventLogService.Interfaces;
using Uchoose.Utils.Extensions;

namespace Uchoose.EventLogService.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить сервис для работы с логами событий.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddEventLogService(this IServiceCollection services)
        {
            return services
                .AddEventLogServiceMappings()
                .AddEventLogServiceValidators()
                .AddTransientApplicationService<IEventLogService, EventLogService>();
        }

        /// <summary>
        /// Добавить сопоставления для сервиса <see cref="EventLogService"/>.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddEventLogServiceMappings(this IServiceCollection services)
        {
            return services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(IEventLogService)));
        }

        /// <summary>
        /// Добавить валидаторы для сервиса <see cref="EventLogService"/>.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddEventLogServiceValidators(this IServiceCollection services)
        {
            return services
                .AddPaginationFilterValidators(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(IEventLogService)))
                .AddExportPaginationFilterValidators(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(IEventLogService)))
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
                .AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(IEventLogService)), includeInternalTypes: true);
        }
    }
}