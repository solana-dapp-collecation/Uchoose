// ------------------------------------------------------------------------------------------------------
// <copyright file="KestrelSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Api.Common.Settings
{
    /// <summary>
    /// Настройки сервера Kestrel.
    /// </summary>
    public class KestrelSettings :
        ISettings
    {
        /// <summary>
        /// Порт http сервера.
        /// </summary>
        public int HttpPort { get; set; }

        /// <summary>
        /// Порт https сервера.
        /// </summary>
        public int HttpsPort { get; set; }
    }
}