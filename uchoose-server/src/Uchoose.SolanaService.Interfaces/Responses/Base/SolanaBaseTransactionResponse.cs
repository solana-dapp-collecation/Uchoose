// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaBaseTransactionResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.SolanaService.Interfaces.Responses.Base
{
    /// <summary>
    /// Базовый ответ с результатом транзакции.
    /// </summary>
    public abstract class SolanaBaseTransactionResponse
    {
        /// <summary>
        /// Hash транзакции.
        /// </summary>
        public string TransactionHash { get; set; }

        /// <summary>
        /// Инициализирует экземпляр класса, наследующего <see cref="SolanaBaseTransactionResponse"/>.
        /// </summary>
        /// <param name="transactionHash">Hash транзакции.</param>
        protected SolanaBaseTransactionResponse(string transactionHash)
        {
            TransactionHash = transactionHash;
        }

        /// <summary>
        /// Инициализирует экземпляр класса, наследующего <see cref="SolanaBaseTransactionResponse"/>.
        /// </summary>
        protected SolanaBaseTransactionResponse()
        {
        }
    }
}