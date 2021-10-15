// ------------------------------------------------------------------------------------------------------
// <copyright file="PostgreSqlConstants.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.AspNetCore.Identity;
using Uchoose.DataAccess.PostgreSql.Identity.Persistence;
using Uchoose.Domain.Identity.Entities;

namespace Uchoose.DataAccess.PostgreSql.Identity.Constants
{
    /// <summary>
    /// Константы для MS SQL.
    /// </summary>
    public static class PostgreSqlConstants
    {
#pragma warning disable SA1201
        /// <summary>
        /// Схемы БД SQL.
        /// </summary>
        public static class Schemes
        {
            #region Identity

            /// <summary>
            /// Таблицы со схемой Identity.
            /// </summary>
            public static class Identity
            {
                /// <summary>
                /// Таблицы SQL.
                /// </summary>
                public static class Tables
                {
                    #region Users

                    /// <summary>
                    /// Имя таблицы пользователей.
                    /// </summary>
                    public const string UsersTableName = nameof(IdentityDbContext.Users);

                    /// <summary>
                    /// Таблица пользователей.
                    /// </summary>
                    public static class Users
                    {
                        #region Lengths

                        /// <summary>
                        /// Длины строк SQL.
                        /// </summary>
                        public static class Lengths
                        {
                            /// <summary>
                            /// Длина строки с <see cref="IdentityUser{TKey}.Id"/>.
                            /// </summary>
                            /// <remarks>
                            /// Фактически содержит <see cref="Guid"/> в виде строки.
                            /// </remarks>
                            public const int IdLength = 36;

                            /// <summary>
                            /// Длина строки с <see cref="IdentityUser{TKey}.Email"/>.
                            /// </summary>
                            public const int EmailLength = 100;

                            /// <summary>
                            /// Длина строки с <see cref="IdentityUser{TKey}.UserName"/>.
                            /// </summary>
                            public const int UserNameLength = 50;

                            /// <summary>
                            /// Длина строки с <see cref="UchooseUser.FirstName"/>.
                            /// </summary>
                            public const int FirstNameLength = 50;

                            /// <summary>
                            /// Длина строки с <see cref="UchooseUser.LastName"/>.
                            /// </summary>
                            public const int LastNameLength = 50;

                            /// <summary>
                            /// Длина строки с <see cref="UchooseUser.MiddleName"/>.
                            /// </summary>
                            public const int MiddleNameLength = 50;

                            /// <summary>
                            /// Длина строки с <see cref="UchooseUser.ExternalId"/>.
                            /// </summary>
                            /// <remarks>
                            /// Фактически содержит <see cref="Guid"/> в виде строки.
                            /// </remarks>
                            public const int ExternalIdLength = 36;

                            /// <summary>
                            /// Длина строки с <see cref="IdentityUser{TKey}.PhoneNumber"/>.
                            /// </summary>
                            public const int PhoneNumberLength = 20;
                        }

                        #endregion Lengths
                    }

                    #endregion Users

                    #region Roles

                    /// <summary>
                    /// Имя таблицы ролей пользователей.
                    /// </summary>
                    public const string RolesTableName = nameof(IdentityDbContext.Roles);

                    /// <summary>
                    /// Таблица ролей пользователей.
                    /// </summary>
                    public static class Roles
                    {
                        #region Lengths

                        /// <summary>
                        /// Длины строк SQL.
                        /// </summary>
                        public static class Lengths
                        {
                            /// <summary>
                            /// Длина строки с <see cref="IdentityRole{TKey}.Id"/>.
                            /// </summary>
                            /// <remarks>
                            /// Фактически содержит <see cref="Guid"/> в виде строки.
                            /// </remarks>
                            public const int IdLength = 36;

                            /// <summary>
                            /// Длина строки с <see cref="UchooseRole.Description"/>.
                            /// </summary>
                            public const int DescriptionLength = 256;
                        }

                        #endregion Lengths
                    }

                    #endregion Roles

                    #region RoleClaims

                    /// <summary>
                    /// Имя таблицы разрешений ролей пользователей.
                    /// </summary>
                    public const string RoleClaimsTableName = nameof(IdentityDbContext.RoleClaims);

