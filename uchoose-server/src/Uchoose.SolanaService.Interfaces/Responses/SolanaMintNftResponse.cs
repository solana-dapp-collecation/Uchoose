// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaMintNftResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.SolanaService.Interfaces.Responses.Base;

namespace Uchoose.SolanaService.Interfaces.Responses
{
    /// <summary>
    /// Ответ с результатом минтинга NFT.
    /// </summary>
    public class SolanaMintNftResponse
        : SolanaBaseTransactionResponse
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="SolanaMintNftResponse"/>.
        /// </summary>
        /// <param name="transactionHash">Hash транзакции.</param>
        public SolanaMintNftResponse(string transactionHash)
            : base(transactionHash)
        {
        }

        /// <summary>
        /// Инициализирует экземпляр <see cref="SolanaMintNftResponse"/>.
        /// </summary>
        public SolanaMintNftResponse()
        {
        }
    }
}