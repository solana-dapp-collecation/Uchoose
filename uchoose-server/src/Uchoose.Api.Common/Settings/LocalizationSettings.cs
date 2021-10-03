// ------------------------------------------------------------------------------------------------------
// <copyright file="LocalizationSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Uchoose.Api.Common.Models;
using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Api.Common.Settings
{
    /// <summary>
    /// Настройки локализации.
    /// </summary>
    public class LocalizationSettings :
        ISettings
    {
        /// <summary>
        /// Путь относительно приложения, в котором расположены ресурсы для локализации.
        /// </summary>
        public string ResourcesPath { get; set; }

        /// <summary>
        /// Список поддерживаемых языков.
        /// </summary>
        public List<LanguageCode> SupportedLanguages { get; set; } = new();
    }
}