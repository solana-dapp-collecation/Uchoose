// ------------------------------------------------------------------------------------------------------
// <copyright file="HangfireSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Api.Common.Settings
{
    /// <summary>
    /// Настройки Hangfire.
    /// </summary>
    public class HangfireSettings :
        ISettings
    {
        /// <summary>
        /// Путь к dashboard Hangfire.
        /// </summary>
        public string PathMatch { get; set; }

        /// <summary>
        /// Заголовок dashboard.
        /// </summary>
        public string DashboardTitle { get; set; }

        /// <summary>
        /// Путь для ссылку возвращения к ресурсу,
        /// откуда изначально был переход на dashboard.
        /// </summary>
        public string AppPath { get; set; }

        /// <summary>
        /// Имя сервера.
        /// </summary>
        public string ServerName { get; set; }
    }
}