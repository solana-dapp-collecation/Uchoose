// ------------------------------------------------------------------------------------------------------
// <copyright file="ConfigurationExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Utils.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IConfiguration"/>.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Получить настройки для заданного типа из конфигурации.
        /// </summary>
        /// <typeparam name="TSettings">Тип для хранения настроек.</typeparam>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <param name="sectionName">Наименование секции в конфигурации приложения. Если не указано, то берётся имя переданного типа.</param>
        /// <returns>Возвращает полученные из конфигурации настройки.</returns>
        public static TSettings GetSettings<TSettings>(this IConfiguration configuration, string sectionName = null)
            where TSettings : class, ISettings, new()
        {
            var section = configuration.GetSection(string.IsNullOrWhiteSpace(sectionName) ? typeof(TSettings).Name : sectionName);
            var settings = new TSettings();
            section.Bind(settings);

            return settings;
        }

        /// <summary>
        /// Добавить настройки указанного типа.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <param name="sectionName">Наименование секции в конфигурации приложения. Если не указано, то берётся имя переданного типа.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddSettings<TSettings>(this IServiceCollection services, IConfiguration configuration, string sectionName = null)
            where TSettings : class, ISettings, new()
        {
            return services
                .Configure<TSettings>(configuration.GetSection(string.IsNullOrWhiteSpace(sectionName) ? typeof(TSettings).Name : sectionName));
        }
    }
}