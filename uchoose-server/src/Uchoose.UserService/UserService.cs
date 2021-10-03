// ------------------------------------------------------------------------------------------------------
// <copyright file="UserService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Uchoose.Application.Constants.Role;
using Uchoose.Domain.Exceptions;
using Uchoose.Domain.Identity.Entities;
using Uchoose.UserService.Interfaces;
using Uchoose.UserService.Interfaces.Models;
using Uchoose.UserService.Interfaces.Requests;
using Uchoose.UserService.Interfaces.Responses;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UserService
{
    /// <inheritdoc cref="IUserService"/>.
    internal sealed class UserService :
        IUserService,
        ITransientService
    {
        private readonly UserManager<UchooseUser> _userManager;
        private readonly RoleManager<UchooseRole> _roleManager;
        private readonly IStringLocalizer<UserService> _localizer;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserService"/>.
        /// </summary>
        /// <param name="userManager"><see cref="UserManager{T}"/>.</param>
        /// <param name="mapper"><see cref="IMapper"/>.</param>
        /// <param name="roleManager"><see cref="RoleManager{T}"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public UserService(
            UserManager<UchooseUser> userManager,
            IMapper mapper,
            RoleManager<UchooseRole> roleManager,
            IStringLocalizer<UserService> localizer)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public async Task<Result<List<UserResponse>>> GetAllAsync()
        {
            var users = await _userManager.Users.AsNoTracking().ToListAsync();
            var result = _mapper.Map<List<UserResponse>>(users);
            return await Result<List<UserResponse>>.SuccessAsync(result);
        }

        /// <inheritdoc/>
        public async Task<IResult<UserResponse>> GetAsync(Guid userId)
        {
            var user = await _userManager.Users.AsNoTracking().Where(u => u.Id == userId).FirstOrDefaultAsync();
            var result = _mapper.Map<UserResponse>(user);
            return await Result<UserResponse>.SuccessAsync(result);
        }

        /// <inheritdoc/>
        public async Task<IResult<Guid>> DeleteAsync(Guid userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new EntityNotFoundException<Guid, UchooseUser>(userId, _localizer);
            }

            bool isSuperAdmin = await _userManager.IsInRoleAsync(user, RoleConstants.SuperAdmin);
            if (isSuperAdmin)
            {
                return await Result<Guid>.FailAsync(userId, _localizer["Not Allowed."]);
            }

            if (user.IsSoftDeleted())
            {
                throw new EntityIsDeletedException<Guid, UchooseUser>(_localizer);
            }

            var identityResult = await _userManager.DeleteAsync(user);
            if (!identityResult.Succeeded)
            {
                // throw new CustomException("11", identityResult.Errors.Select(e => _localizer[e.Description].ToString()).ToList()); // TODO - придумать текст сообщения

                return await Result<Guid>.FailAsync(userId, identityResult.Errors.Select(e => _localizer[e.Description].ToString()).ToList());
            }

            return await Result<Guid>.SuccessAsync(userId, _localizer["User Deleted."]);
        }

        /// <inheritdoc/>
        public async Task<IResult<Guid>> ToggleUserStatusAsync(ToggleUserStatusRequest request)
        {
            var user = await _userManager.Users.Where(u => u.Id == request.UserId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new EntityNotFoundException<Guid, UchooseUser>(request.UserId, _localizer);
            }

            if (user.IsSoftDeleted())
            {
                throw new EntityIsDeletedException<Guid, UchooseUser>(_localizer);
            }

            bool isSuperAdmin = await _userManager.IsInRoleAsync(user, RoleConstants.SuperAdmin);
            if (isSuperAdmin)
            {
                return await Result<Guid>.FailAsync(request.UserId, _localizer["Not Allowed."]);
            }

            user.IsActive = request.ActivateUser;
            var identityResult = await _userManager.UpdateAsync(user);
            if (!identityResult.Succeeded)
            {
                return await Result<Guid>.FailAsync(request.UserId, identityResult.Errors.Select(e => _localizer[e.Description].ToString()).ToList());
            }

            return await Result<Guid>.SuccessAsync(request.UserId, _localizer["User Status Toggled."]);
        }

        /// <inheritdoc/>
        public async Task<IResult<UserRolesResponse>> GetRolesAsync(Guid userId)
        {
            var viewModel = new List<UserRoleModel>();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            foreach (var role in _roleManager.Roles.AsNoTracking())
            {
                var userRolesViewModel = new UserRoleModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    Selected = await _userManager.IsInRoleAsync(user, role.Name)
                };

                viewModel.Add(userRolesViewModel);
            }

            var result = new UserRolesResponse { UserRoles = viewModel };
            return await Result<UserRolesResponse>.SuccessAsync(result);
        }

        /// <inheritdoc/>
        public async Task<IResult<Guid>> UpdateUserRolesAsync(Guid userId, UserRolesRequest request)
        {
            var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new EntityNotFoundException<Guid, UchooseUser>(userId, _localizer);
            }

            if (await _userManager.IsInRoleAsync(user, RoleConstants.SuperAdmin))
            {
                return await Result<Guid>.FailAsync(userId, _localizer["Not Allowed."]);
            }

            if (user.IsSoftDeleted())
            {
                throw new EntityIsDeletedException<Guid, UchooseUser>(_localizer);
            }

            foreach (var userRole in request.UserRoles)
            {
                if (await _roleManager.FindByNameAsync(userRole.RoleName) != null)
                {
                    if (userRole.Selected)
                    {
                        if (!await _userManager.IsInRoleAsync(user, userRole.RoleName))
                        {
                            await _userManager.AddToRoleAsync(user, userRole.RoleName);
                        }
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, userRole.RoleName);
                    }
                }
            }

            return await Result<Guid>.SuccessAsync(userId, string.Format(_localizer["User Roles Updated Successfully."]));
        }

        /// <inheritdoc/>
        public async Task<int> GetCountAsync()
        {
            return await _userManager.Users.AsNoTracking().CountAsync();
        }
    }
}