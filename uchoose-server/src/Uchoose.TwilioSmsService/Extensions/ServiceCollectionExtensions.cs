// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Uchoose.SmsService.Interfaces;
using Uchoose.Utils.Extensions;

namespace Uchoose.TwilioSmsService.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить сервис для работы с СМС.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddTwilioSmsService(this IServiceCollection services)
        {
            return services.
                AddTransientInfrastructureService<ISmsService, TwilioSmsService>();
        }
    }
}