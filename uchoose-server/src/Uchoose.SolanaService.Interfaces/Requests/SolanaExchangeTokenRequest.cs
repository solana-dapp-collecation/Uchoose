// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaExchangeTokenRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.SolanaService.Interfaces.Requests.Base;

namespace Uchoose.SolanaService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на обмен токенов.
    /// </summary>
    public class SolanaExchangeTokenRequest
        : SolanaBaseExchangeRequest
    {
        /// <summary>
        /// Аккаунт для минтинга.
        /// </summary>
        public SolanaBaseAccountRequest MintAccount { get; set; }
    }
}