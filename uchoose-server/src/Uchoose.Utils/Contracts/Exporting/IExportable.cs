// ------------------------------------------------------------------------------------------------------
// <copyright file="IExportable.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.Localization;
using Uchoose.Utils.Attributes.Exporting;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Extensions;

namespace Uchoose.Utils.Contracts.Exporting
{
    /// <summary>
    /// Экспортируемая сущность.
    /// </summary>
    /// <remarks>
    /// Используется при экспорте данных в файл.
    /// </remarks>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public interface IExportable<TEntityId, TEntity>
        where TEntity : IEntity<TEntityId>, IExportable<TEntityId, TEntity>, new()
    {
        /// <summary>
        /// Получить словарь по умолчанию для сопоставления экспортируемых данных сущности с данными в excel файле.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        /// <returns>Возвращает словарь по умолчанию для сопоставления экспортируемых данных сущности с данными в excel файле.</returns>
        Dictionary<string, Func<TEntity, (object Object, int Order)>> GetDefaultExportMappers(IStringLocalizer localizer);

        /// <summary>
        /// Словарь по умолчанию для сопоставления экспортируемых данных сущности с данными в excel файле.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        /// <returns>Возвращает словарь по умолчанию для сопоставления экспортируемых данных сущности с данными в excel файле.</returns>
        static Dictionary<string, Func<TEntity, (object Object, int Order)>> DefaultExportMappers(IStringLocalizer localizer)
        {
            return new TEntity().GetDefaultExportMappers(localizer);
        }

        /// <summary>
        /// Получить словарь для сопоставления экспортируемых данных сущности с данными в excel файле.
        /// </summary>
        /// <param name="properties">Список названий экспортируемых свойств.</param>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        /// <returns>Возвращает словарь для сопоставления экспортируемых данных сущности с данными в excel файле.</returns>
        private protected Dictionary<string, Func<TEntity, (object Object, int Order)>> GetExportMappers(List<string> properties, IStringLocalizer localizer)
        {
            if (properties?.Any() != true)
            {
                return DefaultExportMappers(localizer);
            }

            var result = new Dictionary<string, Func<TEntity, (object Object, int Order)>>();
            if (typeof(TEntity).GetCustomAttribute<NotExportableAttribute>() != null)
            {
                return result;
            }

            int order = 1;
            foreach (string propertyName in properties.Where(x => x.IsPresent()).Select(x => x.Trim()).Distinct())
            {
                (object Object, int Order) Value(TEntity item)
                {
                    var property = item.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                    if (property == null || property.GetCustomAttribute<NotExportableAttribute>() != null)
                    {
                        order++;
                        return (null, order);
                    }

                    object value = property.GetValue(item);
                    order++;
                    return (value, order);
                }

                result.Add(propertyName, Value);
            }

            return result;
        }

        /// <summary>
        /// Словарь для сопоставления экспортируемых данных сущности с данными в excel файле.
        /// </summary>
        /// <param name="properties">Список названий экспортируемых свойств.</param>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        /// <returns>Возвращает словарь для сопоставления экспортируемых данных сущности с данными в excel файле.</returns>
        static Dictionary<string, Func<TEntity, (object Object, int Order)>> ExportMappers(List<string> properties, IStringLocalizer localizer)
        {
            return new TEntity().GetExportMappers(properties, localizer);
        }

        /// <summary>
        /// Словарь допустимых названий свойств c их описанием для экспортируемой сущности.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        static Dictionary<string, string> ExportableProperties(IStringLocalizer localizer = null)
        {
            {
                var result = new Dictionary<string, string>();

                if (typeof(TEntity).GetCustomAttribute(typeof(NotExportableAttribute)) == null)
                {
                    result = typeof(TEntity)
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(propertyInfo => propertyInfo.GetCustomAttribute<NotExportableAttribute>() == null)
                        .ToDictionary(x => x.Name, y => localizer == null ? y.GetCustomAttribute<DisplayAttribute>()?.Name ?? y.Name : localizer[y.GetCustomAttribute<DisplayAttribute>()?.Name ?? y.Name]);
                }

                return result;
            }
        }
    }
}