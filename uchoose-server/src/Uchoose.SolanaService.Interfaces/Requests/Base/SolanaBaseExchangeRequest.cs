﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaBaseExchangeRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.SolanaService.Interfaces.Requests.Base
{
    /// <summary>
    /// Базовый запрос для обмена токенами.
    /// </summary>
    public class SolanaBaseExchangeRequest
    {
        /// <summary>
        /// Аккаунт, с которого производится обмен.
        /// </summary>
        public SolanaBaseAccountRequest FromAccount { get; set; }

        /// <summary>
        /// Аккаунт, на который производится обмен.
        /// </summary>
        public SolanaBaseAccountRequest ToAccount { get; set; }

        /// <summary>
        /// Текст memo.
        /// </summary>
        public string MemoText { get; set; }

        /// <summary>
        /// Количество токенов.
        /// </summary>
        public ulong Amount { get; set; }
    }
}