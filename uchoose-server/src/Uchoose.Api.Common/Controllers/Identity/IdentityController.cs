// ------------------------------------------------------------------------------------------------------
// <copyright file="IdentityController.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Attributes.Throttling;
using Uchoose.Api.Common.Controllers.Identity.Abstractions;
using Uchoose.Api.Common.Swagger.Examples.Audit.Responses;
using Uchoose.Api.Common.Swagger.Examples.Common.Responses;
using Uchoose.AuditService.Interfaces;
using Uchoose.AuditService.Interfaces.Filters;
using Uchoose.AuditService.Interfaces.Requests;
using Uchoose.AuditService.Interfaces.Responses;
using Uchoose.DataAccess.Identity.Interfaces.Contexts;
using Uchoose.Domain.Entities;
using Uchoose.IdentityService.Interfaces;
using Uchoose.IdentityService.Interfaces.Requests;
using Uchoose.Utils.Enums;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Controllers.Identity
{
    /// <summary>
    /// Контроллер для работы с Identity.
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route(BasePath)]
    [SwaggerTag("Идентификация пользователей.")]
    internal sealed class IdentityController :
        IdentityBaseController
    {
        private readonly IIdentityService _identityService;
        private readonly IAuditService<IIdentityDbContext> _auditService;

        /// <summary>
        /// Инициализирует экземпляр <see cref="IdentityController"/>.
        /// </summary>
        /// <param name="identityService"><see cref="IIdentityService"/>.</param>
        /// <param name="auditService"><see cref="IAuditService{TIDbContext}"/>.</param>
        public IdentityController(
            IIdentityService identityService,
            IAuditService<IIdentityDbContext> auditService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
        }

        /// <summary>
        /// Зарегистрировать нового пользователя.
        /// </summary>
        /// <param name="request">Запрос на регистрацию нового пользователя.</param>
        /// <returns>Возвращает идентификатор зарегистрированного пользователя.</returns>
        /// <response code="201">Возвращает идентификатор зарегистрированного пользователя.</response>
        [MapToApiVersion("1")]
        [HttpPost("register", Name = "RegisterUser")]
        [Throttle(RequestsLimit = 1, TimeUnit = TimeUnit.Second, TimeUnitMultiplier = 2, LimitBy = ThrottleLimitBy.Ip)]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "RegisterUser",
            Tags = new[] { IdentityTag })]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status201Created)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(ResultWithGuidIdResponseExample))]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";
            string origin = string.IsNullOrEmpty(Request.Headers["origin"].ToString()) ? baseUrl : Request.Headers["origin"].ToString();
            var result = await _identityService.RegisterAsync(request, origin);

            return CreatedAtRoute("GetUserById", new { id = result.Data, version = ApiVersion }, result);
        }

        /// <summary>
        /// Подтвердить email пользователя.
        /// </summary>
        /// <param name="userId" example="00000000-0000-0000-0000-000000000000">Идентификатор пользователя.</param>
        /// <param name="code" example="example">Код для подтверждения.</param>
        /// <returns>Возвращает идентификатор пользователя с подтверждённым email.</returns>
        /// <response code="200">Возвращает идентификатор пользователя с подтверждённым email.</response>
        [MapToApiVersion("1")]
        [HttpGet("email/confirm", Name = "ConfirmEmail")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "ConfirmEmail",
            Tags = new[] { IdentityTag })]
        [ProducesResponseType(typeof(IResult<string>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultWithGuidIdResponseExample))]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] Guid userId, [FromQuery] string code)
        {
            return Ok(await _identityService.ConfirmEmailAsync(userId, code));
        }

        /// <summary>
        /// Подтвердить номер телефона пользователя.
        /// </summary>
        /// <param name="userId" example="00000000-0000-0000-0000-000000000000">Идентификатор пользователя.</param>
        /// <param name="code" example="example">Код для подтверждения.</param>
        /// <returns>Возвращает идентификатор пользователя с подтверждённым номером телефона.</returns>
        /// <response code="200">Возвращает идентификатор пользователя с подтверждённым номером телефона.</response>
        [MapToApiVersion("1")]
        [HttpGet("phone-number/confirm", Name = "ConfirmPhoneNumber")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "ConfirmPhoneNumber",
            Tags = new[] { IdentityTag })]
        [ProducesResponseType(typeof(IResult<Guid>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultWithGuidIdResponseExample))]
        public async Task<IActionResult> ConfirmPhoneNumberAsync([FromQuery] Guid userId, [FromQuery] string code)
        {
            return Ok(await _identityService.ConfirmPhoneNumberAsync(userId, code));
        }

        /// <summary>
        /// Отправить ссылку для сброса пароля пользователя по email.
        /// </summary>
        /// <param name="request">Запрос для формирования email со ссылкой для сброса пароля пользователя.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        /// <response code="200">Возвращает результат выполнения операции.</response>
        [MapToApiVersion("1")]
        [HttpPost("password/forgot", Name = "ForgotPassword")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "ForgotPassword",
            Tags = new[] { IdentityTag })]
        [ProducesResponseType(typeof(IResult), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(IResultResponseExample))]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";
            string origin = string.IsNullOrEmpty(Request.Headers["origin"].ToString()) ? baseUrl : Request.Headers["origin"].ToString();
            return Ok(await _identityService.ForgotPasswordAsync(request, origin));
        }

        /// <summary>
        /// Сбросить пароль пользователя.
        /// </summary>
        /// <param name="request">Запрос для сброса пароля пользователя.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        /// <response code="200">Возвращает результат выполнения операции.</response>
        [MapToApiVersion("1")]
        [HttpPost("password/reset", Name = "ResetPassword")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "ResetPassword",
            Tags = new[] { IdentityTag })]
        [ProducesResponseType(typeof(IResult), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(IResultResponseExample))]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return Ok(await _identityService.ResetPasswordAsync(request));
        }

        /// <summary>
        /// Получить отфильтрованный список данных аудита, связанных с Identity.
        /// </summary>
        /// <param name="filter">Фильтр для получения данных аудита с пагинацией.</param>
        /// <returns>Возвращает отфильтрованный список данных аудита, связанных с Identity.</returns>
        /// <response code="200">Возвращает отфильтрованный список данных аудита, связанных с Identity.</response>
        [MapToApiVersion("1")]
        [HttpGet("audit-trails", Name = "GetIdentityAuditTrails")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Audit.IdentityViewAll)]
        [SwaggerOperation(
            OperationId = "GetIdentityAuditTrails",
            Tags = new[] { IdentityTag, AuditTag })]
        [ProducesResponseType(typeof(PaginatedResult<AuditResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AuditTrailsResponseExample))]
        public async Task<IActionResult> GetAuditTrailsAsync([FromQuery] AuditTrailsPaginationFilter filter)
        {
            var request = Mapper.Map<GetAuditTrailsRequest>(filter);
            var result = await _auditService.GetAllAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Экспортировать отфильтрованные данные аудита, связанные с Identity.
        /// </summary>
        /// <param name="filter">Фильтр для экспорта в файл данных аудита с пагинацией.</param>
        /// <returns>Возвращает содержимое файла с экспортированными данными в виде base64 строки.</returns>
        /// <response code="200">Возвращает содержимое файла с экспортированными данными в виде base64 строки.</response>
        [MapToApiVersion("1")]
        [HttpGet("audit-trails/export", Name = "ExportIdentityAuditTrails")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Audit.IdentityExport)]
        [SwaggerOperation(
            OperationId = "ExportIdentityAuditTrails",
            Tags = new[] { IdentityTag, AuditTag })]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Octet)]
        [ProducesResponseType(typeof(IResult<string>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ExportFilteredEntityResponseExample))]

        // TODO - добавить примеры для filestream (и в остальных местах тоже)
        public async Task<IActionResult> ExportAuditTrailsAsync([FromQuery] AuditTrailsExportPaginationFilter filter)
        {
            var request = Mapper.Map<ExportAuditTrailsRequest>(filter);
            request.SetSchemaName(IdentityTag);
            var result = await _auditService.ExportToExcelAsync(request);
            if (filter.ReturnAsFileStream)
            {
                byte[] bytes = result.Data.FromBase64();
                var stream = new MemoryStream(bytes);
                return new FileStreamResult(stream, Utils.Constants.Exporting.MediaTypeNames.Application.Xlsx)
                {
                    FileDownloadName = $"{typeof(EventLog).GetGenericTypeName()}_ExportResult_{DateTimeService.NowUtc:yyyy-MM-dd_hh-mm-ss}.xlsx",
                    LastModified = DateTimeService.NowUtcOffset
                };
            }

            return Ok(result);
        }
    }
}