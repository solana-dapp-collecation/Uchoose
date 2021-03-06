// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaBaseAccountRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.SolanaService.Interfaces.Requests.Base
{
    /// <summary>
    /// Базовый запрос для аккаунта.
    /// </summary>
    public class SolanaBaseAccountRequest
    {
        /// <summary>
        /// Публичный ключ.
        /// </summary>
        /// <example>mvines9iiHiQTysrwkJjGf2gb9Ex9jXJX8ns3qwf2kN</example>
        public string PublicKey { get; set; }

        /// <summary>
        /// Получить индекс.
        /// </summary>
        public int GetAccountIndex()
        {
            return 0;
        }
    }
}