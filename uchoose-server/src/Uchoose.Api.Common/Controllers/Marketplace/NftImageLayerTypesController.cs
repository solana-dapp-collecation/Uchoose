// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerTypesController.cs" company="Life Loop">
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
using Uchoose.Api.Common.Swagger.Examples.Marketplace.NftImageLayerTypes.Responses;
using Uchoose.Domain.Marketplace.Entities;
using Uchoose.UseCases.Common.Features.Common.Filters;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Commands;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries.Filters;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries.Responses;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Importing;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Controllers.Marketplace
{
    /// <summary>
    /// Контроллер для работы с типами слоя изображения NFT.
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route(BasePath + "/nft/image/layer/type")]
    [SwaggerTag("Типы слоя изображения NFT.")]
    internal sealed class NftImageLayerTypesController :
        MarketplaceBaseController
    {
        /// <summary>
        /// Общий тэг для операций, связанных с типами слоя изображения NFT.
        /// </summary>
        private const string NftImageLayerTypesTag = "NftImageLayerTypes";

        private readonly IStringLocalizer<NftImageLayerTypesController> _localizer;
        private readonly IStringLocalizer<NftImageLayerType> _nftImageLayerTypeLocalizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerTypesController"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="nftImageLayerTypeLocalizer"><see cref="IStringLocalizer{T}"/> для <see cref="NftImageLayerType"/>.</param>
        public NftImageLayerTypesController(
            IStringLocalizer<NftImageLayerTypesController> localizer,
            IStringLocalizer<NftImageLayerType> nftImageLayerTypeLocalizer)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            _nftImageLayerTypeLocalizer = nftImageLayerTypeLocalizer ?? throw new ArgumentNullException(nameof(nftImageLayerTypeLocalizer));
        }

        /// <summary>
        /// Получить данные типа слоя изображения NFT по его идентификатору.
        /// </summary>
        /// <param name="filter">Фильтр для получения типа слоя изображения NFT.</param>
        /// <returns>Возвращает данные типа слоя изображения NFT.</returns>
        /// <response code="200">Возвращает данные типа слоя изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpGet("{id:guid}", Name = "GetNftImageLayerTypeById")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayerTypes.View)]
        [SwaggerOperation(
            OperationId = "GetNftImageLayerTypeById",
            Tags = new[] { MarketplaceTag, NftImageLayerTypesTag })]
        [ProducesResponseType(typeof(Result<NftImageLayerTypeResponse>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(NftImageLayerTypeResponseExample))]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, NftImageLayerType> filter)
        {
            var request = Mapper.Map<GetNftImageLayerTypeByIdQuery>(filter);
            var nftImageLayerType = await Mediator.Send(request);
            return Ok(nftImageLayerType);
        }

        /// <summary>
        /// Получить отфильтрованный список с данными типов слоя изображения NFT.
        /// </summary>
        /// <param name="filter">Фильтр для получения типов слоя изображения NFT с пагинацией.</param>
        /// <returns>Возвращает отфильтрованный список с данными типов слоя изображения NFT.</returns>
        /// <response code="200">Возвращает отфильтрованный список с данными типов слоя изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpGet(Name = "GetFilteredNftImageLayerTypes")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayerTypes.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetFilteredNftImageLayerTypes",
            Tags = new[] { MarketplaceTag, NftImageLayerTypesTag })]
        [ProducesResponseType(typeof(PaginatedResult<NftImageLayerTypeResponse>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(NftImageLayerTypesResponseExample))]
        public async Task<IActionResult> GetAllAsync([FromQuery] NftImageLayerTypePaginationFilter filter)
        {
            var request = Mapper.Map<GetNftImageLayerTypesQuery>(filter);
            var nftImageLayerTypes = await Mediator.Send(request);
            return Ok(nftImageLayerTypes);
        }

        /// <summary>
        /// Экспортировать отфильтрованный список с данными типов слоя изображения NFT.
        /// </summary>
        /// <param name="filter">Фильтр для экспорта типов слоя изображения NFT с пагинацией в файл.</param>
        /// <returns>Возвращает результат выполнения операции с содержимым файла с экспортированными данными в виде base64 строки.</returns>
        /// <response code="200">Возвращает результат выполнения операции с содержимым файла с экспортированными данными в виде base64 строки.</response>
        [MapToApiVersion("1")]
        [HttpGet("export", Name = "ExportFilteredNftImageLayerTypes")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayerTypes.Export)]
        [SwaggerOperation(
            OperationId = "ExportFilteredNftImageLayerTypes",
            Tags = new[] { MarketplaceTag, NftImageLayerTypesTag })]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Octet)]
        [ProducesResponseType(typeof(IResult<string>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ExportFilteredEntityResponseExample))]
        public async Task<IActionResult> ExportAsync([FromQuery] NftImageLayerTypesExportPaginationFilter filter)
        {
            return await ExportAsync<Guid, NftImageLayerType, ExportNftImageLayerTypesQuery, NftImageLayerTypesExportPaginationFilter>(filter);
        }

        /// <summary>
        /// Получить словарь допустимых названий свойств c их описанием для экспорта типов слоя изображения NFT.
        /// </summary>
        /// <returns>Возвращает словарь допустимых названий свойств c их описанием для экспорта типов слоя изображения NFT.</returns>
        /// <response code="200">Возвращает словарь допустимых названий свойств c их описанием для экспорта типов слоя изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpGet("export/exportable-properties", Name = "GetNftImageLayerTypeExportableProperties")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayerTypes.Export)]
        [SwaggerOperation(
            OperationId = "GetNftImageLayerTypeExportableProperties",
            Tags = new[] { MarketplaceTag, NftImageLayerTypesTag })]
        [ProducesResponseType(typeof(Result<Dictionary<string, string>>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EntitySearchableExportableImportablePropertiesResponseExample))]
        public async Task<IActionResult> ExportablePropertiesAsync()
        {
            var properties = IExportable<Guid, NftImageLayerType>.ExportableProperties(_nftImageLayerTypeLocalizer);
            return Ok(await Result<Dictionary<string, string>>.SuccessAsync(properties, _localizer["Exportable properties are got."]));
        }

        /// <summary>
        /// Получить словарь допустимых названий свойств c их описанием для типов слоя изображения NFT, по которым можно осуществлять поиск.
        /// </summary>
        /// <returns>Возвращает словарь допустимых названий свойств c их описанием для типов слоя изображения NFT, по которым можно осуществлять поиск.</returns>
        /// <response code="200">Возвращает словарь допустимых названий свойств c их описанием для типов слоя изображения NFT, по которым можно осуществлять поиск.</response>
        [MapToApiVersion("1")]
        [HttpGet("search/searchable-properties", Name = "GetNftImageLayerTypeSearchableProperties")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayerTypes.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetNftImageLayerTypeSearchableProperties",
            Tags = new[] { MarketplaceTag, NftImageLayerTypesTag })]
        [ProducesResponseType(typeof(Result<Dictionary<string, string>>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EntitySearchableExportableImportablePropertiesResponseExample))]
        public async Task<IActionResult> SearchablePropertiesAsync()
        {
            var properties = ISearchable<Guid, NftImageLayerType>.SearchableProperties(_nftImageLayerTypeLocalizer);
            return Ok(await Result<Dictionary<string, string>>.SuccessAsync(properties, _localizer["Searchable properties are got."]));
        }

        /// <summary>
        /// Импортировать типы слоя изображения NFT из файла.
        /// </summary>
        /// <param name="command">Команда для импорта типов слоя изображения NFT из файла.</param>
        /// <returns>Возвращает количество импортированных типов слоя изображения NFT.</returns>
        /// <response code="200">Возвращает количество импортированных типов слоя изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpPost("import", Name = "ImportNftImageLayerTypes")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayerTypes.Import)]
        [SwaggerOperation(
            OperationId = "ImportNftImageLayerTypes",
            Tags = new[] { MarketplaceTag, NftImageLayerTypesTag })]
        [ProducesResponseType(typeof(IResult<int>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ImportFilteredEntityResponseExample))]
        public async Task<IActionResult> ImportAsync(ImportNftImageLayerTypesCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Получить словарь допустимых названий свойств c их описанием для импорта типов слоя изображения NFT.
        /// </summary>
        /// <returns>Возвращает словарь допустимых названий свойств c их описанием для импорта типов слоя изображения NFT.</returns>
        /// <response code="200">Возвращает словарь допустимых названий свойств c их описанием для импорта типов слоя изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpGet("import/importable-properties", Name = "GetNftImageLayerTypeImportableProperties")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayerTypes.Import)]
        [SwaggerOperation(
            OperationId = "GetNftImageLayerTypeImportableProperties",
            Tags = new[] { MarketplaceTag, NftImageLayerTypesTag })]
        [ProducesResponseType(typeof(Result<Dictionary<string, string>>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EntitySearchableExportableImportablePropertiesResponseExample))]
        public async Task<IActionResult> ImportablePropertiesAsync()
        {
            var properties = IImportable<Guid, NftImageLayerType>.ImportableProperties(_nftImageLayerTypeLocalizer);
            return Ok(await Result<Dictionary<string, string>>.SuccessAsync(properties, _localizer["Importable properties are got."]));
        }

        /// <summary>
        /// Добавить новый тип слоя изображения NFT.
        /// </summary>
        /// <param name="command">Команда для добавления типа слоя изображения NFT.</param>
        /// <returns>Возвращает идентификатор добавленного типа слоя изображения NFT.</returns>
        /// <response code="201">Возвращает идентификатор добавленного типа слоя изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpPost(Name = "AddNftImageLayerType")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayerTypes.Add)]
        [SwaggerOperation(
            OperationId = "AddNftImageLayerType",
            Tags = new[] { MarketplaceTag, NftImageLayerTypesTag })]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status201Created)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(ResultWithGuidIdResponseExample))]
        public async Task<IActionResult> AddAsync(AddNftImageLayerTypeCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtRoute("GetNftImageLayerTypeById", new { id = result.Data, version = ApiVersion }, result);
        }

        /// <summary>
        /// Обновить данные существующего типа слоя изображения NFT.
        /// </summary>
        /// <param name="command">Команда для обновления типа слоя изображения NFT.</param>
        /// <returns>Возвращает идентификатор обновлённого типа слоя изображения NFT.</returns>
        /// <response code="200">Возвращает идентификатор обновлённого типа слоя изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpPut(Name = "UpdateNftImageLayerType")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayerTypes.Update)]
        [SwaggerOperation(
            OperationId = "UpdateNftImageLayerType",
            Tags = new[] { MarketplaceTag, NftImageLayerTypesTag })]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultWithGuidIdResponseExample))]
        public async Task<IActionResult> UpdateAsync(UpdateNftImageLayerTypeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Удалить существующий тип слоя изображения NFT.
        /// </summary>
        /// <param name="id" example="00000000-0000-0000-0000-000000000000">Идентификатор типа слоя изображения NFT.</param>
        /// <returns>Возвращает идентификатор удалённого типа слоя изображения NFT.</returns>
        /// <response code="200">Возвращает идентификатор удалённого типа слоя изображения NFT.</response>
        [MapToApiVersion("1")]
        [HttpDelete("{id:guid}", Name = "RemoveNftImageLayerType")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.NftImageLayerTypes.Remove)]
        [SwaggerOperation(
            OperationId = "RemoveNftImageLayerType",
            Tags = new[] { MarketplaceTag, NftImageLayerTypesTag })]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultWithGuidIdResponseExample))]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveNftImageLayerTypeCommand(id)));
        }
    }
}