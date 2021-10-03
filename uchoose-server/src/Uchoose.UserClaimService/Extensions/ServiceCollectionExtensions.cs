// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Uchoose.UserClaimService.Interfaces;
using Uchoose.Utils.Extensions;

namespace Uchoose.UserClaimService.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить сервис для работы с разрешениями пользователей.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddUserClaimService(this IServiceCollection services)
        {
            return services
                .AddUserClaimServiceMappings()
                .AddTransientApplicationService<IUserClaimService, UserClaimService>();
        }

        /// <summary>
        /// Добавить сопоставления для сервиса <see cref="UserClaimService"/>.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddUserClaimServiceMappings(this IServiceCollection services)
        {
            return services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(IUserClaimService)));
        }
    }
}