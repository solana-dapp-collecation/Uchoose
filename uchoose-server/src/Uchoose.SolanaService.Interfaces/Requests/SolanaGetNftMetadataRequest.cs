// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaGetNftMetadataRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.SolanaService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на получение метаданных NFT.
    /// </summary>
    public class SolanaGetNftMetadataRequest
    {
        /// <summary>
        /// Адрес аккаунта.
        /// </summary>
        /// <example>7Kqp...irsM</example>
        public string AccountAddress { get; set; }
    }
}