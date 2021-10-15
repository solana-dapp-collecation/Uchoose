// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaGetNftWalletResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Solnet.Extensions;
using Solnet.Extensions.Models;

namespace Uchoose.SolanaService.Interfaces.Responses
{
    /// <summary>
    /// Ответ с результатом получения кошелька NFT
    /// </summary>
    public class SolanaGetNftWalletResponse
    {
        /// <summary>
        /// Список аккаунтов.
        /// </summary>
        public TokenWalletFilterList Accounts { get; set; }

        /// <summary>
        /// Список балансов.
        /// </summary>
        public TokenWalletBalance[] Balances { get; set; }

        /// <summary>
        /// Инициализирует экземпляр <see cref="SolanaGetNftWalletResponse"/>.
        /// </summary>
        /// <param name="accounts"><see cref="TokenWalletFilterList"/>.</param>
        /// <param name="balances"><see cref="TokenWalletBalance"/>.</param>
        public SolanaGetNftWalletResponse(
            TokenWalletFilterList accounts,
            TokenWalletBalance[] balances)
        {
            Accounts = accounts;
            Balances = balances;
        }
    }
}