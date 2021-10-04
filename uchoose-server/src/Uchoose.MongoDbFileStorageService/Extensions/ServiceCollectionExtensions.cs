// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Uchoose.FileStorageService.Interfaces;
using Uchoose.MongoDbFileStorageService.Storage;
using Uchoose.Utils.Extensions;

namespace Uchoose.MongoDbFileStorageService.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить сервис для доступа к хранилищу файлов в MongoDb.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddMongoDbFileStorageService(this IServiceCollection services)
        {
            return services
                .AddScoped<IMongoDbStorage, MongoDbStorage>()
                .AddTransientApplicationService<IFileStorageService, MongoDbFileStorageService>();
        }
    }
}