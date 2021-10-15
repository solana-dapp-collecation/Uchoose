// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Uchoose.RoleService.Interfaces;
using Uchoose.Utils.Extensions;

namespace Uchoose.RoleService.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить сервис для работы с ролями пользователей.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddRoleService(this IServiceCollection services)
        {
            return services
                .AddRoleServiceMappings()
                .AddTransientApplicationService<IRoleService, RoleService>();
        }

        /// <summary>
        /// Добавить сопоставления для сервиса <see cref="RoleService"/>.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddRoleServiceMappings(this IServiceCollection services)
        {
            return services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(IRoleService)));
        }
    }
}