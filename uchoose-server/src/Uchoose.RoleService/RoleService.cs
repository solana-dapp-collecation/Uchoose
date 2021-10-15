// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
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
using Uchoose.DataAccess.Identity.Interfaces.Contexts;
using Uchoose.Domain.Exceptions;
using Uchoose.Domain.Identity.Entities;
using Uchoose.Domain.Identity.Events.Roles;
using Uchoose.Domain.Identity.Exceptions;
using Uchoose.RoleService.Interfaces;
using Uchoose.RoleService.Interfaces.Requests;
using Uchoose.RoleService.Interfaces.Responses;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.RoleService
{
    /// <inheritdoc cref="IRoleService"/>.
    internal sealed class RoleService :
        IRoleService,
        ITransientService
    {
        private readonly RoleManager<UchooseRole> _roleManager;
        private readonly UserManager<UchooseUser> _userManager;
        private readonly IIdentityDbContext _context;
        private readonly IStringLocalizer<RoleService> _localizer;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует экземпляр <see cref="RoleService"/>.
        /// </summary>
        /// <param name="roleManager"><see cref="RoleManager{T}"/>.</param>
        /// <param name="mapper"><see cref="IMapper"/>.</param>
        /// <param name="userManager"><see cref="UserManager{T}"/>.</param>
        /// <param name="context"><see cref="IIdentityDbContext"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public RoleService(
            RoleManager<UchooseRole> roleManager,
            IMapper mapper,
            UserManager<UchooseUser> userManager,
            IIdentityDbContext context,
            IStringLocalizer<RoleService> localizer)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public async Task<Result<RoleResponse>> GetByIdAsync(Guid id)
        {
            var role = await _roleManager.Roles.SingleOrDefaultAsync(x => x.Id == id);
            var roleResponse = _mapper.Map<RoleResponse>(role);
            return await Result<RoleResponse>.SuccessAsync(roleResponse);
        }

        /// <inheritdoc/>
        public async Task<Result<List<RoleResponse>>> GetAllAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var rolesResponse = _mapper.Map<List<RoleResponse>>(roles);
            return await Result<List<RoleResponse>>.SuccessAsync(rolesResponse);
        }

        /// <inheritdoc/>
        public async Task<Result<List<RoleResponse>>> GetRolesByUserIdAsync(Guid userId)
        {
            var userRoles = await _context.UserRoles.AsNoTracking().Where(a => a.UserId == userId).Select(a => a.RoleId).ToListAsync();
            var roles = await _roleManager.Roles.Where(a => userRoles.Contains(a.Id)).ToListAsync();
            var rolesResponse = _mapper.Map<List<RoleResponse>>(roles);
            return await Result<List<RoleResponse>>.SuccessAsync(rolesResponse);
        }

        /// <inheritdoc/>
        public async Task<int> GetCountAsync()
        {
            return await _roleManager.Roles.CountAsync();
        }

        /// <inheritdoc/>
        public async Task<Result<Guid>> SaveAsync(RoleRequest request)
        {
            if (request.Id.Equals(Guid.Empty))
            {
                var existingRole = await _roleManager.FindByNameAsync(request.Name);
                if (existingRole != null)
                {
                    throw new IdentityException(_localizer["Similar Role already exists."], statusCode: System.Net.HttpStatusCode.BadRequest);
                }

                var newRole = new UchooseRole(request.Name, request.Description);
                var result = await _roleManager.CreateAsync(newRole);
                newRole.AddDomainEvent(new RoleAddedEvent(newRole, string.Format(_localizer["Role '{0}' created."], newRole.Name)));
                await _context.SaveChangesAsync();
                if (result.Succeeded)
                {
                    return await Result<Guid>.SuccessAsync(newRole.Id, string.Format(_localizer["Role '{0}' created."], request.Name));
                }
                else
                {
                    return await Result<Guid>.FailAsync(newRole.Id, result.Errors.Select(e => _localizer[e.Description].ToString()).ToList());
                }
            }
            else
            {
                var existingRole = await _roleManager.FindByIdAsync(request.Id.ToString());
                if (existingRole == null)
                {
                    return await Result<Guid>.FailAsync(request.Id, _localizer["Role does not exist."]);
                }

                if (existingRole.IsSoftDeleted())
                {
                    throw new EntityIsDeletedException<Guid, UchooseRole>(_localizer);
                }

                if (DefaultRoles().Contains(existingRole.Name))
                {
                    return await Result<Guid>.FailAsync(request.Id, string.Format(_localizer["Not allowed to modify '{0}' Role."], existingRole.Name));
                }

                existingRole.Name = request.Name;
                existingRole.NormalizedName = request.Name.ToUpper();
                existingRole.Description = request.Description;
                existingRole.AddDomainEvent(new RoleUpdatedEvent(existingRole, string.Format(_localizer["Role '{0}' updated."], existingRole.Name)));
                await _roleManager.UpdateAsync(existingRole);
                return await Result<Guid>.SuccessAsync(existingRole.Id, string.Format(_localizer["Role '{0}' updated."], existingRole.Name));
            }
        }

        /// <inheritdoc/>
        public async Task<Result<Guid>> DeleteAsync(Guid id)
        {
            var existingRole = await _roleManager.FindByIdAsync(id.ToString());
            if (existingRole == null)
            {
                throw new EntityNotFoundException<Guid, UchooseRole>(id, _localizer);
            }

            if (existingRole.IsSoftDeleted())
            {
                throw new EntityIsDeletedException<Guid, UchooseRole>(_localizer);
            }

            if (DefaultRoles().Contains(existingRole.Name))
            {
                return await Result<Guid>.FailAsync(id, string.Format(_localizer["Not allowed to delete '{0}' Role."], existingRole.Name));
            }

            bool roleIsNotUsed = true;
            var allUsers = await _userManager.Users.ToListAsync();
            foreach (var user in allUsers)
            {
                if (await _userManager.IsInRoleAsync(user, existingRole.Name))
                {
                    roleIsNotUsed = false;
                }
            }

            if (roleIsNotUsed)
            {
                existingRole.AddDomainEvent(new RoleDeletedEvent(id, string.Format(_localizer["Role '{0}' deleted."], existingRole.Name)));
                await _roleManager.DeleteAsync(existingRole);
                return await Result<Guid>.SuccessAsync(existingRole.Id, string.Format(_localizer["Role '{0}' deleted."], existingRole.Name));
            }
            else
            {
                return await Result<Guid>.FailAsync(id, string.Format(_localizer["Not allowed to delete '{0}' Role as it is being used."], existingRole.Name));
            }
        }

        /// <summary>
        /// Получить список ролей по умолчанию.
        /// </summary>
        /// <returns>Возвращает список ролей по умолчанию.</returns>
        private static List<string> DefaultRoles()
        {
            return typeof(RoleConstants).GetAllPublicConstantValues<string>();
        }
    }
}