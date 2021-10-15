// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Reflection;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Filters.Validators;

namespace Uchoose.Utils.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region Application services

        #region AddTransientApplicationService

        /// <summary>
        /// Добавить transient сервис приложения.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddTransientApplicationService<TService, TImplementation>(this IServiceCollection services)
            where TService : IApplicationService
            where TImplementation : TService, ITransientService
        {
            return services
                .AddTransient(typeof(TService), typeof(TImplementation));
        }

        #endregion AddTransientApplicationService

        #region AddScopedApplicationService

        /// <summary>
        /// Добавить scoped сервис приложения.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddScopedApplicationService<TService, TImplementation>(this IServiceCollection services)
            where TService : IApplicationService
            where TImplementation : TService, IScopedService
        {
            return services
                .AddScoped(typeof(TService), typeof(TImplementation));
        }

        #endregion AddScopedApplicationService

        #region AddSingletonApplicationService

        /// <summary>
        /// Добавить singleton сервис приложения.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddSingletonApplicationService<TService, TImplementation>(this IServiceCollection services)
            where TService : IApplicationService
            where TImplementation : TService, ISingletonService
        {
            return services
                .AddSingleton(typeof(TService), typeof(TImplementation));
        }

        #endregion AddSingletonApplicationService

        #endregion Application services

        #region Infrastructure services

        #region AddTransientInfrastructureService

        /// <summary>
        /// Добавить transient сервис инфраструктуры.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddTransientInfrastructureService<TService, TImplementation>(this IServiceCollection services)
            where TService : IInfrastructureService
            where TImplementation : TService, ITransientService
        {
            return services
                .AddTransient(typeof(TService), typeof(TImplementation));
        }

        #endregion AddTransientInfrastructureService

        #region AddScopedInfrastructureService

        /// <summary>
        /// Добавить scoped сервис инфраструктуры.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddScopedInfrastructureService<TService, TImplementation>(this IServiceCollection services)
            where TService : IInfrastructureService
            where TImplementation : TService, IScopedService
        {
            return services
                .AddScoped(typeof(TService), typeof(TImplementation));
        }

        #endregion AddScopedInfrastructureService

        #region AddSingletonInfrastructureService

        /// <summary>
        /// Добавить singleton сервис инфраструктуры.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddSingletonInfrastructureService<TService, TImplementation>(this IServiceCollection services)
            where TService : IInfrastructureService
            where TImplementation : TService, ISingletonService
        {
            return services
                .AddSingleton(typeof(TService), typeof(TImplementation));
        }

        #endregion AddSingletonInfrastructureService

        #endregion Infrastructure services

        #region AddPaginationFilterValidators

        /// <summary>
        /// Добавить валидаторы для фильтров сущностей.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="assemblies">Сборки с расширенными атрибутами, используемыми для добавления валидаторов.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddPaginationFilterValidators(this IServiceCollection services, params Assembly[] assemblies)
        {
            var validatorTypes = assemblies
                .SelectMany(assembly => assembly
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                    .Select(t => new
                    {
                        BaseGenericType = t.BaseType,
                        CurrentType = t
                    })
                    .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(PaginationFilterValidator<,,>)))
                .ToList();

            foreach (var validatorType in validatorTypes)
            {
                var validatorTypeGenericArguments = validatorType.BaseGenericType.GetGenericArguments().ToList();
                var validatorServiceType = typeof(IValidator<>).MakeGenericType(validatorTypeGenericArguments.Last());
                services.AddScoped(validatorServiceType, validatorType.CurrentType);
            }

            return services;
        }

        #endregion AddPaginationFilterValidators

        #region AddExportPaginationFilterValidators

        /// <summary>
        /// Добавить валидаторы для фильтров сущностей.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="assemblies">Сборки с расширенными атрибутами, используемыми для добавления валидаторов.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddExportPaginationFilterValidators(this IServiceCollection services, params Assembly[] assemblies)
        {
            var validatorTypes = assemblies
                .SelectMany(assembly => assembly
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                    .Select(t => new
                    {
                        BaseGenericType = t.BaseType,
                        CurrentType = t
                    })
                    .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(ExportPaginationFilterValidator<,,>)))
                .ToList();

            foreach (var validatorType in validatorTypes)
            {
                var validatorTypeGenericArguments = validatorType.BaseGenericType.GetGenericArguments().ToList();
                var validatorServiceType = typeof(IValidator<>).MakeGenericType(validatorTypeGenericArguments.Last());
                services.AddScoped(validatorServiceType, validatorType.CurrentType);
            }

            return services;
        }

        #endregion AddExportPaginationFilterValidators
    }
}