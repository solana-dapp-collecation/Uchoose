// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayersController.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Marketplace.Abstractions;
using Uchoose.Api.Common.Swagger.Examples.Common.Responses;
using Uchoose.Api.Common.Swagger.Examples.Marketplace.NftImageLayers.Responses;
using Uchoose.Domain.Marketplace.Entities;
using Uchoose.UseCases.Common.Features.Common.Filters;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Commands;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries.Filters;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries.Responses;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Importing;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Controllers.Marketplace
{
    /// <summary>
    /// Контроллер для работы со слоями изображения NFT.
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route(BasePath + "/nft/image/layer")]
    [SwaggerTag("Слои изображения NFT.")]
    internal sealed class NftImageLayersController :
        MarketplaceBaseController
    {
        /// <summary>
        /// Общий тэг для операций, связанных со слоями изображения NFT.
        /// </summary>
        private const string NftImageLayersTag = "NftImageLayers";

        private readonly IStringLocalizer<NftImageLayersController> _localizer;
        private readonly IStringLocalizer<NftImageLayer> _nftImageLayerLocalizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayersController"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="nftImageLayerLocalizer"><see cref="IStringLocalizer{T}"/> для <see cref="NftImageLayer"/>.</param>
        public NftImageLayersController(
            IStringLocalizer<NftImageLayersController> localizer,
            IStringLocalizer<NftImageLayer> nftImageLayerLocalizer)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            _nftImageLayerLocalizer = nftImageLayerLocalizer ?? throw new ArgumentNullException(nameof(nftImageLayerLocalizer));
        }

        /// <summary>
        /// Получить данные слоя изображения NFT по его идентификатору.
        /// </summary>
        /// <param name="filter">Фильтр для получения слоя изображения NFT.</param>
        /// <returns>Возвращает данные слоя изображения NFT.</returns>
        /// <response code="200">Возвращает данные слоя изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpGet("{id:guid}", Name = "GetNftImageLayerById")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayers.View)]
        [SwaggerOperation(
            OperationId = "GetNftImageLayerById",
            Tags = new[] { MarketplaceTag, NftImageLayersTag })]
        [ProducesResponseType(typeof(Result<NftImageLayerResponse>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(NftImageLayerResponseExample))]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, NftImageLayer> filter)
        {
            var request = Mapper.Map<GetNftImageLayerByIdQuery>(filter);
            var nftImageLayer = await Mediator.Send(request);
            return Ok(nftImageLayer);
        }

        /// <summary>
        /// Получить отфильтрованный список с данными слоёв изображения NFT.
        /// </summary>
        /// <param name="filter">Фильтр для получения слоёв изображения NFT с пагинацией.</param>
        /// <returns>Возвращает отфильтрованный список с данными слоёв изображения NFT.</returns>
        /// <response code="200">Возвращает отфильтрованный список с данными слоёв изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpGet(Name = "GetFilteredNftImageLayers")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayers.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetFilteredNftImageLayers",
            Tags = new[] { MarketplaceTag, NftImageLayersTag })]
        [ProducesResponseType(typeof(PaginatedResult<NftImageLayerResponse>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(NftImageLayersResponseExample))]
        public async Task<IActionResult> GetAllAsync([FromQuery] NftImageLayerPaginationFilter filter)
        {
            var request = Mapper.Map<GetNftImageLayersQuery>(filter);
            var nftImageLayers = await Mediator.Send(request);
            return Ok(nftImageLayers);
        }

        /// <summary>
        /// Экспортировать отфильтрованный список с данными слоёв изображения NFT.
        /// </summary>
        /// <param name="filter">Фильтр для экспорта слоёв изображения NFT с пагинацией в файл.</param>
        /// <returns>Возвращает результат выполнения операции с содержимым файла с экспортированными данными в виде base64 строки.</returns>
        /// <response code="200">Возвращает результат выполнения операции с содержимым файла с экспортированными данными в виде base64 строки.</response>
        [MapToApiVersion("1")]
        [HttpGet("export", Name = "ExportFilteredNftImageLayers")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayers.Export)]
        [SwaggerOperation(
            OperationId = "ExportFilteredNftImageLayers",
            Tags = new[] { MarketplaceTag, NftImageLayersTag })]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Octet)]
        [ProducesResponseType(typeof(IResult<string>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ExportFilteredEntityResponseExample))]
        public async Task<IActionResult> ExportAsync([FromQuery] NftImageLayersExportPaginationFilter filter)
        {
            return await ExportAsync<Guid, NftImageLayer, ExportNftImageLayersQuery, NftImageLayersExportPaginationFilter>(filter);
        }

        /// <summary>
        /// Получить словарь допустимых названий свойств c их описанием для экспорта слоёв изображения NFT.
        /// </summary>
        /// <returns>Возвращает словарь допустимых названий свойств c их описанием для экспорта слоёв изображения NFT.</returns>
        /// <response code="200">Возвращает словарь допустимых названий свойств c их описанием для экспорта слоёв изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpGet("export/exportable-properties", Name = "GetNftImageLayerExportableProperties")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayers.Export)]
        [SwaggerOperation(
            OperationId = "GetNftImageLayerExportableProperties",
            Tags = new[] { MarketplaceTag, NftImageLayersTag })]
        [ProducesResponseType(typeof(Result<Dictionary<string, string>>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EntitySearchableExportableImportablePropertiesResponseExample))]
        public async Task<IActionResult> ExportablePropertiesAsync()
        {
            var properties = IExportable<Guid, NftImageLayer>.ExportableProperties(_nftImageLayerLocalizer);
            return Ok(await Result<Dictionary<string, string>>.SuccessAsync(properties, _localizer["Exportable properties are got."]));
        }

        /// <summary>
        /// Получить словарь допустимых названий свойств c их описанием для слоёв изображения NFT, по которым можно осуществлять поиск.
        /// </summary>
        /// <returns>Возвращает словарь допустимых названий свойств c их описанием для слоёв изображения NFT, по которым можно осуществлять поиск.</returns>
        /// <response code="200">Возвращает словарь допустимых названий свойств c их описанием для слоёв изображения NFT, по которым можно осуществлять поиск.</response>
        [MapToApiVersion("1")]
        [HttpGet("search/searchable-properties", Name = "GetNftImageLayerSearchableProperties")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayers.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetNftImageLayerSearchableProperties",
            Tags = new[] { MarketplaceTag, NftImageLayersTag })]
        [ProducesResponseType(typeof(Result<Dictionary<string, string>>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EntitySearchableExportableImportablePropertiesResponseExample))]
        public async Task<IActionResult> SearchablePropertiesAsync()
        {
            var properties = ISearchable<Guid, NftImageLayer>.SearchableProperties(_nftImageLayerLocalizer);
            return Ok(await Result<Dictionary<string, string>>.SuccessAsync(properties, _localizer["Searchable properties are got."]));
        }

        /// <summary>
        /// Импортировать слои изображения NFT из файла.
        /// </summary>
        /// <param name="command">Команда для импорта слоёв изображения NFT из файла.</param>
        /// <returns>Возвращает количество импортированных слоёв изображения NFT.</returns>
        /// <response code="200">Возвращает количество импортированных слоёв изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpPost("import", Name = "ImportNftImageLayers")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayers.Import)]
        [SwaggerOperation(
            OperationId = "ImportNftImageLayers",
            Tags = new[] { MarketplaceTag, NftImageLayersTag })]
        [ProducesResponseType(typeof(IResult<int>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ImportFilteredEntityResponseExample))]
        public async Task<IActionResult> ImportAsync(ImportNftImageLayersCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Получить словарь допустимых названий свойств c их описанием для импорта слоёв изображения NFT.
        /// </summary>
        /// <returns>Возвращает словарь допустимых названий свойств c их описанием для импорта слоёв изображения NFT.</returns>
        /// <response code="200">Возвращает словарь допустимых названий свойств c их описанием для импорта слоёв изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpGet("import/importable-properties", Name = "GetNftImageLayerImportableProperties")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayers.Import)]
        [SwaggerOperation(
            OperationId = "GetNftImageLayerImportableProperties",
            Tags = new[] { MarketplaceTag, NftImageLayersTag })]
        [ProducesResponseType(typeof(Result<Dictionary<string, string>>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EntitySearchableExportableImportablePropertiesResponseExample))]
        public async Task<IActionResult> ImportablePropertiesAsync()
        {
            var properties = IImportable<Guid, NftImageLayer>.ImportableProperties(_nftImageLayerLocalizer);
            return Ok(await Result<Dictionary<string, string>>.SuccessAsync(properties, _localizer["Importable properties are got."]));
        }

        /// <summary>
        /// Добавить новый слой изображения NFT.
        /// </summary>
        /// <param name="command">Команда для добавления слоя изображения NFT.</param>
        /// <returns>Возвращает идентификатор добавленного слоя изображения NFT.</returns>
        /// <response code="201">Возвращает идентификатор добавленного слоя изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpPost(Name = "AddNftImageLayer")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayers.Add)]
        [SwaggerOperation(
            OperationId = "AddNftImageLayer",
            Tags = new[] { MarketplaceTag, NftImageLayersTag })]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status201Created)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(ResultWithGuidIdResponseExample))]
        public async Task<IActionResult> AddAsync([FromForm] AddNftImageLayerCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtRoute("GetNftImageLayerById", new { id = result.Data, version = ApiVersion }, result);
        }

        /// <summary>
        /// Обновить данные существующего слоя изображения NFT.
        /// </summary>
        /// <param name="command">Команда для обновления слоя изображения NFT.</param>
        /// <returns>Возвращает идентификатор обновлённого слоя изображения NFT.</returns>
        /// <response code="200">Возвращает идентификатор обновлённого слоя изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpPut(Name = "UpdateNftImageLayer")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayers.Update)]
        [SwaggerOperation(
            OperationId = "UpdateNftImageLayer",
            Tags = new[] { MarketplaceTag, NftImageLayersTag })]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultWithGuidIdResponseExample))]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateNftImageLayerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Удалить существующий слой изображения NFT.
        /// </summary>
        /// <param name="id" example="00000000-0000-0000-0000-000000000000">Идентификатор слоя изображения NFT.</param>
        /// <returns>Возвращает идентификатор удалённого слоя изображения NFT.</returns>
        /// <response code="200">Возвращает идентификатор удалённого слоя изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpDelete("{id:guid}", Name = "RemoveNftImageLayer")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayers.Remove)]
        [SwaggerOperation(
            OperationId = "RemoveNftImageLayer",
            Tags = new[] { MarketplaceTag, NftImageLayersTag })]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultWithGuidIdResponseExample))]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveNftImageLayerCommand(id)));
        }
    }
}