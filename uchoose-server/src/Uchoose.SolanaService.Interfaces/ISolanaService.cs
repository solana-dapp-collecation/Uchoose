// ------------------------------------------------------------------------------------------------------
// <copyright file="ISolanaService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Uchoose.SolanaService.Interfaces.Requests;
using Uchoose.SolanaService.Interfaces.Responses;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.SolanaService.Interfaces
{
    /// <summary>
    /// Сервис для работы с блокчейном Solana.
    /// </summary>
    public interface ISolanaService :
        IInfrastructureService
    {
        /// <summary>
        /// Сминтить NFT.
        /// </summary>
        /// <param name="request">Запрос на минтинг NFT.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        Task<Result<SolanaMintNftResponse>> MintNftAsync(SolanaMintNftRequest request);

        /// <summary>
        /// Получить метаданные NFT.
        /// </summary>
        /// <param name="request">Запрос на получение метаданных NFT.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        Task<Result<SolanaGetNftMetadataResponse>> GetNftMetadataAsync(SolanaGetNftMetadataRequest request);

        /// <summary>
        /// Получить кошелёк NFT.
        /// </summary>
        /// <param name="request">Запрос на получение кошелька NFT.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        Task<Result<SolanaGetNftWalletResponse>> GetNftWalletAsync(SolanaGetNftWalletRequest request);

        /// <summary>
        /// Обменять токены.
        /// </summary>
        /// <param name="request">Запрос на обмен токенов.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        Task<Result<SolanaExchangeTokenResponse>> ExchangeTokensAsync(SolanaExchangeTokenRequest request);

        /// <summary>
        /// Отправить транзакцию.
        /// </summary>
        /// <param name="request">Запрос на отправку транзакции.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        Task<Result<SolanaSendTransactionResponse>> SendTransactionAsync(SolanaSendTransactionRequest request);
    }
}