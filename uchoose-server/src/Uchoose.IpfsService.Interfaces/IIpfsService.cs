// ------------------------------------------------------------------------------------------------------
// <copyright file="IIpfsService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Ipfs.Http;
using Uchoose.Utils.Contracts.Services;

namespace Uchoose.IpfsService.Interfaces
{
    /// <summary>
    /// Сервис для работы с IPFS.
    /// </summary>
    public interface IIpfsService :
        IInfrastructureService
    {
        /// <summary>
        /// Получить <see cref="IpfsClient"/>.
        /// </summary>
        public IpfsClient GetIpfsClient { get; }
    }
}