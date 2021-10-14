// ------------------------------------------------------------------------------------------------------
// <copyright file="ISolanaService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Uchoose.SolanaService.Interfaces.Requests;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.SolanaService.Interfaces
{
    /// <summary>
    /// Сервис для работы с блокчейном Solana.
    /// </summary>
    public interface ISolanaService :
        IInfrastructureService
    {
        /// <summary>
        /// Сминтить NFT.
        /// </summary>
        /// <param name="request">Запрос на minting NFT.</param>
        /// <returns>Возвращает hash транзакции.</returns>
        Task<Result<string>> MintNftAsync(MintNftRequest request);

        // TODO - добавить методы
    }
}