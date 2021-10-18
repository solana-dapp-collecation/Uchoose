// ------------------------------------------------------------------------------------------------------
// <copyright file="IpfsSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.IpfsService.Interfaces.Settings
{
    /// <summary>
    /// Настройки IPFS.
    /// </summary>
    public class IpfsSettings :
        ISettings
    {
        /// <summary>
        /// Url сервера IPFS HTTP API.
        /// </summary>
        public string IpfsHttpUrl { get; set; }
    }
}