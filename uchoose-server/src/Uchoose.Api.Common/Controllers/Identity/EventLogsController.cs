// ------------------------------------------------------------------------------------------------------
// <copyright file="EventLogsController.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Abstractions;
using Uchoose.Api.Common.Swagger.Examples.Common.Responses;
using Uchoose.Api.Common.Swagger.Examples.Identity.EventLogs.Responses;
using Uchoose.Domain.Entities;
using Uchoose.EventLogService.Interfaces;
using Uchoose.EventLogService.Interfaces.Filters;
using Uchoose.EventLogService.Interfaces.Requests;
using Uchoose.EventLogService.Interfaces.Responses;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Controllers.Identity
{
    /// <summary>
    /// Контроллер для работы с логами событий.
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [SwaggerTag("Логи событий.")]
    internal sealed class EventLogsController :
        BaseController
    {
        private readonly IEventLogService _eventLogService;
        private readonly IStringLocalizer<EventLogsController> _localizer;
        private readonly IStringLocalizer<EventLog> _eventLogLocalizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="IdentityController"/>.
        /// </summary>
        /// <param name="eventLogService"><see cref="IEventLogService"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="eventLogLocalizer"><see cref="IStringLocalizer{T}"/> для <see cref="EventLog"/>.</param>
        public EventLogsController(
            IEventLogService eventLogService,
            IStringLocalizer<EventLogsController> localizer,
            IStringLocalizer<EventLog> eventLogLocalizer)
        {
            _eventLogService = eventLogService ?? throw new ArgumentNullException(nameof(eventLogService));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            _eventLogLocalizer = eventLogLocalizer ?? throw new ArgumentNullException(nameof(eventLogLocalizer));
        }

        /// <summary>
        /// Получить данные лога событий по его идентификатору.
        /// </summary>
        /// <param name="id" example="00000000-0000-0000-0000-000000000000">Идентификатор лога событий.</param>
        /// <returns>Возвращает данные лога событий.</returns>
        /// <response code="200">Возвращает данные лога событий.</response>
        [MapToApiVersion("1")]
        [HttpGet("{id:guid}", Name = "GetEventLogById")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.EventLogs.View)]
        [SwaggerOperation(
            OperationId = "GetEventLogById",
            Tags = new[] { EventLogsTag })]
        [ProducesResponseType(typeof(Result<EventLogResponse>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EventLogResponseExample))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var eventLog = await _eventLogService.GetByIdAsync(id);
            return Ok(eventLog);
        }

        /// <summary>
        /// Получить отфильтрованный список данных логов событий.
        /// </summary>
        /// <param name="filter">Фильтр для получения логов событий с пагинацией.</param>
        /// <returns>Возвращает список данных логов событий.</returns>
        /// <response code="200">Возвращает список данных логов событий.</response>
        [MapToApiVersion("1")]
        [HttpGet(Name = "GetEventLogs")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.EventLogs.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetEventLogs",
            Tags = new[] { EventLogsTag })]
        [ProducesResponseType(typeof(PaginatedResult<EventLogResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EventLogsResponseExample))]
        public async Task<IActionResult> GetAllAsync([FromQuery] EventLogsPaginationFilter filter)
        {
            var request = Mapper.Map<GetEventLogsRequest>(filter);
            var eventLogs = await _eventLogService.GetAllAsync(request);
            return Ok(eventLogs);
        }

        /// <summary>
        /// Добавить пользовательское событие в логи событий.
        /// </summary>
        /// <param name="request">Запрос на добавление пользовательского события в логи событий.</param>
        /// <returns>Возвращает идентификатор добавленного события.</returns>
        /// <response code="201">Возвращает идентификатор добавленного события.</response>
        [MapToApiVersion("1")]
        [HttpPost(Name = "LogCustomEvent")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.EventLogs.Create)]
        [SwaggerOperation(
            OperationId = "LogCustomEvent",
            Tags = new[] { EventLogsTag })]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status201Created)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(ResultWithGuidIdResponseExample))]
        public async Task<IActionResult> LogCustomEventAsync(LogEventRequest request)
        {
            var result = await _eventLogService.LogCustomEventAsync(request);
            return CreatedAtRoute("GetEventLogById", new { id = result.Data, version = ApiVersion }, result);
        }

        /// <summary>
        /// Экспортировать отфильтрованный список с данными логов событий.
        /// </summary>
        /// <param name="filter">Фильтр для экспорта данных логов событий с пагинацией в файл.</param>
        /// <returns>Возвращает результат выполнения операции с содержимым файла с экспортированными данными в виде base64 строки.</returns>
        /// <response code="200">Возвращает результат выполнения операции с содержимым файла с экспортированными данными в виде base64 строки.</response>
        [MapToApiVersion("1")]
        [HttpGet("export", Name = "ExportFilteredEventLogs")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.EventLogs.Export)]
        [SwaggerOperation(
            OperationId = "ExportFilteredEventLogs",
            Tags = new[] { EventLogsTag })]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Octet)]
        [ProducesResponseType(typeof(IResult<string>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ExportFilteredEntityResponseExample))]

        // TODO - добавить примеры для filestream (и в остальных местах тоже)
        public async Task<IActionResult> ExportAsync([FromQuery] EventLogsExportPaginationFilter filter)
        {
            var request = Mapper.Map<ExportEventLogsRequest>(filter);
            var result = await _eventLogService.ExportToExcelAsync(request);
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

        /// <summary>
        /// Получить словарь допустимых названий свойств c их описанием для экспорта данных логов событий.
        /// </summary>
        /// <returns>Возвращает словарь допустимых названий свойств c их описанием для экспорта данных логов событий.</returns>
        /// <response code="200">Возвращает словарь допустимых названий свойств c их описанием для экспорта данных логов событий.</response>
        [MapToApiVersion("1")]
        [HttpGet("export/exportable-properties", Name = "GetEventLogsExportableProperties")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.EventLogs.Export)]
        [SwaggerOperation(
            OperationId = "GetEventLogsExportableProperties",
            Tags = new[] { EventLogsTag })]
        [ProducesResponseType(typeof(Result<Dictionary<string, string>>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EntitySearchableExportableImportablePropertiesResponseExample))]
        public async Task<IActionResult> ExportablePropertiesAsync()
        {
            var properties = IExportable<Guid, EventLog>.ExportableProperties(_eventLogLocalizer);
            return Ok(await Result<Dictionary<string, string>>.SuccessAsync(properties, _localizer["Exportable properties are got."]));
        }

        /// <summary>
        /// Получить словарь допустимых названий свойств c их описанием для данных логов событий, по которым можно осуществлять поиск.
        /// </summary>
        /// <returns>Возвращает словарь допустимых названий свойств c их описанием для данных логов событий, по которым можно осуществлять поиск.</returns>
        /// <response code="200">Возвращает словарь допустимых названий свойств c их описанием для данных логов событий, по которым можно осуществлять поиск.</response>
        [MapToApiVersion("1")]
        [HttpGet("search/searchable-properties", Name = "GetEventLogsSearchableProperties")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.EventLogs.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetEventLogsSearchableProperties",
            Tags = new[] { EventLogsTag })]
        [ProducesResponseType(typeof(Result<Dictionary<string, string>>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EntitySearchableExportableImportablePropertiesResponseExample))]
        public async Task<IActionResult> SearchablePropertiesAsync()
        {
            var properties = ISearchable<Guid, EventLog>.SearchableProperties(_eventLogLocalizer);
            return Ok(await Result<Dictionary<string, string>>.SuccessAsync(properties, _localizer["Searchable properties are got."]));
        }
    }
}