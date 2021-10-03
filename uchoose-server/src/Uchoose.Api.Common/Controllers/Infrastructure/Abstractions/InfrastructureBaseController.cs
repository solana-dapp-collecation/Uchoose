// ------------------------------------------------------------------------------------------------------
// <copyright file="InfrastructureBaseController.cs" company="Life Loop">
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

namespace Uchoose.Api.Common.Controllers.Infrastructure.Abstractions
{
    /// <summary>
    /// Базовый контроллер для работы с инфраструктурой.
    /// </summary>
    [ApiController]
    [Route(BasePath + "/[controller]")]
    public abstract class InfrastructureBaseController :
        BaseController
    {
        /// <summary>
        /// Базовый путь для роутинга.
        /// </summary>
        protected internal new const string BasePath = "api/v{version:apiVersion}/infrastructure";

        /// <summary>
        /// Общий тэг для операций, связанных с инфраструктурой.
        /// </summary>
        protected internal const string InfrastructureTag = "Infrastructure";
    }

    /// <summary>
    /// Контроллер для объединения всех действий, связанных с инфраструктурой.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [SwaggerTag("Инфраструктура.")]
    internal sealed class InfrastructureController : InfrastructureBaseController
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
            Tags = new[] { InfrastructureTag })]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PingResponseExample))]
        public IActionResult Ping()
        {
            return Ok("Ok");
        }
    }
}