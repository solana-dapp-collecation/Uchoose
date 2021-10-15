// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaSendTransactionResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.SolanaService.Interfaces.Requests;
using Uchoose.SolanaService.Interfaces.Responses.Base;

namespace Uchoose.SolanaService.Interfaces.Responses
{
    /// <summary>
    /// Ответ с результатом отправки транзакции.
    /// </summary>
    public class SolanaSendTransactionResponse
        : SolanaBaseTransactionResponse
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="SolanaSendTransactionRequest"/>.
        /// </summary>
        /// <param name="transactionHash">Hash транзакции.</param>
        public SolanaSendTransactionResponse(string transactionHash)
            : base(transactionHash)
        {
        }

        /// <summary>
        /// Инициализирует экземпляр <see cref="SolanaSendTransactionRequest"/>.
        /// </summary>
        public SolanaSendTransactionResponse()
        {
        }
    }
}