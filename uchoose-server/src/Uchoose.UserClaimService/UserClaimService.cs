// ------------------------------------------------------------------------------------------------------
// <copyright file="UserClaimService.cs" company="Life Loop">
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
using Uchoose.Domain.Identity.Events.UserClaims;
using Uchoose.UserClaimService.Interfaces;
using Uchoose.UserClaimService.Interfaces.Requests;
using Uchoose.UserClaimService.Interfaces.Responses;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UserClaimService
{
    /// <inheritdoc cref="IUserClaimService"/>.
    internal sealed class UserClaimService :
        IUserClaimService,
        ITransientService
    {
        private readonly UserManager<UchooseUser> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStringLocalizer<UserClaimService> _localizer;
        private readonly IMapper _mapper;
        private readonly IIdentityDbContext _db;

        /// <summary>
        /// Инициализирует экземпляр <see cref="UserClaimService"/>.
        /// </summary>
        /// <param name="userManager"><see cref="UserManager{T}"/>.</param>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="mapper"><see cref="IMapper"/>.</param>
        /// <param name="db"><see cref="IIdentityDbContext"/>.</param>
        public UserClaimService(
            UserManager<UchooseUser> userManager,
            ICurrentUserService currentUserService,
            IStringLocalizer<UserClaimService> localizer,
            IMapper mapper,
            IIdentityDbContext db)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
            _localizer = localizer;
            _mapper = mapper;
            _db = db;
        }

        /// <inheritdoc/>
        public async Task<Result<UserClaimResponse>> GetByIdAsync(int id)
        {
            var userClaim = await _db.UserClaims.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
            var userClaimResponse = _mapper.Map<UserClaimResponse>(userClaim);
            return await Result<UserClaimResponse>.SuccessAsync(userClaimResponse);
        }

        /// <inheritdoc/>
        public async Task<Result<List<UserClaimResponse>>> GetAllAsync()
        {
            var userClaims = await _db.UserClaims.AsNoTracking().ToListAsync();
            var userClaimsResponse = _mapper.Map<List<UserClaimResponse>>(userClaims);
            return await Result<List<UserClaimResponse>>.SuccessAsync(userClaimsResponse);
        }

        /// <inheritdoc/>
        public async Task<Result<List<UserClaimResponse>>> GetAllByUserIdAsync(Guid userId)
        {
            var userClaims = await _db.UserClaims
                .AsNoTracking()
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .ToListAsync();
            var userClaimsResponse = _mapper.Map<List<UserClaimResponse>>(userClaims);
            return await Result<List<UserClaimResponse>>.SuccessAsync(userClaimsResponse);
        }

        /// <inheritdoc/>
        public async Task<int> GetCountAsync()
        {
            return await _db.UserClaims.AsNoTracking().CountAsync();
        }

        /// <inheritdoc/>
        public async Task<Result<int>> SaveAsync(UserClaimRequest request)
        {
            if (request.UserId.Equals(Guid.Empty))
            {
                return await Result<int>.FailAsync(request.Id, _localizer["User is required."]);
            }

            if (request.Id == 0)
            {
                var existingUserClaim =
                    await _db.UserClaims
                        .SingleOrDefaultAsync(x =>
                            x.UserId == request.UserId && x.ClaimType == request.Type && x.ClaimValue == request.Value);
                if (existingUserClaim != null)
                {
                    return await Result<int>.FailAsync(request.Id, _localizer["Similar User Claim already exists."]);
                }

                var userClaim = _mapper.Map<UchooseUserClaim>(request);
                await _db.UserClaims.AddAsync(userClaim);
                userClaim.AddDomainEvent(new UserClaimAddedEvent(userClaim, string.Format(_localizer["User Claim '{0}' with type '{1}' added."], userClaim.ClaimValue, userClaim.ClaimType)));
                await _db.SaveChangesAsync();
                return await Result<int>.SuccessAsync(userClaim.Id, string.Format(_localizer["User Claim '{0}' added."], request.Value));
            }
            else
            {
                var existingUserClaim =
                    await _db.UserClaims
                        .Include(x => x.User)
                        .SingleOrDefaultAsync(x => x.Id == request.Id);
                if (existingUserClaim == null)
                {
                    return await Result<int>.FailAsync(request.Id, _localizer["User Claim does not exist."]);
                }
                else
                {
                    existingUserClaim.ClaimType = request.Type;
                    existingUserClaim.ClaimValue = request.Value;
                    existingUserClaim.Group = request.Group;
                    existingUserClaim.Description = request.Description;
                    existingUserClaim.UserId = request.UserId;
                    _db.UserClaims.Update(existingUserClaim);
                    existingUserClaim.AddDomainEvent(new UserClaimUpdatedEvent(existingUserClaim, string.Format(_localizer["User Claim '{0}' with type '{1}' updated."], existingUserClaim.ClaimValue, existingUserClaim.ClaimType)));
                    await _db.SaveChangesAsync();
                    return await Result<int>.SuccessAsync(existingUserClaim.Id, string.Format(_localizer["User Claim '{0}' for Role '{1}' updated."], request.Value, existingUserClaim.User.UserName));
                }
            }
        }

        /// <inheritdoc/>
        public async Task<Result<int>> DeleteAsync(int id)
        {
            var existingUserClaim = await _db.UserClaims
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (existingUserClaim != null)
            {
                _db.UserClaims.Remove(existingUserClaim);
                existingUserClaim.AddDomainEvent(new UserClaimDeletedEvent(id, existingUserClaim.UserId, string.Format(_localizer["User Claim '{0}' with type '{1}' deleted."], existingUserClaim.ClaimValue, existingUserClaim.ClaimType)));
                await _db.SaveChangesAsync();
                return await Result<int>.SuccessAsync(id, string.Format(_localizer["User Claim '{0}' for '{1}' Role deleted."], existingUserClaim.ClaimValue, existingUserClaim.User?.UserName));
            }
            else
            {
                return await Result<int>.FailAsync(id, string.Format(_localizer["User Claim with Id '{0}' does not exist."], id));
            }
        }

        /// <inheritdoc/>
        public async Task<Result<UserPermissionsResponse>> GetAllPermissionsAsync(Guid userId)
        {
            var response = new UserPermissionsResponse
            {
                UserClaims = new()
            };
            response.UserClaims.GetAllPermissions();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                response.UserId = user.Id;
                response.UserName = user.UserName;
                var allUserClaims = await GetAllAsync();
                var userClaimsResult = await GetAllByUserIdAsync(user.Id);
                if (userClaimsResult.Succeeded)
                {
                    var userClaims = userClaimsResult.Data;
                    var allClaimValues = response.UserClaims.ConvertAll(a => a.Value);
                    var userClaimValues = userClaims.ConvertAll(a => a.Value);
                    var authorizedClaims = allClaimValues.Intersect(userClaimValues).ToList();
                    foreach (var permission in response.UserClaims)
                    {
                        permission.Id = allUserClaims.Data?.SingleOrDefault(x => x.UserId == userId && x.Type == permission.Type && x.Value == permission.Value)?.Id ?? 0;
                        permission.UserId = userId;
                        if (authorizedClaims.Any(a => a == permission.Value))
                        {
                            permission.Selected = true;
                            var userClaim = userClaims.SingleOrDefault(a => a.Type == permission.Type && a.Value == permission.Value);
                            if (userClaim?.Description != null)
                            {
                                permission.Description = userClaim.Description;
                            }

                            if (userClaim?.Group != null)
                            {
                                permission.Group = userClaim.Group;
                            }
                        }
                    }
                }
                else
                {
                    response.UserClaims = new();
                    return await Result<UserPermissionsResponse>.FailAsync(userClaimsResult.Messages);
                }
            }
            else
            {
                response.UserClaims = new();
                return await Result<UserPermissionsResponse>.FailAsync(_localizer["User does not exist."]);
            }

            return await Result<UserPermissionsResponse>.SuccessAsync(response);
        }

        /// <inheritdoc/>
        public async Task<IResult> UpdatePermissionsAsync(UserPermissionRequest request)
        {
            if (request.UserId.Equals(Guid.Empty))
            {
                return await Result.FailAsync(_localizer["User is required."]);
            }

            if (request.UserClaims.Any(c => !c.Type.Equals(ApplicationClaimTypes.Permission)))
            {
                return await Result.FailAsync(string.Format(_localizer["All User Claims Type values should be '{0}'."], ApplicationClaimTypes.Permission));
            }

            if (request.UserClaims.Any(c => !c.UserId.Equals(request.UserId)))
            {
                return await Result.FailAsync(string.Format(_localizer["All User Claims should contain the same User Id as in the request."], ApplicationClaimTypes.Permission));
            }

            var errors = new List<string>();
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user != null)
            {
                if (user.IsSoftDeleted())
                {
                    throw new EntityIsDeletedException<Guid, UchooseUser>(_localizer);
                }

                if (await _userManager.IsInRoleAsync(user, RoleConstants.SuperAdmin))
                {
                    var currentUser = await _userManager.Users.SingleAsync(x => x.Id == _currentUserService.GetUserId());
                    if (!await _userManager.IsInRoleAsync(currentUser, RoleConstants.SuperAdmin))
                    {
                        return await Result.FailAsync(_localizer["Not allowed to modify Permissions for this User."]);
                    }
                }

                // удаляем не отмеченные разрешения
                foreach (var claim in request.UserClaims.Where(a => !a.Selected && a.Id != 0).ToList())
                {
                    var removeResult = await DeleteAsync(claim.Id);
                    if (!removeResult.Succeeded)
                    {
                        errors.AddRange(removeResult.Messages);
                    }
                }

                // добавляем/обновляем отмеченные разрешения
                foreach (var claim in request.UserClaims.Where(a => a.Selected).ToList())
                {
                    var saveResult = await SaveAsync(_mapper.Map<UserClaimRequest>(claim));
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
                return await Result.FailAsync(_localizer["User does not exist."]);
            }
        }
    }
}