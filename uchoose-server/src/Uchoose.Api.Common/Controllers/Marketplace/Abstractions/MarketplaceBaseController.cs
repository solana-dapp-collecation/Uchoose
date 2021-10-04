// ------------------------------------------------------------------------------------------------------
// <copyright file="MarketplaceBaseController.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Attributes.Throttling;
using Uchoose.Api.Common.Controllers.Abstractions;
using Uchoose.Api.Common.Swagger.Examples.Common.Responses;
using Uchoose.Utils.Enums;

namespace Uchoose.Api.Common.Controllers.Marketplace.Abstractions
{
    /// <summary>
    /// Базовый контроллер для работы с маркетплейсом NFT.
    /// </summary>
    [ApiController]
    [Route(BasePath + "/[controller]")]
    public abstract class MarketplaceBaseController :
        BaseController
    {
        /// <summary>
        /// Базовый путь для роутинга.
        /// </summary>
        protected internal new const string BasePath = "api/v{version:apiVersion}/marketplace";

        /// <summary>
        /// Общий тэг для операций, связанных с маркетплейсом NFT.
        /// </summary>
        protected internal const string MarketplaceTag = "Marketplace";
    }

    /// <summary>
    /// Контроллер для объединения всех действий, связанных с маркетплейсом NFT.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [SwaggerTag("Маркетплейс.")]
    internal sealed class MarketplaceController :
        MarketplaceBaseController
    {
        /// <summary>
        /// Проверить ping API.
        /// </summary>
        /// <returns>Возвращает строку "Ok".</returns>
        /// <response code="200">Возвращает строку "Ok".</response>
        [HttpGet(nameof(Ping))]
        [Throttle(RequestsLimit = 3, TimeUnit = TimeUnit.Second, TimeUnitMultiplier = 15, LimitBy = ThrottleLimitBy.Ip)]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = nameof(Ping),
            Tags = new[] { MarketplaceTag })]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PingResponseExample))]
        public IActionResult Ping()
        {
            return Ok("Ok");
        }
    }
}