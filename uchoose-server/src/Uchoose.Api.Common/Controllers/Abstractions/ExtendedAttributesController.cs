// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributesController.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Attributes.Throttling;
using Uchoose.Api.Common.Swagger.Examples.Common.Responses;
using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Filters;
using Uchoose.UseCases.Common.Features.Common.Filters;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Commands;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Enums;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Controllers.Abstractions
{
    /// <summary>
    /// Контроллер для работы с расширенными атрибутами сущностей.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    [ApiController]
    public abstract class ExtendedAttributesController<TEntityId, TEntity> : BaseController
        where TEntity : class, IEntity<TEntityId>
    {
        /// <summary>
        /// Получить данные расширенного атрибута сущности по его идентификатору.
        /// </summary>
        /// <param name="filter">Фильтр для получения расширенного атрибута сущности по его идентификатору с кэшированием.</param>
        /// <returns>Возвращает данные расширенного атрибута сущности.</returns>
        /// <response code="200">Возвращает данные расширенного атрибута сущности.</response>
        [HttpGet("{id:guid}")]
        public virtual async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, ExtendedAttribute<TEntityId, TEntity>> filter)
        {
            var request = Mapper.Map<GetExtendedAttributeByIdQuery<TEntityId, TEntity>>(filter);
            var extendedAttribute = await Mediator.Send(request);
            return Ok(extendedAttribute);
        }

        /// <summary>
        /// Получить отфильтрованный список всех расширенных атрибутов сущности.
        /// </summary>
        /// <param name="filter">Фильтр для получения расширенных атрибутов сущности с пагинацией.</param>
        /// <returns>Возвращает отфильтрованный список расширенных атрибутов сущности.</returns>
        /// <response code="200">Возвращает отфильтрованный список расширенных атрибутов сущности.</response>
        [HttpGet]
        public virtual async Task<IActionResult> GetAllAsync([FromQuery] ExtendedAttributePaginationFilter<TEntityId, TEntity> filter)
        {
            var extendedAttributes = await Mediator.Send(new GetExtendedAttributesQuery<TEntityId, TEntity>(filter));
            return Ok(extendedAttributes);
        }

        /// <summary>
        /// Добавить расширенный атрибут сущности.
        /// </summary>
        /// <param name="command">Команда для добавления расширенного атрибута сущности.</param>
        /// <param name="getRouteName">Имя route для получения добавленного расширенного атрибута.</param>
        /// <returns>Возвращает идентификатор добавленного расширенного атрибута сущности.</returns>
        /// <response code="201">Возвращает идентификатор добавленного расширенного атрибута сущности.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status201Created)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(ResultWithGuidIdResponseExample))]
        public virtual async Task<IActionResult> AddAsync(AddExtendedAttributeCommand<TEntityId, TEntity> command, [BindNever] string getRouteName)
        {
            var result = await Mediator.Send(command);
            return CreatedAtRoute(getRouteName, new { id = result.Data, version = ApiVersion }, result);
        }

        /// <summary>
        /// Обновить расширенный атрибут сущности.
        /// </summary>
        /// <param name="command">Команда для обновления расширенного атрибута сущности.</param>
        /// <returns>Возвращает идентификатор обновлённого расширенного атрибута сущности.</returns>
        /// <response code="200">Возвращает идентификатор обновлённого расширенного атрибута сущности.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultWithGuidIdResponseExample))]
        public virtual async Task<IActionResult> UpdateAsync(UpdateExtendedAttributeCommand<TEntityId, TEntity> command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Удалить расширенный атрибут сущности.
        /// </summary>
        /// <param name="id" example="00000000-0000-0000-0000-000000000000">Идентификатор расширенного атрибута сущности.</param>
        /// <returns>Возвращает идентификатор удалённого расширенного атрибута сущности.</returns>
        /// <response code="200">Возвращает идентификатор удалённого расширенного атрибута сущности.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultWithGuidIdResponseExample))]
        public virtual async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveExtendedAttributeCommand<TEntityId, TEntity>(id)));
        }
    }

    /// <summary>
    /// Контроллер для объединения всех действий, связанных с расширенными атрибутами сущностей.
    /// </summary>
    [Route(BasePath + "/attributes")]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [SwaggerTag("Расширенные атрибуты.")]
    internal sealed class ExtendedAttributesController : BaseController
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
            Tags = new[] { ExtendedAttributesTag })]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PingResponseExample))]
        public IActionResult Ping()
        {
            return Ok("Ok");
        }
    }
}