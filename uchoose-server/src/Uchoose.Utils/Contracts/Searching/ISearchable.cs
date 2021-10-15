// ------------------------------------------------------------------------------------------------------
// <copyright file="ISearchable.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.Localization;
using Uchoose.Utils.Attributes.Searching;
using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Utils.Contracts.Searching
{
    /// <summary>
    /// Сущность, по которой можно осуществлять поиск.
    /// </summary>
    public interface ISearchable<TEntityId, TEntity>
        where TEntity : IEntity<TEntityId>, ISearchable<TEntityId, TEntity>
    {
        /// <summary>
        /// Словарь допустимых названий свойств c их описанием для сущности, по которым можно осуществлять поиск.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        static Dictionary<string, string> SearchableProperties(IStringLocalizer localizer = null)
        {
            var result = new Dictionary<string, string>();

            if (typeof(TEntity).GetCustomAttribute(typeof(NotSearchableAttribute)) == null)
            {
                result = typeof(TEntity)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(propertyInfo => propertyInfo.GetCustomAttribute<NotSearchableAttribute>() == null)
                    .ToDictionary(x => x.Name, y => localizer == null ? y.GetCustomAttribute<DisplayAttribute>()?.Name ?? y.Name : localizer[y.GetCustomAttribute<DisplayAttribute>()?.Name ?? y.Name]);
            }

            return result;
        }
    }
}