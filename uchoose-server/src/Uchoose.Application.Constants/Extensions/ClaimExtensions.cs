// ------------------------------------------------------------------------------------------------------
// <copyright file="ClaimExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

using Uchoose.Application.Constants.Permission;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.Application.Constants.Extensions
{
    /// <summary>
    /// Методы расширения для разрешений.
    /// </summary>
    public static class ClaimExtensions
    {
        #region GetAllPermissions

        /// <summary>
        /// Получить список данных с разрешениями.
        /// </summary>
        /// <param name="allPermissions">Список данных с разрешениями.</param>
        public static void GetAllPermissions<TClaim>(this List<TClaim> allPermissions) // TODO - подумать, как вынести в общий проект (не держать в constants)
            where TClaim : IHasValue, IHasType, IHasDescription, IHasGroup, new()
        {
            foreach (var module in typeof(Permissions).GetNestedTypes())
            {
                string moduleName = string.Empty;
                string moduleDescription = string.Empty;

                if (module.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .FirstOrDefault() is DisplayNameAttribute displayNameAttribute)
                {
                    moduleName = displayNameAttribute.DisplayName;
                }

                if (module.GetCustomAttributes(typeof(DescriptionAttribute), true)
                    .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                {
                    moduleDescription = descriptionAttribute.Description;
                }

                foreach (var fi in module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
                {
                    object? propertyValue = fi.GetValue(null);

                    if (propertyValue is not null)
                    {
                        allPermissions.Add(new() { Value = propertyValue.ToString() !, Type = ApplicationClaimTypes.Permission, Group = moduleName, Description = moduleDescription });
                    }
                }
            }
        }

        #endregion GetAllPermissions
    }
}