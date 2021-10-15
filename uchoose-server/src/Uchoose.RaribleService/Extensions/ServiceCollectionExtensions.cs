// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Uchoose.RaribleService.Interfaces;
using Uchoose.Utils.Extensions;

namespace Uchoose.RaribleService.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить сервис для работы с блокчейном Solana.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddRaribleService(this IServiceCollection services)
        {
            return services
                .AddTransientInfrastructureService<IRaribleService, RaribleService>();
        }
    }
}