// ------------------------------------------------------------------------------------------------------
// <copyright file="UserExtendedAttributesController.cs" company="Life Loop">
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
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Abstractions;
using Uchoose.Api.Common.Controllers.Identity.Abstractions;
using Uchoose.Api.Common.Swagger.Examples.Common.ExtendedAttributes.Responses;
using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Filters;
using Uchoose.Domain.Identity.Entities;
using Uchoose.UseCases.Common.Features.Common.Filters;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Commands;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Controllers.Identity.ExtendedAttributes
{
    /// <summary>
    /// Контроллер для работы с расширенными атрибутами пользователей.
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route(IdentityBaseController.BasePath + "/user/attributes")]
    [SwaggerTag("Расширенные атрибуты пользователей.")]
    internal sealed class UserExtendedAttributesController :
        ExtendedAttributesController<Guid, UchooseUser>
    {
        /// <summary>
        /// Общий тэг для операций, связанных с расширенными атрибутами пользователей.
        /// </summary>
        private const string UserExtendedAttributesTag = "UserExtendedAttributes";

        /// <summary>
        /// Получить данные расширенного атрибута пользователя по его идентификатору.
        /// </summary>
        /// <param name="filter">Фильтр для получения расширенного атрибута пользователя по его идентификатору с кэшированием.</param>
        /// <returns>Возвращает данные расширенного атрибута пользователя.</returns>
        /// <response code="200">Возвращает данные расширенного атрибута пользователя.</response>
        [MapToApiVersion("1")]
        [HttpGet("{id:guid}", Name = "GetUserExtendedAttributeById")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.UsersExtendedAttributes.View)]
        [SwaggerOperation(
            OperationId = "GetUserExtendedAttributeById",
            Tags = new[] { ExtendedAttributesTag, UserExtendedAttributesTag })]
        [ProducesResponseType(typeof(Result<ExtendedAttributeResponse<Guid>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ExtendedAttributeWithGuidEntityIdResponseExample))]
        public override Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, ExtendedAttribute<Guid, UchooseUser>> filter)
        {
            return base.GetByIdAsync(filter);
        }

        /// <summary>
        /// Получить отфильтрованный список всех расширенных атрибутов пользователя.
        /// </summary>
        /// <param name="filter">Фильтр для получения расширенных атрибутов пользователя с пагинацией.</param>
        /// <returns>Возвращает отфильтрованный список расширенных атрибутов пользователя.</returns>
        /// <response code="200">Возвращает отфильтрованный список расширенных атрибутов пользователя.</response>
        [MapToApiVersion("1")]
        [HttpGet(Name = "GetFilteredUserExtendedAttributes")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.UsersExtendedAttributes.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetFilteredUserExtendedAttributes",
            Tags = new[] { ExtendedAttributesTag, UserExtendedAttributesTag })]
        [ProducesResponseType(typeof(PaginatedResult<ExtendedAttributeResponse<Guid>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ExtendedAttributesWithGuidEntityIdResponseExample))]
        public override Task<IActionResult> GetAllAsync(ExtendedAttributePaginationFilter<Guid, UchooseUser> filter)
        {
            return base.GetAllAsync(filter);
        }

        /// <summary>
        /// Добавить расширенный атрибут пользователя.
        /// </summary>
        /// <param name="command">Команда для добавления расширенного атрибута пользователя.</param>
        /// <param name="_">Имя route для получения добавленного расширенного атрибута.</param>
        /// <returns>Возвращает идентификатор добавленного расширенного атрибута пользователя.</returns>
        /// <response code="201">Возвращает идентификатор добавленного расширенного атрибута пользователя.</response>
        [MapToApiVersion("1")]
        [HttpPost(Name = "AddUserExtendedAttribute")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.UsersExtendedAttributes.Add)]
        [SwaggerOperation(
            OperationId = "AddUserExtendedAttribute",
            Tags = new[] { ExtendedAttributesTag, UserExtendedAttributesTag })]
        public override Task<IActionResult> AddAsync(AddExtendedAttributeCommand<Guid, UchooseUser> command, string _)
        {
            return base.AddAsync(command, "GetUserExtendedAttributeById");
        }

        /// <summary>
        /// Обновить расширенный атрибут пользователя.
        /// </summary>
        /// <param name="command">Команда для обновления расширенного атрибута пользователя.</param>
        /// <returns>Возвращает идентификатор обновлённого расширенного атрибута пользователя.</returns>
        /// <response code="200">Возвращает идентификатор обновлённого расширенного атрибута пользователя.</response>
        [MapToApiVersion("1")]
        [HttpPut(Name = "UpdateUserExtendedAttribute")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.UsersExtendedAttributes.Update)]
        [SwaggerOperation(
            OperationId = "UpdateUserExtendedAttribute",
            Tags = new[] { ExtendedAttributesTag, UserExtendedAttributesTag })]
        public override Task<IActionResult> UpdateAsync(UpdateExtendedAttributeCommand<Guid, UchooseUser> command)
        {
            return base.UpdateAsync(command);
        }

        /// <summary>
        /// Удалить расширенный атрибут пользователя.
        /// </summary>
        /// <param name="id" example="00000000-0000-0000-0000-000000000000">Идентификатор расширенного атрибута пользователя.</param>
        /// <returns>Возвращает идентификатор удалённого расширенного атрибута пользователя.</returns>
        /// <response code="200">Возвращает идентификатор удалённого расширенного атрибута пользователя.</response>
        [MapToApiVersion("1")]
        [HttpDelete("{id:guid}", Name = "RemoveUserExtendedAttribute")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.UsersExtendedAttributes.Remove)]
        [SwaggerOperation(
            OperationId = "RemoveUserExtendedAttribute",
            Tags = new[] { ExtendedAttributesTag, UserExtendedAttributesTag })]
        public override Task<IActionResult> RemoveAsync(Guid id)
        {
            return base.RemoveAsync(id);
        }
    }
}