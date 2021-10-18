// ------------------------------------------------------------------------------------------------------
// <copyright file="IpfsService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Ipfs.Http;
using Microsoft.Extensions.Options;
using Uchoose.IpfsService.Interfaces;
using Uchoose.IpfsService.Interfaces.Settings;
using Uchoose.Utils.Contracts.Services;

namespace Uchoose.IpfsService
{
    /// <inheritdoc cref="IIpfsService"/>
    public class IpfsService :
        IIpfsService,
        IScopedService
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly IpfsSettings _ipfsSettings;

        /// <summary>
        /// Инициализирует экземпляр <see cref="IpfsService"/>.
        /// </summary>
        /// <param name="ipfsSettings"><see cref="IpfsSettings"/>.</param>
        public IpfsService(
            IOptionsSnapshot<IpfsSettings> ipfsSettings)
        {
            _ipfsSettings = ipfsSettings.Value;
            GetIpfsClient = new(_ipfsSettings.IpfsHttpUrl);
        }

        /// <inheritdoc/>
        public IpfsClient GetIpfsClient { get; }
    }
}