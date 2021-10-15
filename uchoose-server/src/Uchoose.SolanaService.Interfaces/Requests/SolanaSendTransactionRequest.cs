// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaSendTransactionRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.SolanaService.Interfaces.Requests.Base;

namespace Uchoose.SolanaService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на отправку транзакции.
    /// </summary>
    public class SolanaSendTransactionRequest
        : SolanaBaseExchangeRequest
    {
        /// <summary>
        /// Количество отправляемых lampposts.
        /// </summary>
        public ulong Lampposts { get; set; }
    }
}