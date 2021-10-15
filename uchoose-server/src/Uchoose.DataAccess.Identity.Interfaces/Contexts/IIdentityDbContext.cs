// ------------------------------------------------------------------------------------------------------
// <copyright file="IIdentityDbContext.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Uchoose.DataAccess.Interfaces;
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.Domain.Identity.Entities;

namespace Uchoose.DataAccess.Identity.Interfaces.Contexts
{
    /// <summary>
    /// Контекст доступа к данным, связанным с Identity.
    /// </summary>
    public interface IIdentityDbContext :
        IAuditableDbContext,
        IDbContextInterface
    {
        /// <summary>
        /// Коллекция пользователей.
        /// </summary>
        public DbSet<UchooseUser> Users { get; set; }

        /// <summary>
        /// Коллекция ролей пользователей.
        /// </summary>
        public DbSet<UchooseRole> Roles { get; set; }

        /// <summary>
        /// Коллекция связей пользователей с ролями пользователей.
        /// </summary>
        public DbSet<IdentityUserRole<Guid>> UserRoles { get; set; }

        /// <summary>
        /// Коллекция разрешений ролей пользователей.
        /// </summary>
        public DbSet<UchooseRoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// Коллекция разрешений пользователей.
        /// </summary>
        public DbSet<UchooseUserClaim> UserClaims { get; set; }
    }
}