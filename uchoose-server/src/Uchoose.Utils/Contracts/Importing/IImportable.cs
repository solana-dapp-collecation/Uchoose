// ------------------------------------------------------------------------------------------------------
// <copyright file="IImportable.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.Localization;
using Uchoose.Utils.Attributes.Importing;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Extensions;

namespace Uchoose.Utils.Contracts.Importing
{
    /// <summary>
    /// Импортируемая сущность.
    /// </summary>
    /// <remarks>
    /// Используется при импорте данных из файла.
    /// </remarks>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public interface IImportable<TEntityId, TEntity>
        where TEntity : IEntity<TEntityId>, IImportable<TEntityId, TEntity>, new()
    {
        /// <summary>
        /// Получить словарь по умолчанию для сопоставления импортируемых из excel данных с данными сущности.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        /// <returns>Возвращает словарь по умолчанию для сопоставления импортируемых из excel данных с данными сущности.</returns>
        Dictionary<string, Func<DataRow, TEntity, (object Object, int Order)>> GetDefaultImportMappers(IStringLocalizer localizer);

        /// <summary>
        /// Словарь по умолчанию для сопоставления импортируемых из excel данных с данными сущности.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        /// <returns>Возвращает словарь по умолчанию для сопоставления импортируемых из excel данных с данными сущности.</returns>
        static Dictionary<string, Func<DataRow, TEntity, (object Object, int Order)>> DefaultImportMappers(IStringLocalizer localizer)
        {
            return new TEntity().GetDefaultImportMappers(localizer);
        }

        /// <summary>
        /// Получить словарь для сопоставления импортируемых из excel данных с данными сущности.
        /// </summary>
        /// <param name="properties">Список названий импортируемых свойств.</param>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        /// <returns>Возвращает словарь для сопоставления импортируемых из excel данных с данными сущности.</returns>
        private protected Dictionary<string, Func<DataRow, TEntity, (object Object, int Order)>> GetImportMappers(List<string> properties, IStringLocalizer localizer)
        {
            var result = new Dictionary<string, Func<DataRow, TEntity, (object Object, int Order)>>();
            if (typeof(TEntity).GetCustomAttribute<NotImportableAttribute>() != null)
            {
                return result;
            }

            foreach (string propertyName in properties.Where(x => x.IsPresent()).Select(x => x.Trim()).Distinct())
            {
                (object Object, int Order) Value(DataRow row, TEntity item)
                {
                    var property = item.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                    if (property != null && property.GetCustomAttribute<NotImportableAttribute>() == null)
                    {
                        object value = Convert.ChangeType(row[localizer[propertyName]!], property.PropertyType);
                        property.SetValue(item, value);
                    }

                    return (row[localizer[propertyName]!].ToString(), item.GetImportExportOrderAttributeValue(propertyName));
                }

                // TODO - как сделать так, чтобы не добавлялись неимпортируемые (и ненайденные) свойства в result?
                result.Add(localizer[propertyName]!, Value);
            }

            return result;
        }

        /// <summary>
        /// Словарь для сопоставления импортируемых из excel данных с данными сущности.
        /// </summary>
        /// <param name="properties">Список названий импортируемых свойств.</param>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        /// <returns>Возвращает словарь для сопоставления импортируемых из excel данных с данными сущности.</returns>
        static Dictionary<string, Func<DataRow, TEntity, (object Object, int Order)>> ImportMappers(List<string> properties, IStringLocalizer localizer)
        {
            return new TEntity().GetImportMappers(properties, localizer);
        }

        /// <summary>
        /// Словарь допустимых названий свойств c их описанием для импортируемой сущности.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        static Dictionary<string, string> ImportableProperties(IStringLocalizer localizer = null)
        {
            var result = new Dictionary<string, string>();

            if (typeof(TEntity).GetCustomAttribute(typeof(NotImportableAttribute)) == null)
            {
                result = typeof(TEntity)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(propertyInfo => propertyInfo.GetCustomAttribute<NotImportableAttribute>() == null)
                    .ToDictionary(x => x.Name, y => localizer == null ? y.GetCustomAttribute<DisplayAttribute>()?.Name ?? y.Name : localizer[y.GetCustomAttribute<DisplayAttribute>()?.Name ?? y.Name]);
            }

            return result;
        }
    }
}