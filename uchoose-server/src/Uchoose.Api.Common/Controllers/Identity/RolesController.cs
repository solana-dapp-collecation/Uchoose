// ------------------------------------------------------------------------------------------------------
// <copyright file="RolesController.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Identity.Abstractions;
using Uchoose.Api.Common.Swagger.Examples.Common.Responses;
using Uchoose.Api.Common.Swagger.Examples.Identity.Roles.Responses;
using Uchoose.CurrentUserService.Interfaces;
using Uchoose.RoleClaimService.Interfaces;
using Uchoose.RoleClaimService.Interfaces.Requests;
using Uchoose.RoleClaimService.Interfaces.Responses;
using Uchoose.RoleService.Interfaces;
using Uchoose.RoleService.Interfaces.Requests;
using Uchoose.RoleService.Interfaces.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Controllers.Identity
{
    /// <summary>
    /// Контроллер для работы с ролями пользователей.
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [SwaggerTag("Роли пользователей.")]
    internal sealed class RolesController :
        IdentityBaseController
    {
        private readonly IRoleService _roleService;
        private readonly IRoleClaimService _roleClaimService;
        private readonly ICurrentUserService _currentUserService;

        /// <summary>
        /// Инициализирует экземпляр <see cref="RolesController"/>.
        /// </summary>
        /// <param name="roleService"><see cref="IRoleService"/>.</param>
        /// <param name="roleClaimService"><see cref="IRoleClaimService"/>.</param>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        public RolesController(
            IRoleService roleService,
            IRoleClaimService roleClaimService,
            ICurrentUserService currentUserService)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _roleClaimService = roleClaimService ?? throw new ArgumentNullException(nameof(roleClaimService));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        /// <summary>
        /// Получить роль пользователя по её идентификатору.
        /// </summary>
        /// <param name="id" example="00000000-0000-0000-0000-000000000000">Идентификатор роли пользователя.</param>
        /// <returns>Возвращает роль пользователя.</returns>
        /// <response code="200">Возвращает роль пользователя.</response>
        [MapToApiVersion("1")]
        [HttpGet("{id}", Name = "GetRoleById")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Roles.View)]
        [SwaggerOperation(
            OperationId = "GetRoleById",
            Tags = new[] { RolesTag })]
        [ProducesResponseType(typeof(Result<RoleResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoleResponseExample))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var result = await _roleService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Получить список ролей авторизованного пользователя.
        /// </summary>
        /// <returns>Возвращает роль авторизованного пользователя.</returns>
        /// <response code="200">Возвращает роль авторизованного пользователя.</response>
        [MapToApiVersion("1")]
        [HttpGet("user", Name = "GetCurrentUserRole")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Roles.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetCurrentUserRole",
            Tags = new[] { RolesTag })]
        [ProducesResponseType(typeof(Result<List<RoleResponse>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RolesResponseExample))]
        public async Task<IActionResult> GetCurrentUserRoleAsync()
        {
            var currentUserId = _currentUserService.GetUserId();
            var result = await _roleService.GetRolesByUserIdAsync(currentUserId);
            return Ok(result);
        }

        /// <summary>
        /// Получить список всех ролей пользователей.
        /// </summary>
        /// <returns>Возвращает список ролей пользователей.</returns>
        /// <response code="200">Возвращает список ролей пользователей.</response>
        [MapToApiVersion("1")]
        [HttpGet(Name = "GetAllRoles")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Roles.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetAllRoles",
            Tags = new[] { RolesTag })]
        [ProducesResponseType(typeof(Result<List<RoleResponse>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RolesResponseExample))]
        public async Task<IActionResult> GetAllAsync()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        /// <summary>
        /// Добавить роль пользователя.
        /// </summary>
        /// <param name="request">Запрос с данными роли пользователя.</param>
        /// <returns>Возвращает идентификатор созданной роли пользователя.</returns>
        /// <response code="201">Возвращает идентификатор созданной роли пользователя.</response>
        [MapToApiVersion("1")]
        [HttpPost(Name = "CreateRole")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Roles.Create)]
        [SwaggerOperation(
            OperationId = "CreateRole",
            Tags = new[] { RolesTag })]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status201Created)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(ResultWithGuidIdResponseExample))]
        public async Task<IActionResult> CreateAsync(RoleRequest request)
        {
            var result = await _roleService.SaveAsync(request);
            return CreatedAtRoute("GetRoleById", new { id = result.Data, version = ApiVersion }, result);
        }

        /// <summary>
        /// Удалить роль пользователя по её идентификатору.
        /// </summary>
        /// <param name="id" example="00000000-0000-0000-0000-000000000000">Идентификатор роли пользователя.</param>
        /// <returns>Возвращает идентификатор удалённой роли пользователя.</returns>
        /// <response code="200">Возвращает идентификатор удалённой роли пользователя.</response>
        [MapToApiVersion("1")]
        [HttpDelete("{id}", Name = "DeleteRoleById")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Roles.Delete)]
        [SwaggerOperation(
            OperationId = "DeleteRoleById",
            Tags = new[] { RolesTag })]
        [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultWithGuidIdResponseExample))]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var result = await _roleService.DeleteAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Получить разрешение роли пользователя.
        /// </summary>
        /// <param name="id" example="0">Идентификатор разрешения роли пользователя.</param>
        /// <returns>Возвращает разрешение роли пользователя.</returns>
        /// <response code="200">Возвращает разрешение роли пользователя.</response>
        [MapToApiVersion("1")]
        [HttpGet("permissions/{id:int}", Name = "GetRoleClaimById")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.RoleClaims.View)]
        [SwaggerOperation(
            OperationId = "GetRoleClaimById",
            Tags = new[] { RolesTag })]
        [ProducesResponseType(typeof(Result<RoleClaimResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoleClaimResponseExample))]
        public async Task<IActionResult> GetClaimByIdAsync([FromRoute] int id)
        {
            var result = await _roleClaimService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Получить список всех разрешений ролей пользователей из базы данных.
        /// </summary>
        /// <returns>Возвращает список разрешений ролей пользователей из базы данных.</returns>
        /// <response code="200">Возвращает список разрешений ролей пользователей из базы данных.</response>
        [MapToApiVersion("1")]
        [HttpGet("permissions", Name = "GetAllRoleClaims")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.RoleClaims.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetAllRoleClaims",
            Tags = new[] { RolesTag })]
        [ProducesResponseType(typeof(Result<List<RoleClaimResponse>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoleClaimsResponseExample))]
        public async Task<IActionResult> GetAllClaimsAsync()
        {
            var result = await _roleClaimService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Получить список разрешений указанной роли пользователя.
        /// </summary>
        /// <param name="roleId" example="00000000-0000-0000-0000-000000000000">Идентификатор роли пользователя.</param>
        /// <returns>Возвращает список разрешений указанной роли пользователя.</returns>
        /// <response code="200">Возвращает список разрешений указанной роли пользователя.</response>
        [MapToApiVersion("1")]
        [HttpGet("permissions/role/{roleId:guid}", Name = "GetRolePermissions")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.RoleClaims.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetRolePermissions",
            Tags = new[] { RolesTag })]
        [ProducesResponseType(typeof(Result<List<RolePermissionsResponse>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RolePermissionsResponseExample))]
        public async Task<IActionResult> GetPermissionsByRoleIdAsync([FromRoute] Guid roleId)
        {
            var result = await _roleClaimService.GetAllPermissionsAsync(roleId);
            return Ok(result);
        }

        /// <summary>
        /// Обновить разрешения указанной роли пользователя.
        /// </summary>
        /// <param name="request">Запрос с данными разрешений роли пользователя.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        /// <response code="200">Возвращает результат выполнения операции.</response>
        [MapToApiVersion("1")]
        [HttpPut("permissions/update", Name = "UpdateRolePermissions")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.RoleClaims.Edit)]
        [SwaggerOperation(
            OperationId = "UpdateRolePermissions",
            Tags = new[] { RolesTag })]
        [ProducesResponseType(typeof(IResult), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(IResultResponseExample))]
        public async Task<IActionResult> UpdatePermissionsAsync(RolePermissionRequest request)
        {
            var result = await _roleClaimService.UpdatePermissionsAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Удалить разрешение роли пользователя.
        /// </summary>
        /// <param name="id" example="0">Идентификатор разрешения роли пользователя.</param>
        /// <returns>Возвращает идентификатор удалённого разрешения роли пользователя.</returns>
        /// <response code="200">Возвращает идентификатор удалённого разрешения роли пользователя.</response>
        [MapToApiVersion("1")]
        [HttpDelete("permissions/{id:int}", Name = "DeleteRoleClaim")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.RoleClaims.Delete)]
        [SwaggerOperation(
            OperationId = "DeleteRoleClaim",
            Tags = new[] { RolesTag })]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultWithIntIdResponseExample))]
        public async Task<IActionResult> DeleteClaimByIdAsync([FromRoute] int id)
        {
            var result = await _roleClaimService.DeleteAsync(id);
            return Ok(result);
        }
    }
}