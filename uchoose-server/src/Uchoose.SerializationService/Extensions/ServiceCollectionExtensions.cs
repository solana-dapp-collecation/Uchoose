// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Text.Json;

using Microsoft.Extensions.DependencyInjection;
using Uchoose.SerializationService.Interfaces;
using Uchoose.SerializationService.Interfaces.Converters;
using Uchoose.SerializationService.Serializers;

namespace Uchoose.SerializationService.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить сериализатор json.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddJsonSerializer(this IServiceCollection services)
        {
            services
                .AddSingleton<IJsonSerializer, SystemTextJsonSerializer>()
                .Configure<JsonSerializerOptions>(configureOptions =>
                {
                    if (!configureOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                    {
                        configureOptions.Converters.Add(new TimespanJsonConverter());
                    }
                });

            return services;
        }
    }
}