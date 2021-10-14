// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.SolanaService.Interfaces.Settings
{
    /// <summary>
    /// Настройки Solana.
    /// </summary>
    public class SolanaSettings :
        ISettings
    {
        /// <summary>
        /// Точки доступа кластеров Solana.
        /// </summary>
        public SolanaEndpoints Endpoints { get; set; }

        /// <summary>
        /// Программы Solana.
        /// </summary>
        public SolanaPrograms Programs { get; set; }

        /// <summary>
        /// Аккаунты Solana.
        /// </summary>
        public SolanaAccounts Accounts { get; set; }
    }

    /// <summary>
    /// Точки доступа кластеров Solana.
    /// </summary>
    public class SolanaEndpoints
    {
        /// <summary>
        /// Uri кластера DevNet.
        /// </summary>
        /// <remarks>
        /// Используется при разработке.
        /// </remarks>
        public string DevNetCluster { get; set; }

        /// <summary>
        /// Uri кластера TestNet.
        /// </summary>
        /// <remarks>
        /// используется при тестировании перед публикацией.
        /// </remarks>
        public string TestNetCluster { get; set; }

        /// <summary>
        /// Uri кластера MainNet.
        /// </summary>
        /// <remarks>
        /// Используется для боевого применения.
        /// </remarks>
        public string MainNetCluster { get; set; }
    }

    /// <summary>
    /// Программы Solana.
    /// </summary>
    public class SolanaPrograms
    {
        // TODO - добавить программы
    }

    /// <summary>
    /// Аккаунты Solana.
    /// </summary>
    public class SolanaAccounts
    {
        // TODO - добавить аккаунты
    }
}