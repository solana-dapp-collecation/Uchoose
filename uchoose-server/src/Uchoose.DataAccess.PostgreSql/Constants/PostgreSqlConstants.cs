// ------------------------------------------------------------------------------------------------------
// <copyright file="PostgreSqlConstants.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Entities;

// ReSharper disable MemberHidesStaticFromOuterClass

namespace Uchoose.DataAccess.PostgreSql.Constants
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
            #region Common

            /// <summary>
            /// Таблицы с различными схемами.
            /// </summary>
            public static class Common
            {
                #region Lengths

                /// <summary>
                /// Длины строк SQL.
                /// </summary>
                public static class Lengths
                {
                    /// <summary>
                    /// Длина строки с идентификатором <see cref="Guid"/> в виде строки.
                    /// </summary>
                    public const int GuidStringIdLength = 36;
                }

                #endregion Lengths

                /// <summary>
                /// Таблицы SQL.
                /// </summary>
                public static class Tables
                {
                    #region ExtendedAttributes

                    /// <summary>
                    /// Имена таблиц расширенных атрибутов Identity.
                    /// </summary>
                    public static class ExtendedAttributes
                    {
                        #region Lengths

                        /// <summary>
                        /// Длины строк SQL.
                        /// </summary>
                        // ReSharper disable once MemberHidesStaticFromOuterClass
                        public static class Lengths
                        {
                            /// <summary>
                            /// Длина строки с <see cref="ExtendedAttribute{TEntityId,TEntity}.Key"/>.
                            /// </summary>
                            public const int KeyLength = 50;

                            /// <summary>
                            /// Длина строки с <see cref="ExtendedAttribute{TEntityId,TEntity}.Description"/>.
                            /// </summary>
                            public const int DescriptionLength = 256;

                            /// <summary>
                            /// Длина строки с <see cref="ExtendedAttribute{TEntityId,TEntity}.Group"/>.
                            /// </summary>
                            public const int GroupLength = 50;

                            /// <summary>
                            /// Длина строки с <see cref="ExtendedAttribute{TEntityId,TEntity}.ExternalId"/>.
                            /// </summary>
                            public const int ExternalIdLength = 50;
                        }

                        #endregion Lengths
                    }

                    #endregion ExtendedAttributes

                    #region AuditTrails

                    /// <summary>
                    /// Имя таблицы аудита сущностей.
                    /// </summary>
                    public const string AuditTrailsTableName = nameof(AuditTrails);

                    /// <summary>
                    /// Таблица аудита сущностей.
                    /// </summary>
                    public static class AuditTrails
                    {
                        #region Lengths

                        /// <summary>
                        /// Длины строк SQL.
                        /// </summary>
                        public static class Lengths
                        {
                            /// <summary>
                            /// Длина строки с <see cref="Audit.EntityName"/>.
                            /// </summary>
                            /// <remarks>
                            /// Фактически содержит <see cref="Guid"/> в виде строки.
                            /// </remarks>
                            public const int EntityNameLength = 256;

                            /// <summary>
                            /// Длина строки с <see cref="Audit.Type"/>.
                            /// </summary>
                            public const int TypeLength = 10;
                        }

                        #endregion Lengths
                    }

                    #endregion
                }
            }

            #endregion Common

            #region Application

            /// <summary>
            /// Таблицы со схемой Application.
            /// </summary>
            public static class Application
            {
            }

            #endregion Application
        }
#pragma warning restore SA1201
    }
}