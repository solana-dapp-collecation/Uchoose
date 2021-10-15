// ------------------------------------------------------------------------------------------------------
// <copyright file="InternalControllerFeatureProvider.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Uchoose.Api.Common.Providers
{
    /// <summary>
    /// Провайдер для проверки, является ли тип контроллером.
    /// </summary>
    internal class InternalControllerFeatureProvider :
        ControllerFeatureProvider
    {
        /// <summary>
        /// Суффикс названия контроллера.
        /// </summary>
        private const string ControllerSuffix = "Controller";

        /// <inheritdoc/>
        protected override bool IsController(TypeInfo typeInfo)
        {
            if (!typeInfo.IsClass)
            {
                return false;
            }

            if (typeInfo.IsAbstract)
            {
                return false;
            }

            if (typeInfo.ContainsGenericParameters)
            {
                return false;
            }

            if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
            {
                return false;
            }

            return typeInfo.Name.EndsWith(ControllerSuffix, StringComparison.OrdinalIgnoreCase) ||
                   typeInfo.IsDefined(typeof(ControllerAttribute));
        }
    }
}