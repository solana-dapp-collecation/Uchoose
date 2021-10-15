// ------------------------------------------------------------------------------------------------------
// <copyright file="UsersController.cs" company="Life Loop">
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
using Uchoose.Api.Common.Swagger.Examples.Identity.Users.Responses;
using Uchoose.UserClaimService.Interfaces;
using Uchoose.UserClaimService.Interfaces.Requests;
using Uchoose.UserClaimService.Interfaces.Responses;
using Uchoose.UserService.Interfaces;
using Uchoose.UserService.Interfaces.Requests;
using Uchoose.UserService.Interfaces.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Controllers.Identity
{
    /// <summary>
    /// Контроллер для работы с пользователями.
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [SwaggerTag("Пользователи.")]
    internal sealed class UsersController :
        IdentityBaseController
    {
        private readonly IUserService _userService;
        private readonly IUserClaimService _userClaimService;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UsersController"/>.
        /// </summary>
        /// <param name="userService"><see cref="IUserService"/>.</param>
        /// <param name="userClaimService"><see cref="IUserClaimService"/>.</param>
        public UsersController(
            IUserService userService,
            IUserClaimService userClaimService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _userClaimService = userClaimService ?? throw new ArgumentNullException(nameof(userClaimService));
        }

        /// <summary>
        /// Получить данные пользователя по его идентификатору.
        /// </summary>
        /// <param name="id" example="00000000-0000-0000-0000-000000000000">Идентификатор пользователя.</param>
        /// <returns>Возвращает данные пользователя.</returns>
        /// <response code="200">Возвращает данные пользователя.</response>
        [MapToApiVersion("1")]
        [HttpGet("{id:guid}", Name = "GetUserById")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Users.View)]
        [SwaggerOperation(
            OperationId = "GetUserById",
            Tags = new[] { UsersTag })]
        [ProducesResponseType(typeof(IResult<UserResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserResponseExample))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _userService.GetAsync(id);
            return Ok(user);
        }

        /// <summary>
        /// Получить список всех пользователей.
        /// </summary>
        /// <returns>Возвращает список пользователей.</returns>
        /// <response code="200">Возвращает список пользователей.</response>
        [MapToApiVersion("1")]
        [HttpGet(Name = "GetAllUsers")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Users.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetAllUsers",
            Tags = new[] { UsersTag })]
        [ProducesResponseType(typeof(Result<List<UserResponse>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UsersResponseExample))]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        /// <summary>
        /// Получить список всех ролей пользователя.
        /// </summary>
        /// <param name="id" example="00000000-0000-0000-0000-000000000000">Идентификатор пользователя.</param>
        /// <returns>Возвращает список ролей пользователя.</returns>
        /// <response code="200">Возвращает список ролей пользователя.</response>
        [MapToApiVersion("1")]
        [HttpGet("roles/{id:guid}", Name = "GetUserRoles")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Users.View)]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Roles.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetUserRoles",
            Tags = new[] { UsersTag })]
        [ProducesResponseType(typeof(Result<UserRolesResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserRolesResponseExample))]
        public async Task<IActionResult> GetRolesAsync(Guid id)
        {
            var userRoles = await _userService.GetRolesAsync(id);
            return Ok(userRoles);
        }

        /// <summary>
        /// Удалить пользователя по его идентификатору.
        /// </summary>
        /// <param name="id" example="00000000-0000-0000-0000-000000000000">Идентификатор пользователя.</param>
        /// <returns>Возвращает идентификатор удалённого пользователя.</returns>
        /// <response code="200">Возвращает идентификатор удалённого пользователя.</response>
        [MapToApiVersion("1")]
        [HttpDelete("{id:guid}", Name = "DeleteUserById")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Users.Delete)]
        [SwaggerOperation(
            OperationId = "DeleteUserById",
            Tags = new[] { UsersTag })]
        [ProducesResponseType(typeof(IResult<Guid>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultWithGuidIdResponseExample))]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            var userId = await _userService.DeleteAsync(id);
            return Ok(userId);
        }

        /// <summary>
        /// Обновить список ролей пользователя.
        /// </summary>
        /// <param name="id" example="00000000-0000-0000-0000-000000000000">Идентификатор пользователя.</param>
        /// <param name="request">Запрос со списком ролей пользователя.</param>
        /// <returns>Возвращает идентификатор пользователя.</returns>
        /// <response code="200">Возвращает идентификатор пользователя.</response>
        [MapToApiVersion("1")]
        [HttpPut("roles/{id}", Name = "UpdateUserRoles")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Users.Edit)]
        [SwaggerOperation(
            OperationId = "UpdateUserRoles",
            Tags = new[] { UsersTag })]
        [ProducesResponseType(typeof(IResult<Guid>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultWithGuidIdResponseExample))]
        public async Task<IActionResult> UpdateUserRolesAsync(Guid id, UserRolesRequest request)
        {
            var result = await _userService.UpdateUserRolesAsync(id, request);
            return Ok(result);
        }

        /// <summary>
        /// Получить разрешение пользователя.
        /// </summary>
        /// <param name="id" example="0">Идентификатор разрешения пользователя.</param>
        /// <returns>Возвращает разрешение пользователя.</returns>
        /// <response code="200">Возвращает разрешение пользователя.</response>
        [MapToApiVersion("1")]
        [HttpGet("permissions/{id:int}", Name = "GetUserClaimById")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.UserClaims.View)]
        [SwaggerOperation(
            OperationId = "GetUserClaimById",
            Tags = new[] { UsersTag })]
        [ProducesResponseType(typeof(Result<UserClaimResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserClaimResponseExample))]
        public async Task<IActionResult> GetClaimByIdAsync([FromRoute] int id)
        {
            var result = await _userClaimService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Получить список всех разрешений пользователей из базы данных.
        /// </summary>
        /// <returns>Возвращает список разрешений пользователей из базы данных.</returns>
        /// <response code="200">Возвращает список разрешений пользователей из базы данных.</response>
        [MapToApiVersion("1")]
        [HttpGet("permissions", Name = "GetAllUserClaims")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.UserClaims.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetAllUserClaims",
            Tags = new[] { UsersTag })]
        [ProducesResponseType(typeof(Result<List<UserClaimResponse>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserClaimsResponseExample))]
        public async Task<IActionResult> GetAllClaimsAsync()
        {
            var result = await _userClaimService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Получить список разрешений указанного пользователя.
        /// </summary>
        /// <param name="userId" example="00000000-0000-0000-0000-000000000000">Идентификатор пользователя.</param>
        /// <returns>Возвращает список разрешений указанного пользователя.</returns>
        /// <response code="200">Возвращает список разрешений указанного пользователя.</response>
        [MapToApiVersion("1")]
        [HttpGet("permissions/user/{userId:guid}", Name = "GetUserPermissions")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.UserClaims.ViewAll)]
        [SwaggerOperation(
            OperationId = "GetUserPermissions",
            Tags = new[] { UsersTag })]
        [ProducesResponseType(typeof(Result<List<UserPermissionsResponse>>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserPermissionsResponseExample))]
        public async Task<IActionResult> GetPermissionsByUserIdAsync([FromRoute] Guid userId)
        {
            var result = await _userClaimService.GetAllPermissionsAsync(userId);
            return Ok(result);
        }

        /// <summary>
        /// Обновить разрешения указанного пользователя.
        /// </summary>
        /// <param name="request">Запрос с данными разрешений пользователя.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        /// <response code="200">Возвращает результат выполнения операции.</response>
        [MapToApiVersion("1")]
        [HttpPut("permissions/update", Name = "UpdateUserPermissions")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.UserClaims.Edit)]
        [SwaggerOperation(
            OperationId = "UpdateUserPermissions",
            Tags = new[] { UsersTag })]
        [ProducesResponseType(typeof(IResult), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(IResultResponseExample))]
        public async Task<IActionResult> UpdatePermissionsAsync(UserPermissionRequest request)
        {
            var result = await _userClaimService.UpdatePermissionsAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Удалить разрешение пользователя.
        /// </summary>
        /// <param name="id" example="0">Идентификатор разрешения пользователя.</param>
        /// <returns>Возвращает идентификатор удалённого разрешения пользователя.</returns>
        /// <response code="200">Возвращает идентификатор удалённого разрешения пользователя.</response>
        [MapToApiVersion("1")]
        [HttpDelete("permissions/{id:int}", Name = "DeleteUserClaim")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.UserClaims.Delete)]
        [SwaggerOperation(
            OperationId = "DeleteUserClaim",
            Tags = new[] { UsersTag })]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ResultWithIntIdResponseExample))]
        public async Task<IActionResult> DeleteClaimByIdAsync([FromRoute] int id)
        {
            var result = await _userClaimService.DeleteAsync(id);
            return Ok(result);
        }
    }
}