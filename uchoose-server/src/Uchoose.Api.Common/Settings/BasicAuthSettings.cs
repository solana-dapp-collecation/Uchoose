// ------------------------------------------------------------------------------------------------------
// <copyright file="BasicAuthSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Uchoose.Api.Common.Attributes.Auth;
using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Api.Common.Settings
{
    /// <summary>
    /// Настройки Basic авторизации.
    /// </summary>
    public class BasicAuthSettings :
        ISettings
    {
        /// <summary>
        /// Basic авторизация включена (глобально).
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Использовать <see cref="BasicAuthAttribute"/> для авторизации.
        /// </summary>
        public bool UseBasicAuthAttribute { get; set; }

        /// <summary>
        /// Защищённые Basic авторизацией пути.
        /// </summary>
        public List<SecuredPath> SecuredPaths { get; set; } = new();
    }

    /// <summary>
    /// Защищённый путь.
    /// </summary>
    public class SecuredPath
    {
        /// <summary>
        /// Наименование защищённого пути.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Basic авторизация включена (для текущего пути).
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Префикс пути к ресурсу (например, "/{path}"), где {path} - конкретное значение пути.
        /// </summary>
        public string PathPrefix { get; set; }

        /// <summary>
        /// Список Claim, необходимых для доступа к ресурсу.
        /// </summary>
        public List<string> RequiredClaims { get; set; }
    }
}