                    /// <summary>
                    /// Таблица разрешений ролей пользователей.
                    /// </summary>
                    public static class RoleClaims
                    {
                        #region Lengths

                        /// <summary>
                        /// Длины строк SQL.
                        /// </summary>
                        public static class Lengths
                        {
                            /// <summary>
                            /// Длина строки с <see cref="UchooseRoleClaim.Description"/>.
                            /// </summary>
                            public const int DescriptionLength = 256;

                            /// <summary>
                            /// Длина строки с <see cref="UchooseRoleClaim.Group"/>.
                            /// </summary>
                            public const int GroupLength = 50;
                        }

                        #endregion Lengths
                    }

                    #endregion RoleClaims

                    #region UserRoles

                    /// <summary>
                    /// Имя таблицы для связи ролей пользователей с пользователями.
                    /// </summary>
                    public const string UserRolesTableName = nameof(IdentityDbContext.UserRoles);

                    /// <summary>
                    /// Таблица для связи ролей пользователей с пользователями.
                    /// </summary>
                    public static class UserRoles
                    {
                    }

                    #endregion UserRoles

                    #region UserClaims

                    /// <summary>
                    /// Имя таблицы разрешений пользователей.
                    /// </summary>
                    public const string UserClaimsTableName = nameof(IdentityDbContext.UserClaims);

                    /// <summary>
                    /// Таблица разрешений пользователей.
                    /// </summary>
                    public static class UserClaims
                    {
                        #region Lengths

                        /// <summary>
                        /// Длины строк SQL.
                        /// </summary>
                        public static class Lengths
                        {
                            /// <summary>
                            /// Длина строки с <see cref="UchooseUserClaim.Description"/>.
                            /// </summary>
                            public const int DescriptionLength = 256;

                            /// <summary>
                            /// Длина строки с <see cref="UchooseUserClaim.Group"/>.
                            /// </summary>
                            public const int GroupLength = 50;
                        }

                        #endregion Lengths
                    }

                    #endregion UserClaims

                    #region UserLogins

                    /// <summary>
                    /// Имя таблицы логинов (с провайдерами) пользователей.
                    /// </summary>
                    public const string UserLoginsTableName = nameof(IdentityDbContext.UserLogins);

                    /// <summary>
                    /// Таблица логинов (с провайдерами) пользователей.
                    /// </summary>
                    public static class UserLogins
                    {
                    }

                    #endregion UserLogins

                    #region UserTokens

                    /// <summary>
                    /// Имя таблицы токенов аутентификации пользователей.
                    /// </summary>
                    public const string UserTokensTableName = nameof(IdentityDbContext.UserTokens);

                    /// <summary>
                    /// Таблица токенов аутентификации пользователей.
                    /// </summary>
                    public static class UserTokens
                    {
                    }

                    #endregion UserTokens

                    #region ExtendedAttributes

                    /// <summary>
                    /// Имена таблиц расширенных атрибутов Identity.
                    /// </summary>
                    public static class ExtendedAttributes
                    {
                        #region UserExtendedAttributes

                        /// <summary>
                        /// Имя таблицы расширенных атрибутов пользователей.
                        /// </summary>
                        public const string UserExtendedAttributesTableName = nameof(UserExtendedAttributes);

                        /// <summary>
                        /// Таблица расширенных атрибутов пользователей.
                        /// </summary>
                        public static class UserExtendedAttributes
                        {
                        }

                        #endregion UserExtendedAttributes

                        #region RoleExtendedAttributes

                        /// <summary>
                        /// Имя таблицы расширенных атрибутов ролей пользователей.
                        /// </summary>
                        public const string RoleExtendedAttributesTableName = nameof(RoleExtendedAttributes);

                        /// <summary>
                        /// Таблица расширенных атрибутов ролей пользователей.
                        /// </summary>
                        public static class RoleExtendedAttributes
                        {
                        }

                        #endregion RoleExtendedAttributes
                    }

                    #endregion ExtendedAttributes
                }
            }

            #endregion Identity
        }
#pragma warning restore SA1201
    }
}