// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Uchoose.IpfsService.Interfaces;
using Uchoose.Utils.Extensions;

namespace Uchoose.IpfsService.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить сервис для работы с IPFS.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddIpfsService(this IServiceCollection services)
        {
            return services
                .AddScopedInfrastructureService<IIpfsService, IpfsService>();
        }
    }
}