// ------------------------------------------------------------------------------------------------------
// <copyright file="CorsSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Api.Common.Settings
{
    /// <summary>
    /// Настройки CORS.
    /// </summary>
    public class CorsSettings :
        ISettings
    {
        /// <summary>
        /// Допустимый Url Origin для client приложения.
        /// </summary>
        public string Client { get; set; }
    }
}