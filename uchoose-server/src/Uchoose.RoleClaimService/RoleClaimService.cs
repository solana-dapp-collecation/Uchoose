// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimService.cs" company="Life Loop">
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
using Uchoose.Application.Constants.Extensions;
using Uchoose.Application.Constants.Permission;
using Uchoose.Application.Constants.Role;
using Uchoose.CurrentUserService.Interfaces;
using Uchoose.DataAccess.Identity.Interfaces.Contexts;
using Uchoose.Domain.Exceptions;
using Uchoose.Domain.Identity.Entities;
using Uchoose.Domain.Identity.Events.RoleClaims;
using Uchoose.RoleClaimService.Interfaces;
using Uchoose.RoleClaimService.Interfaces.Requests;
using Uchoose.RoleClaimService.Interfaces.Responses;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.RoleClaimService
{
    /// <inheritdoc cref="IRoleClaimService"/>.
    internal sealed class RoleClaimService :
        IRoleClaimService,
        ITransientService
    {
        private readonly RoleManager<UchooseRole> _roleManager;
        private readonly UserManager<UchooseUser> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStringLocalizer<RoleClaimService> _localizer;
        private readonly IMapper _mapper;
        private readonly IIdentityDbContext _db;

        /// <summary>
        /// Инициализирует экземпляр <see cref="RoleClaimService"/>.
        /// </summary>
        /// <param name="roleManager"><see cref="RoleManager{T}"/>.</param>
        /// <param name="userManager"><see cref="UserManager{T}"/>.</param>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="mapper"><see cref="IMapper"/>.</param>
        /// <param name="db"><see cref="IIdentityDbContext"/>.</param>
        public RoleClaimService(
            RoleManager<UchooseRole> roleManager,
            UserManager<UchooseUser> userManager,
            ICurrentUserService currentUserService,
            IStringLocalizer<RoleClaimService> localizer,
            IMapper mapper,
            IIdentityDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _currentUserService = currentUserService;
            _localizer = localizer;
            _mapper = mapper;
            _db = db;
        }

        /// <inheritdoc/>
        public async Task<Result<RoleClaimResponse>> GetByIdAsync(int id)
        {
            var roleClaim = await _db.RoleClaims.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
            var roleClaimResponse = _mapper.Map<RoleClaimResponse>(roleClaim);
            return await Result<RoleClaimResponse>.SuccessAsync(roleClaimResponse);
        }

        /// <inheritdoc/>
        public async Task<Result<List<RoleClaimResponse>>> GetAllAsync()
        {
            var roleClaims = await _db.RoleClaims.AsNoTracking().ToListAsync();
            var roleClaimsResponse = _mapper.Map<List<RoleClaimResponse>>(roleClaims);
            return await Result<List<RoleClaimResponse>>.SuccessAsync(roleClaimsResponse);
        }

        /// <inheritdoc/>
        public async Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(Guid roleId)
        {
            var roleClaims = await _db.RoleClaims
                .AsNoTracking()
                .Include(x => x.Role)
                .Where(x => x.RoleId == roleId)
                .ToListAsync();
            var roleClaimsResponse = _mapper.Map<List<RoleClaimResponse>>(roleClaims);
            return await Result<List<RoleClaimResponse>>.SuccessAsync(roleClaimsResponse);
        }

        /// <inheritdoc/>
        public async Task<int> GetCountAsync()
        {
            return await _db.RoleClaims.AsNoTracking().CountAsync();
        }

        /// <inheritdoc/>
        public async Task<Result<int>> SaveAsync(RoleClaimRequest request)
        {
            if (request.RoleId.Equals(Guid.Empty))
            {
                return await Result<int>.FailAsync(request.Id, _localizer["Role is required."]);
            }

            if (request.Id == 0)
            {
                var existingRoleClaim =
                    await _db.RoleClaims
                        .SingleOrDefaultAsync(x =>
                            x.RoleId == request.RoleId && x.ClaimType == request.Type && x.ClaimValue == request.Value);
                if (existingRoleClaim != null)
                {
                    return await Result<int>.FailAsync(request.Id, _localizer["Similar Role Claim already exists."]);
                }

                var roleClaim = _mapper.Map<UchooseRoleClaim>(request);
                await _db.RoleClaims.AddAsync(roleClaim);
                roleClaim.AddDomainEvent(new RoleClaimAddedEvent(roleClaim, string.Format(_localizer["Role Claim '{0}' with type '{1}' added."], roleClaim.ClaimValue, roleClaim.ClaimType)));
                await _db.SaveChangesAsync();
                return await Result<int>.SuccessAsync(roleClaim.Id, string.Format(_localizer["Role Claim '{0}' added."], request.Value));
            }
            else
            {
                var existingRoleClaim =
                    await _db.RoleClaims
                        .Include(x => x.Role)
                        .SingleOrDefaultAsync(x => x.Id == request.Id);
                if (existingRoleClaim == null)
                {
                    return await Result<int>.FailAsync(request.Id, _localizer["Role Claim does not exist."]);
                }
                else
                {
                    existingRoleClaim.ClaimType = request.Type;
                    existingRoleClaim.ClaimValue = request.Value;
                    existingRoleClaim.Group = request.Group;
                    existingRoleClaim.Description = request.Description;
                    existingRoleClaim.RoleId = request.RoleId;
                    _db.RoleClaims.Update(existingRoleClaim);
                    existingRoleClaim.AddDomainEvent(new RoleClaimUpdatedEvent(existingRoleClaim, string.Format(_localizer["Role Claim '{0}' with type '{1}' updated."], existingRoleClaim.ClaimValue, existingRoleClaim.ClaimType)));
                    await _db.SaveChangesAsync();
                    return await Result<int>.SuccessAsync(existingRoleClaim.Id, string.Format(_localizer["Role Claim '{0}' for Role '{1}' updated."], request.Value, existingRoleClaim.Role.Name));
                }
            }
        }

        /// <inheritdoc/>
        public async Task<Result<int>> DeleteAsync(int id)
        {
            var existingRoleClaim = await _db.RoleClaims
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (existingRoleClaim != null)
            {
                if (existingRoleClaim.Role?.Name == RoleConstants.SuperAdmin)
                {
                    return await Result<int>.FailAsync(id, string.Format(_localizer["Not allowed to delete Permissions for '{0}' Role."], existingRoleClaim.Role.Name));
                }

                _db.RoleClaims.Remove(existingRoleClaim);
                existingRoleClaim.AddDomainEvent(new RoleClaimDeletedEvent(id, existingRoleClaim.RoleId, string.Format(_localizer["Role Claim '{0}' with type '{1}' deleted."], existingRoleClaim.ClaimValue, existingRoleClaim.ClaimType)));
                await _db.SaveChangesAsync();
                return await Result<int>.SuccessAsync(id, string.Format(_localizer["Role Claim '{0}' for '{1}' Role deleted."], existingRoleClaim.ClaimValue, existingRoleClaim.Role?.Name));
            }
            else
            {
                return await Result<int>.FailAsync(id, string.Format(_localizer["Role Claim with Id '{0}' does not exist."], id));
            }
        }

        /// <inheritdoc/>
        public async Task<Result<RolePermissionsResponse>> GetAllPermissionsAsync(Guid roleId)
        {
            var response = new RolePermissionsResponse
            {
                RoleClaims = new()
            };
            response.RoleClaims.GetAllPermissions();
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role != null)
            {
                response.RoleId = role.Id;
                response.RoleName = role.Name;
                var allRoleClaims = await GetAllAsync();
                var roleClaimsResult = await GetAllByRoleIdAsync(role.Id);
                if (roleClaimsResult.Succeeded)
                {
                    var roleClaims = roleClaimsResult.Data;
                    var allClaimValues = response.RoleClaims.ConvertAll(a => a.Value);
                    var roleClaimValues = roleClaims.ConvertAll(a => a.Value);
                    var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
                    foreach (var permission in response.RoleClaims)
                    {
                        permission.Id = allRoleClaims.Data?.SingleOrDefault(x => x.RoleId == roleId && x.Type == permission.Type && x.Value == permission.Value)?.Id ?? 0;
                        permission.RoleId = roleId;
                        if (authorizedClaims.Any(a => a == permission.Value))
                        {
                            permission.Selected = true;
                            var roleClaim = roleClaims.SingleOrDefault(a => a.Type == permission.Type && a.Value == permission.Value);
                            if (roleClaim?.Description != null)
                            {
                                permission.Description = roleClaim.Description;
                            }

                            if (roleClaim?.Group != null)
                            {
                                permission.Group = roleClaim.Group;
                            }
                        }
                    }
                }
                else
                {
                    response.RoleClaims = new();
                    return await Result<RolePermissionsResponse>.FailAsync(roleClaimsResult.Messages);
                }
            }
            else
            {
                response.RoleClaims = new();
                return await Result<RolePermissionsResponse>.FailAsync(_localizer["Role does not exist."]);
            }

            return await Result<RolePermissionsResponse>.SuccessAsync(response);
        }

        /// <inheritdoc/>
        public async Task<IResult> UpdatePermissionsAsync(RolePermissionRequest request)
        {
            if (request.RoleId.Equals(Guid.Empty))
            {
                return await Result.FailAsync(_localizer["Role is required."]);
            }

            if (request.RoleClaims.Any(c => !c.Type.Equals(ApplicationClaimTypes.Permission)))
            {
                return await Result.FailAsync(string.Format(_localizer["All Role Claims Type values should be '{0}'."], ApplicationClaimTypes.Permission));
            }

            if (request.RoleClaims.Any(c => !c.RoleId.Equals(request.RoleId)))
            {
                return await Result.FailAsync(string.Format(_localizer["All Role Claims should contain the same Role Id as in the request."], ApplicationClaimTypes.Permission));
            }

            var errors = new List<string>();
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role != null)
            {
                if (role.IsSoftDeleted())
                {
                    throw new EntityIsDeletedException<Guid, UchooseRole>(_localizer);
                }

                if (role.Name == RoleConstants.SuperAdmin)
                {
                    var currentUser = await _userManager.Users.SingleAsync(x => x.Id == _currentUserService.GetUserId());
                    if (!await _userManager.IsInRoleAsync(currentUser, RoleConstants.SuperAdmin))
                    {
                        return await Result.FailAsync(_localizer["Not allowed to modify Permissions for this Role."]);
                    }
                }

                var deSelectedClaims = request.RoleClaims.Where(a => !a.Selected && a.Id != 0).ToList();
                if (role.Name == RoleConstants.SuperAdmin)
                {
                    if (deSelectedClaims.Any(x => x.Value == Permissions.Roles.View) ||
                        deSelectedClaims.Any(x => x.Value == Permissions.RoleClaims.View) ||
                        deSelectedClaims.Any(x => x.Value == Permissions.RoleClaims.Edit))
                    {
                        return await Result<string>.FailAsync(string.Format(
                            _localizer["Not allowed to deselect '{0}' or '{1}' or '{2}' for this Role."],
                            Permissions.Roles.View,
                            Permissions.RoleClaims.View,
                            Permissions.RoleClaims.Edit));
                    }
                }

                // удаляем не отмеченные разрешения
                foreach (var claim in deSelectedClaims)
                {
                    var removeResult = await DeleteAsync(claim.Id);
                    if (!removeResult.Succeeded)
                    {
                        errors.AddRange(removeResult.Messages);
                    }
                }

                // добавляем/обновляем отмеченные разрешения
                foreach (var claim in request.RoleClaims.Where(a => a.Selected).ToList())
                {
                    var saveResult = await SaveAsync(_mapper.Map<RoleClaimRequest>(claim));
                    if (!saveResult.Succeeded)
                    {
                        errors.AddRange(saveResult.Messages);
                    }
                }

                if (errors.Count > 0)
                {
                    return await Result.FailAsync(errors);
                }

                return await Result.SuccessAsync(_localizer["Permissions Updated."]);
            }
            else
            {
                return await Result.FailAsync(_localizer["Role does not exist."]);
            }
        }
    }
}