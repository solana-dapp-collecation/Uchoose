// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaGetNftWalletRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.SolanaService.Interfaces.Requests.Base;

namespace Uchoose.SolanaService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на получение кошелька NFT.
    /// </summary>
    public class SolanaGetNftWalletRequest
    {
        /// <summary>
        /// Аккаунт владельца.
        /// </summary>
        public SolanaBaseAccountRequest OwnerAccount { get; set; }

        /// <summary>
        /// Символ для минтинга.
        /// </summary>
        public string MintSymbol { get; set; } = string.Empty;

        /// <summary>
        /// Токен для минтинга.
        /// </summary>
        /// <example>HyzkFkW3daaAy6HuRKNJFbrAzwuois6N5cHDeg5TPG2Z</example>
        public string MintToken { get; set; }

        /// <summary>
        /// Наименование токена.
        /// </summary>
        /// <example>Ape 0</example>
        public string MintName { get; set; }

        /// <summary>
        /// Количество знаков после запятой.
        /// </summary>
        /// <example>0</example>
        public int MintDecimal { get; set; }
    }
}