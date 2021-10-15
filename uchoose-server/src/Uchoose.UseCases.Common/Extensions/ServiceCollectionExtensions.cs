// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Entities;
using Uchoose.Domain.Events.ExtendedAttributes;
using Uchoose.Domain.Extensions;
using Uchoose.Domain.Identity.Entities;
using Uchoose.UseCases.Common.Behaviors;
using Uchoose.UseCases.Common.Features.Common.Commands.Validators;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Commands;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Commands.Validators.Abstractions;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.EventHandlers;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries.Responses;
using Uchoose.UseCases.Common.Settings;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region AddCommonUseCases

        /// <summary>
        /// Добавить общие UseCases.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddCommonUseCases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddCommonBehaviors(configuration);
            services.AddAutoMapper(
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(EventLog)),
                Assembly.GetAssembly(typeof(UchooseUser))); // , Assembly.GetAssembly(typeof(NFTRequest)) // TODO
            services.AddExtendedAttributeHandlers(
                Assembly.GetAssembly(typeof(UchooseUser))); // , Assembly.GetAssembly(typeof(NFTRequest)) // TODO

            services.AddExtendedAttributeCommandValidators(Assembly.GetExecutingAssembly());
            services.AddExtendedAttributePaginationFilterValidators(Assembly.GetExecutingAssembly());
            services.AddPaginationFilterValidators(Assembly.GetExecutingAssembly());
            services.AddImportEntitiesCommandValidators(Assembly.GetExecutingAssembly());
            services.AddExportPaginationFilterValidators(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
            return services;
        }

        #endregion AddCommonUseCases

        #region AddExtendedAttributeCommandValidators

        /// <summary>
        /// Добавить валидаторы для расширенных атрибутов сущностей.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="assemblies">Сборки с расширенными атрибутами, используемыми для добавления валидаторов.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddExtendedAttributeCommandValidators(this IServiceCollection services, params Assembly[] assemblies)
        {
            #region AddExtendedAttributeCommandValidator

            var addCommandValidatorTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                    .Select(t => new
                    {
                        BaseGenericType = t.BaseType,
                        CurrentType = t
                    })
                    .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(AddExtendedAttributeCommandValidator<,>)))
                .ToList();

            foreach (var addCommandValidatorType in addCommandValidatorTypes)
            {
                var addCommandValidatorTypeGenericArguments = addCommandValidatorType.BaseGenericType.GetGenericArguments().ToList();

                var addCommandType = typeof(AddExtendedAttributeCommand<,>).MakeGenericType(addCommandValidatorTypeGenericArguments.ToArray());
                var validatorServiceType = typeof(IValidator<>).MakeGenericType(addCommandType);
                services.AddScoped(validatorServiceType, addCommandValidatorType.CurrentType);
            }

            #endregion AddExtendedAttributeCommandValidator

            #region UpdateExtendedAttributeCommandValidator

            var updateCommandValidatorTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                    .Select(t => new
                    {
                        BaseGenericType = t.BaseType,
                        CurrentType = t
                    })
                    .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(UpdateExtendedAttributeCommandValidator<,>)))
                .ToList();

            foreach (var updateCommandValidatorType in updateCommandValidatorTypes)
            {
                var updateCommandValidatorTypeGenericArguments = updateCommandValidatorType.BaseGenericType.GetGenericArguments().ToList();

                var updateCommandType = typeof(UpdateExtendedAttributeCommand<,>).MakeGenericType(updateCommandValidatorTypeGenericArguments.ToArray());
                var validatorServiceType = typeof(IValidator<>).MakeGenericType(updateCommandType);
                services.AddScoped(validatorServiceType, updateCommandValidatorType.CurrentType);
            }

            #endregion UpdateExtendedAttributeCommandValidator

            #region RemoveExtendedAttributeCommandValidator

            var removeCommandValidatorTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                    .Select(t => new
                    {
                        BaseGenericType = t.BaseType,
                        CurrentType = t
                    })
                    .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(RemoveExtendedAttributeCommandValidator<,>)))
                .ToList();

            foreach (var removeCommandValidatorType in removeCommandValidatorTypes)
            {
                var removeCommandValidatorTypeGenericArguments = removeCommandValidatorType.BaseGenericType.GetGenericArguments().ToList();

                var removeCommandType = typeof(RemoveExtendedAttributeCommand<,>).MakeGenericType(removeCommandValidatorTypeGenericArguments.ToArray());
                var validatorServiceType = typeof(IValidator<>).MakeGenericType(removeCommandType);
                services.AddScoped(validatorServiceType, removeCommandValidatorType.CurrentType);
            }

            #endregion RemoveExtendedAttributeCommandValidator

            return services;
        }

        #endregion AddExtendedAttributeCommandValidators

        #region AddImportEntitiesCommandValidators

        /// <summary>
        /// Добавить валидаторы для фильтров сущностей.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="assemblies">Сборки с расширенными атрибутами, используемыми для добавления валидаторов.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddImportEntitiesCommandValidators(this IServiceCollection services, params Assembly[] assemblies)
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
                    .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(ImportEntitiesCommandValidator<,,>)))
                .ToList();

            foreach (var validatorType in validatorTypes)
            {
                var validatorTypeGenericArguments = validatorType.BaseGenericType.GetGenericArguments().ToList();
                var validatorServiceType = typeof(IValidator<>).MakeGenericType(validatorTypeGenericArguments.Last());
                services.AddScoped(validatorServiceType, validatorType.CurrentType);
            }

            return services;
        }

        #endregion AddImportEntitiesCommandValidators

        #region GetSettings

        /// <summary>
        /// Получить настройки для заданного типа из конфигурации.
        /// </summary>
        /// <typeparam name="TSettings">Тип для хранения настроек.</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает полученные из конфигурации настройки.</returns>
        public static TSettings GetSettings<TSettings>(this IServiceCollection services)
            where TSettings : class, ISettings, new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var settingsOptions = serviceProvider.GetRequiredService<IOptionsSnapshot<TSettings>>();
            return settingsOptions.Value;
        }

        #endregion GetSettings

        #region AddExtendedAttributeHandlers

        /// <summary>
        /// Добавить обработчики расширенных атрибутов сущностей.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="assemblies">Сборки с расширенными атрибутами, используемыми для добавления обработчиков.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddExtendedAttributeHandlers(this IServiceCollection services, params Assembly[] assemblies)
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
                var queriesImplementationType = typeof(ExtendedAttributeQueryHandler<,,>).MakeGenericType(extendedAttributeTypeGenericArguments[0], extendedAttributeTypeGenericArguments[1], extendedAttributeType.CurrentType);
                var commandsImplementationType = typeof(ExtendedAttributeCommandHandler<,,>).MakeGenericType(extendedAttributeTypeGenericArguments[0], extendedAttributeTypeGenericArguments[1], extendedAttributeType.CurrentType);
                var eventsImplementationType = typeof(ExtendedAttributeEventHandler<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());

                #region GetExtendedAttributesQuery

                var tRequest = typeof(GetExtendedAttributesQuery<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                var tResponse = typeof(PaginatedResult<>).MakeGenericType(typeof(ExtendedAttributeResponse<>).MakeGenericType(extendedAttributeTypeGenericArguments[0]));
                var serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, queriesImplementationType);

                #endregion GetExtendedAttributesQuery

                #region GetExtendedAttributeByIdQuery

                tRequest = typeof(GetExtendedAttributeByIdQuery<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                tResponse = typeof(Result<>).MakeGenericType(typeof(ExtendedAttributeResponse<>).MakeGenericType(extendedAttributeTypeGenericArguments[0]));
                serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, queriesImplementationType);

                #endregion GetExtendedAttributeByIdQuery

                #region AddExtendedAttributeCommand

                tRequest = typeof(AddExtendedAttributeCommand<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                tResponse = typeof(Result<>).MakeGenericType(typeof(Guid));
                serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, commandsImplementationType);

                #endregion AddExtendedAttributeCommand

                #region UpdateExtendedAttributeCommand

                tRequest = typeof(UpdateExtendedAttributeCommand<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                tResponse = typeof(Result<>).MakeGenericType(typeof(Guid));
                serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, commandsImplementationType);

                #endregion UpdateExtendedAttributeCommand

                #region RemoveExtendedAttributeCommand

                tRequest = typeof(RemoveExtendedAttributeCommand<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                tResponse = typeof(Result<>).MakeGenericType(typeof(Guid));
                serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, commandsImplementationType);

                #endregion RemoveExtendedAttributeCommand

                #region ExtendedAttributeAddedEvent

                serviceType = typeof(INotificationHandler<>).MakeGenericType(typeof(ExtendedAttributeAddedEvent<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray()));
                services.AddScoped(serviceType, eventsImplementationType);

                #endregion ExtendedAttributeAddedEvent

                #region ExtendedAttributeUpdatedEvent

                serviceType = typeof(INotificationHandler<>).MakeGenericType(typeof(ExtendedAttributeUpdatedEvent<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray()));
                services.AddScoped(serviceType, eventsImplementationType);

                #endregion ExtendedAttributeUpdatedEvent

                #region ExtendedAttributeRemovedEvent

                serviceType = typeof(INotificationHandler<>).MakeGenericType(typeof(ExtendedAttributeRemovedEvent<>).MakeGenericType(extendedAttributeTypeGenericArguments[1]));
                services.AddScoped(serviceType, eventsImplementationType);

                #endregion ExtendedAttributeRemovedEvent
            }

            return services;
        }

        #endregion AddExtendedAttributeHandlers

        #region AddCommonBehaviors

        /// <summary>
        /// Добавить посредников для обработчиков запросов.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddCommonBehaviors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSettings<CacheSettings>(configuration);
            services.AddSettings<BehaviorSettings>(configuration);

            // порядок добавления важен: в таком же порядке будет обрабатываться запрос
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ResponseCachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

            return services;
        }

        #endregion AddCommonBehaviors
    }
}