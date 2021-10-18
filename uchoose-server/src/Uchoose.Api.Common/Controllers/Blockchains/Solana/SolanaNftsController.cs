// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaNftsController.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Uchoose.Api.Common.Controllers.Blockchains.Solana.Abstractions;
using Uchoose.SolanaService.Interfaces;
using Uchoose.SolanaService.Interfaces.Requests;
using Uchoose.SolanaService.Interfaces.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Controllers.Blockchains.Solana
{
    /// <summary>
    /// Контроллер для работы с NFT в Solana.
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route(BasePath + "/nfts")]
    [SwaggerTag("NFT в Solana.")]
    internal sealed class SolanaNftsController :
        SolanaBaseController
    {
        /// <summary>
        /// Общий тэг для операций, связанных с NFT в Solana.
        /// </summary>
        private const string SolanaNftsTag = "SolanaNfts";

        private readonly IStringLocalizer<SolanaNftsController> _localizer;
        private readonly ISolanaService _solanaService;

        /// <summary>
        /// Инициализирует экземпляр <see cref="SolanaNftsController"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="solanaService"><see cref="ISolanaService"/>.</param>
        public SolanaNftsController(
            IStringLocalizer<SolanaNftsController> localizer,
            ISolanaService solanaService)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            _solanaService = solanaService ?? throw new ArgumentNullException(nameof(solanaService));
        }

        /// <summary>
        /// Сминтить NFT.
        /// </summary>
        /// <param name="request">Запрос на минтинг NFT.</param>
        /// <returns>Возвращает hash проведённой транзакции минтинга NFT.</returns>
        /// <response code="200">Возвращает hash проведённой транзакции минтинга NFT.</response>
        [MapToApiVersion("1")]
        [HttpPost("mint", Name = "MintSolanaNft")]

        // [Authorize(Policy = Application.Constants.Permission.Permissions.SolanaNfts.Mint)]
        [AllowAnonymous] // TODO - потом поменять
        [SwaggerOperation(
            OperationId = "MintSolanaNft",
            Tags = new[] { SolanaTag, SolanaNftsTag })]
        [ProducesResponseType(typeof(Result<SolanaMintNftResponse>), StatusCodes.Status200OK)]

        // [SwaggerResponseExample(StatusCodes.Status200OK, typeof())] // TODO - добавить пример
        public async Task<IActionResult> MintNftAsync([FromBody] SolanaMintNftRequest request)
        {
            // TODO - добавить реализацию через CQRS (потом)
            var result = await _solanaService.MintNftAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Получить метаданные NFT.
        /// </summary>
        /// <param name="request">Запрос на получение метаданных NFT.</param>
        /// <returns>Возвращает метаданные NFT.</returns>
        /// <response code="200">Возвращает метаданные NFT.</response>
        [HttpGet("metadata", Name = "GetSolanaNftMetadata")]

        // [Authorize(Policy = Application.Constants.Permission.Permissions.SolanaNfts.Metadata.Get)]
        [AllowAnonymous] // TODO - потом поменять
        [SwaggerOperation(
            OperationId = "GetSolanaNftMetadata",
            Tags = new[] { SolanaTag, SolanaNftsTag })]
        [ProducesResponseType(typeof(Result<SolanaGetNftMetadataResponse>), StatusCodes.Status200OK)]

        // [SwaggerResponseExample(StatusCodes.Status200OK, typeof())] // TODO - добавить пример
        public async Task<IActionResult> GetNftMetadataAsync([FromQuery] SolanaGetNftMetadataRequest request)
        {
            // TODO - добавить реализацию через CQRS (потом)
            var result = await _solanaService.GetNftMetadataAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Получить кошелёк NFT.
        /// </summary>
        /// <param name="request">Запрос на получение кошелька NFT.</param>
        /// <returns>Возвращает данные кошелька NFT.</returns>
        /// <response code="200">Возвращает данные кошелька NFT.</response>
        [HttpGet("wallet", Name = "GetSolanaNftWallet")]

        // [Authorize(Policy = Application.Constants.Permission.Permissions.SolanaNfts.Wallet.Get)]
        [AllowAnonymous] // TODO - потом поменять
        [SwaggerOperation(
            OperationId = "GetSolanaNftMetadata",
            Tags = new[] { SolanaTag, SolanaNftsTag })]
        [ProducesResponseType(typeof(Result<SolanaGetNftWalletResponse>), StatusCodes.Status200OK)]

        // [SwaggerResponseExample(StatusCodes.Status200OK, typeof())] // TODO - добавить пример
        public async Task<IActionResult> GetNftWalletAsync([FromQuery] SolanaGetNftWalletRequest request)
        {
            // TODO - добавить реализацию через CQRS (потом)
            var result = await _solanaService.GetNftWalletAsync(request);
            return Ok(result);
        }
    }
}