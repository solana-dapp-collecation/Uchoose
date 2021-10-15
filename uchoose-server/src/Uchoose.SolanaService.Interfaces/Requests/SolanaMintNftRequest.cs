// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaMintNftRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.SolanaService.Interfaces.Requests.Base;

namespace Uchoose.SolanaService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на минтинг NFT.
    /// </summary>
    public class SolanaMintNftRequest
        : SolanaBaseExchangeRequest
    {
        /// <summary>
        /// Аккаунт для минтинга.
        /// </summary>
        /// <example>7Kqp...irsM</example>
        public SolanaBaseAccountRequest MintAccount { get; set; }

        /// <summary>
        /// Кол-во знаков после запятой у токена.
        /// </summary>
        /// <remarks>
        /// Для NFT равен 0.
        /// </remarks>
        /// <example>0</example>
        public int MintDecimals { get; set; }
    }
}