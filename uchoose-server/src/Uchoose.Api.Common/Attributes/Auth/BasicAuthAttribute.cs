// ------------------------------------------------------------------------------------------------------
// <copyright file="BasicAuthAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.AspNetCore.Mvc;
using Uchoose.Api.Common.Filters.Auth;

namespace Uchoose.Api.Common.Attributes.Auth
{
    /// <summary>
    /// Атрибут, указывающий, что помеченное им действие или контроллер должны пройти Basic авторизацию.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="BasicAuthAttribute"/>.
        /// </summary>
        /// <param name="realm">Описание защищаемого ресурса.</param>
        public BasicAuthAttribute(string realm = "Secured Resource")
            : base(typeof(BasicAuthFilter))
        {
            Arguments = new object[] { realm };
        }
    }
}