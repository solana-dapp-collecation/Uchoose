// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleExtendedAttributesController.cs" company="Life Loop">
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
    /// Контроллер для работы с расширенными атрибутами ролей пользователей.
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route(IdentityBaseController.BasePath + "/role/attributes")]
    [SwaggerTag("Расширенные атрибуты ролей пользователей.")]
    internal sealed class RoleExtendedAttributesController :
        ExtendedAttributesController<Guid, UchooseRole>
    {
        /// <summary>
        /// Общий тэг для операций, связанных с расширенными атрибутами ролей пользователей.
        /// </summary>
        private const string RoleExtendedAttributesTag = "RoleExtendedAttributes";

        /// <summary>
        /// Получить данные расширенного атрибута роли пользователя по его идентификатору.
        /// </summary>
        /// <param name="filter">Фильтр для получения расширенного атрибута роли пользователя по его идентификатору с кэшированием.</param>
        /// <returns>Возвращает данные расширенного атрибута роли пользователя.</returns>
        /// <response code="200">Возвращает данные расширенного атрибута роли пользователя.</response>
        [MapToApiVersion("1")]
        [HttpGet("{id:guid}", Name = "GetRoleExtendedAttributeById")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.RolesExtendedAttributes.View)]
        [SwaggerOperation(
            OperationId = "GetRoleExtendedAttributeById",
            Tags = new[] { ExtendedAttributesTag, RoleExtendedAttributesTag })]
        [ProducesResponseType(typeof(Result<ExtendedAttributeResponse<Guid>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ExtendedAttributeWithGuidEntityIdResponseExample))]
        public override Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, ExtendedAttribute<Guid, UchooseRole>> filter)
        {
            return base.GetByIdAsync(filter);
        }

        /// <summary>
        /// Получить отфильтрованный список всех расширенных атрибутов роли пользователя.
        /// </summary>
        /// <param name="filter">Фильтр для получения расширенных атрибутов роли пользователя с пагинацией.</param>
        /// <returns>Возвращает отфильтрованный список расширенных атрибутов роли пользователя.</returns>
        /// <response code="200">Возвращает отфильтрованный список расширенных атрибутов роли пользователя.</response>
        [MapToApiVersion("1")]
        [HttpGet(Name = "GetFilteredRoleExtendedAttributes")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.RolesExtendedAttributes.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetFilteredRoleExtendedAttributes",
            Tags = new[] { ExtendedAttributesTag, RoleExtendedAttributesTag })]
        [ProducesResponseType(typeof(PaginatedResult<ExtendedAttributeResponse<Guid>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ExtendedAttributesWithGuidEntityIdResponseExample))]
        public override Task<IActionResult> GetAllAsync(ExtendedAttributePaginationFilter<Guid, UchooseRole> filter)
        {
            return base.GetAllAsync(filter);
        }

        /// <summary>
        /// Добавить расширенный атрибут роли пользователя.
        /// </summary>
        /// <param name="command">Команда для добавления расширенного атрибута роли пользователя.</param>
        /// <param name="_">Имя route для получения добавленного расширенного атрибута.</param>
        /// <returns>Возвращает идентификатор добавленного расширенного атрибута роли пользователя.</returns>
        /// <response code="201">Возвращает идентификатор добавленного расширенного атрибута роли пользователя.</response>
        [MapToApiVersion("1")]
        [HttpPost(Name = "AddRoleExtendedAttribute")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.RolesExtendedAttributes.Add)]
        [SwaggerOperation(
            OperationId = "AddRoleExtendedAttribute",
            Tags = new[] { ExtendedAttributesTag, RoleExtendedAttributesTag })]
        public override Task<IActionResult> AddAsync(AddExtendedAttributeCommand<Guid, UchooseRole> command, string _)
        {
            return base.AddAsync(command, "GetRoleExtendedAttributeById");
        }

        /// <summary>
        /// Обновить расширенный атрибут роли пользователя.
        /// </summary>
        /// <param name="command">Команда для обновления расширенного атрибута роли пользователя.</param>
        /// <returns>Возвращает идентификатор обновлённого расширенного атрибута роли пользователя.</returns>
        /// <response code="200">Возвращает идентификатор обновлённого расширенного атрибута роли пользователя.</response>
        [MapToApiVersion("1")]
        [HttpPut(Name = "UpdateRoleExtendedAttribute")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.RolesExtendedAttributes.Update)]
        [SwaggerOperation(
            OperationId = "UpdateRoleExtendedAttribute",
            Tags = new[] { ExtendedAttributesTag, RoleExtendedAttributesTag })]
        public override Task<IActionResult> UpdateAsync(UpdateExtendedAttributeCommand<Guid, UchooseRole> command)
        {
            return base.UpdateAsync(command);
        }

        /// <summary>
        /// Удалить расширенный атрибут роли пользователя.
        /// </summary>
        /// <param name="id" example="00000000-0000-0000-0000-000000000000">Идентификатор расширенного атрибута роли пользователя.</param>
        /// <returns>Возвращает идентификатор удалённого расширенного атрибута роли пользователя.</returns>
        /// <response code="200">Возвращает идентификатор удалённого расширенного атрибута роли пользователя.</response>
        [MapToApiVersion("1")]
        [HttpDelete("{id:guid}", Name = "RemoveRoleExtendedAttribute")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.RolesExtendedAttributes.Remove)]
        [SwaggerOperation(
            OperationId = "RemoveRoleExtendedAttribute",
            Tags = new[] { ExtendedAttributesTag, RoleExtendedAttributesTag })]
        public override Task<IActionResult> RemoveAsync(Guid id)
        {
            return base.RemoveAsync(id);
        }
    }
}