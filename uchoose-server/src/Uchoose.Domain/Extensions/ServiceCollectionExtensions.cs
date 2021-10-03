// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Reflection;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Uchoose.Domain.Contracts;
using Uchoose.Domain.Filters;
using Uchoose.Domain.Filters.Validators;
using Uchoose.Utils.Contracts.Services;

namespace Uchoose.Domain.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region Domain services

        #region AddTransientDomainService

        /// <summary>
        /// Добавить transient доменный сервис.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddTransientDomainService<TService, TImplementation>(this IServiceCollection services)
            where TService : IDomainService
            where TImplementation : TService, ITransientService
        {
            return services
                .AddTransient(typeof(TService), typeof(TImplementation));
        }

        #endregion AddTransientDomainService

        #region AddScopedDomainService

        /// <summary>
        /// Добавить scoped доменный сервис.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddScopedDomainService<TService, TImplementation>(this IServiceCollection services)
            where TService : IDomainService
            where TImplementation : TService, IScopedService
        {
            return services
                .AddScoped(typeof(TService), typeof(TImplementation));
        }

        #endregion AddScopedDomainService

        #region AddSingletonDomainService

        /// <summary>
        /// Добавить singleton доменный сервис.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddSingletonDomainService<TService, TImplementation>(this IServiceCollection services)
            where TService : IDomainService
            where TImplementation : TService, ISingletonService
        {
            return services
                .AddSingleton(typeof(TService), typeof(TImplementation));
        }

        #endregion AddSingletonDomainService

        #endregion Domain services

        #region AddExtendedAttributePaginationFilterValidators

        /// <summary>
        /// Добавить валидаторы для фильтров расширенных атрибутов сущностей.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="assemblies">Сборки с расширенными атрибутами, используемыми для добавления валидаторов.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddExtendedAttributePaginationFilterValidators(this IServiceCollection services, params Assembly[] assemblies)
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
                    .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(ExtendedAttributePaginationFilterValidator<,>)))
                .ToList();

            foreach (var validatorType in validatorTypes)
            {
                var validatorTypeGenericArguments = validatorType.BaseGenericType.GetGenericArguments().ToList();

                var filterType = typeof(ExtendedAttributePaginationFilter<,>).MakeGenericType(validatorTypeGenericArguments.ToArray());
                var validatorServiceType = typeof(IValidator<>).MakeGenericType(filterType);
                services.AddScoped(validatorServiceType, validatorType.CurrentType);
            }

            return services;
        }

        #endregion AddExtendedAttributePaginationFilterValidators
    }
}