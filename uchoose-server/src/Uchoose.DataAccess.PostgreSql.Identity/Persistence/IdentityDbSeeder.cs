// ------------------------------------------------------------------------------------------------------
// <copyright file="IdentityDbSeeder.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Uchoose.Application.Constants.Permission;
using Uchoose.Application.Constants.Role;
using Uchoose.Application.Constants.User;
using Uchoose.DataAccess.Identity.Interfaces.Contexts;
using Uchoose.DataAccess.Interfaces;
using Uchoose.DataAccess.PostgreSql.Identity.Extensions;
using Uchoose.DataAccess.PostgreSql.Identity.Persistence.SeedData.Models;
using Uchoose.DateTimeService.Interfaces;
using Uchoose.Domain.Identity.Entities;
using Uchoose.SerializationService.Interfaces;
using Uchoose.Utils.Extensions;

namespace Uchoose.DataAccess.PostgreSql.Identity.Persistence
{
    /// <summary>
    /// Наполнитель БД данными, связанными с Identity.
    /// </summary>
    internal class IdentityDbSeeder :
        IDatabaseSeeder
    {
        private readonly ILogger<IdentityDbSeeder> _logger;
        private readonly IIdentityDbContext _context;
        private readonly UserManager<UchooseUser> _userManager;
        private readonly IDateTimeService _dateTimeService;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IStringLocalizer<IdentityDbSeeder> _localizer;
        private readonly RoleManager<UchooseRole> _roleManager;

        private readonly Guid _systemUserId;

        /// <summary>
        /// Инициализирует экземпляр <see cref="IdentityDbSeeder"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        /// <param name="context"><see cref="IIdentityDbContext"/>.</param>
        /// <param name="roleManager"><see cref="RoleManager{T}"/>.</param>
        /// <param name="userManager"><see cref="UserManager{T}"/>.</param>
        /// <param name="dateTimeService"><see cref="IDateTimeService"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public IdentityDbSeeder(
            ILogger<IdentityDbSeeder> logger,
            IIdentityDbContext context,
            RoleManager<UchooseRole> roleManager,
            UserManager<UchooseUser> userManager,
            IDateTimeService dateTimeService,
            IJsonSerializer jsonSerializer,
            IStringLocalizer<IdentityDbSeeder> localizer)
        {
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _dateTimeService = dateTimeService;
            _jsonSerializer = jsonSerializer;
            _localizer = localizer;

            _systemUserId = new(UserConstants.SystemUserId);
        }

        /// <summary>
        /// Путь к каталогу с данными для наполнения БД.
        /// </summary>
        private static string SeedDataPath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "Persistence", "SeedData");

        /// <inheritdoc/>
        public void Initialize()
        {
            AddDefaultRoles();

            AddUsers();

            _context.SaveChanges();
        }

        /// <summary>
        /// Получить список ролей пользователей по умолчанию.
        /// </summary>
        /// <returns>Возвращает список ролей пользователей по умолчанию.</returns>
        private static List<string> GetDefaultRoles()
        {
            return typeof(RoleConstants).GetAllPublicConstantValues<string>();
        }

        /// <summary>
        /// Добавить роли по умолчанию.
        /// </summary>
        private void AddDefaultRoles()
        {
            Task.Run(async () =>
            {
                foreach (string roleName in GetDefaultRoles())
                {
                    var role = new UchooseRole(roleName); // TODO - добавить описание из атрибутов
                    var roleInDb = await _roleManager.FindByNameAsync(roleName);
                    if (roleInDb == null)
                    {
                        await _roleManager.CreateAsync(role);
                        _logger.LogInformation(string.Format(_localizer["Added '{0}' to Roles"], roleName));
                    }
                }
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Добавить пользователей.
        /// </summary>
        private void AddUsers()
        {
            Task.Run(async () =>
            {
                string identityUsersData = await File.ReadAllTextAsync(Path.Combine(SeedDataPath, "identityUsers.json"), Encoding.UTF8);
                var identityUsersModels = _jsonSerializer.Deserialize<List<IdentityUsersModel>>(identityUsersData);

                if (identityUsersModels?.Any() == true)
                {
                    var defaultRoles = GetDefaultRoles();
                    foreach (var identityUsersModel in identityUsersModels)
                    {
                        if (defaultRoles.Contains(identityUsersModel.Role))
                        {
                            var role = new UchooseRole(identityUsersModel.Role); // TODO - добавить описание из атрибутов
                            var roleInDb = await _roleManager.FindByNameAsync(identityUsersModel.Role);
                            if (roleInDb == null)
                            {
                                await _roleManager.CreateAsync(role);
                                roleInDb = await _roleManager.FindByNameAsync(identityUsersModel.Role);
                            }

                            if (roleInDb.Name.Equals(RoleConstants.SuperAdmin))
                            {
                                foreach (string permission in typeof(Permissions).GetNestedClassesStaticStringValues())
                                {
                                    await _roleManager.AddPermissionClaimAsync(roleInDb, permission);
                                }
                            }

                            foreach (var user in identityUsersModel.Users)
                            {
                                var currentDateTime = _dateTimeService.NowUtc;

                                user.CreatedBy = _systemUserId;
                                user.CreatedOn = currentDateTime;
                                user.LastModifiedBy = _systemUserId;
                                user.LastModifiedOn = currentDateTime;

                                var userInDb = await _userManager.FindByEmailAsync(user.Email);
                                if (userInDb == null)
                                {
                                    await _userManager.CreateAsync(user, UserConstants.DefaultPassword);
                                    var result = await _userManager.AddToRoleAsync(user, identityUsersModel.Role);
                                    if (result.Succeeded)
                                    {
                                        _logger.LogInformation(string.Format(_localizer["Seeded Default '{0}' User With '{1}' User Name."], identityUsersModel.Role, user.UserName));
                                    }
                                    else
                                    {
                                        foreach (var error in result.Errors)
                                        {
                                            _logger.LogError(error.Description);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }).GetAwaiter().GetResult();
        }
    }
}