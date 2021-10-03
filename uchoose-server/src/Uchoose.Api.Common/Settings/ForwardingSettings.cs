// ------------------------------------------------------------------------------------------------------
// <copyright file="ForwardingSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Api.Common.Settings
{
    /// <summary>
    /// Настройки перенаправления через прокси.
    /// </summary>
    public class ForwardingSettings :
        ISettings
    {
        /// <summary>
        /// Использовать перенаправление через прокси.
        /// </summary>
        public bool UseForwardingProxy { get; set; }

        /// <summary>
        /// Список ip адресов прокси.
        /// </summary>
        public List<string> ProxyIps { get; set; }

        /// <summary>
        /// URL приложения.
        /// </summary>
        public string ApplicationUrl { get; set; }
    }
}