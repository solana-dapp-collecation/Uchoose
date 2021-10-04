// ------------------------------------------------------------------------------------------------------
// <copyright file="Permissions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace Uchoose.Application.Constants.Permission
{
    /// <summary>
    /// Разрешения.
    /// </summary>
    public static class Permissions
    {
        /// <summary>
        /// Пользователи.
        /// </summary>
        [DisplayName("Users")]
        [Description("Users Permissions")]
        public static class Users
        {
            /// <summary>
            /// Просмотр.
            /// </summary>
            public const string View = "Permissions.Users.View";

            /// <summary>
            /// Просмотр всех.
            /// </summary>
            public const string ViewAll = "Permissions.Users.ViewAll";

            /// <summary>
            /// Создание.
            /// </summary>
            public const string Create = "Permissions.Users.Create";

            /// <summary>
            /// Редактирование.
            /// </summary>
            public const string Edit = "Permissions.Users.Edit";

            /// <summary>
            /// Удаление.
            /// </summary>
            public const string Delete = "Permissions.Users.Delete";

            /// <summary>
            /// Просмотр данных аудита.
            /// </summary>
            public const string ViewAudit = "Permissions.Users.ViewAudit";
        }

        /// <summary>
        /// Данные аудита.
        /// </summary>
        [DisplayName("Audit")]
        [Description("Audit Permissions")]
        public static class Audit
        {
            /// <summary>
            /// Экспорт данных аудита.
            /// </summary>
            public const string Export = "Permissions.Audit.Export";

            /// <summary>
            /// Поиск в данных аудита.
            /// </summary>
            public const string Search = "Permissions.Audit.Search";

            /// <summary>
            /// Просмотр данных аудита, связанных с Identity.
            /// </summary>
            public const string IdentityViewAll = "Permissions.Audit.Identity.ViewAll";

            /// <summary>
            /// Экспорт данных аудита, связанных с Identity.
            /// </summary>
            public const string IdentityExport = "Permissions.Audit.Identity.Export";
        }

        /// <summary>
        /// Расширенные атрибуты пользователей.
        /// </summary>
        [DisplayName("Users Extended Attributes")]
        [Description("Users Extended Attributes Permissions")]
        public static class UsersExtendedAttributes
        {
            /// <summary>
            /// Просмотр.
            /// </summary>
            public const string View = "Permissions.Users.ExtendedAttributes.View";

            /// <summary>
            /// Просмотр всех.
            /// </summary>
            public const string ViewAll = "Permissions.Users.ExtendedAttributes.ViewAll";

            /// <summary>
            /// Добавление.
            /// </summary>
            public const string Add = "Permissions.Users.ExtendedAttributes.Add";

            /// <summary>
            /// Обновление.
            /// </summary>
            public const string Update = "Permissions.Users.ExtendedAttributes.Update";

            /// <summary>
            /// Удаление.
            /// </summary>
            public const string Remove = "Permissions.Users.ExtendedAttributes.Remove";
        }

        /// <summary>
        /// Роли.
        /// </summary>
        [DisplayName("Roles")]
        [Description("Roles Permissions")]
        public static class Roles
        {
            /// <summary>
            /// Просмотр.
            /// </summary>
            public const string View = "Permissions.Roles.View";

            /// <summary>
            /// Просмотр всех.
            /// </summary>
            public const string ViewAll = "Permissions.Roles.ViewAll";

            /// <summary>
            /// Создание.
            /// </summary>
            public const string Create = "Permissions.Roles.Create";

            /// <summary>
            /// Редактирование.
            /// </summary>
            public const string Edit = "Permissions.Roles.Edit";

            /// <summary>
            /// Удаление.
            /// </summary>
            public const string Delete = "Permissions.Roles.Delete";
        }

        /// <summary>
        /// Расширенные атрибуты ролей пользователей.
        /// </summary>
        [DisplayName("Roles Extended Attributes")]
        [Description("Roles Extended Attributes Permissions")]
        public static class RolesExtendedAttributes
        {
            /// <summary>
            /// Просмотр.
            /// </summary>
            public const string View = "Permissions.Roles.ExtendedAttributes.View";

            /// <summary>
            /// Просмотр всех.
            /// </summary>
            public const string ViewAll = "Permissions.Roles.ExtendedAttributes.ViewAll";

            /// <summary>
            /// Добавление.
            /// </summary>
            public const string Add = "Permissions.Roles.ExtendedAttributes.Add";

            /// <summary>
            /// Обновление.
            /// </summary>
            public const string Update = "Permissions.Roles.ExtendedAttributes.Update";

            /// <summary>
            /// Удаление.
            /// </summary>
            public const string Remove = "Permissions.Roles.ExtendedAttributes.Remove";
        }

        /// <summary>
        /// Разрешения роли пользователя.
        /// </summary>
        [DisplayName("Role Claims")]
        [Description("Role Claims Permissions")]
        public static class RoleClaims
        {
            /// <summary>
            /// Просмотр.
            /// </summary>
            public const string View = "Permissions.RoleClaims.View";

            /// <summary>
            /// Просмотр всех.
            /// </summary>
            public const string ViewAll = "Permissions.RoleClaims.ViewAll";

            /// <summary>
            /// Создание.
            /// </summary>
            public const string Create = "Permissions.RoleClaims.Create";

            /// <summary>
            /// Редактирование.
            /// </summary>
            public const string Edit = "Permissions.RoleClaims.Edit";

            /// <summary>
            /// Удаление.
            /// </summary>
            public const string Delete = "Permissions.RoleClaims.Delete";
        }

        /// <summary>
        /// Разрешения пользователя.
        /// </summary>
        [DisplayName("User Claims")]
        [Description("User Claims Permissions")]
        public static class UserClaims
        {
            /// <summary>
            /// Просмотр.
            /// </summary>
            public const string View = "Permissions.UserClaims.View";

            /// <summary>
            /// Просмотр всех.
            /// </summary>
            public const string ViewAll = "Permissions.UserClaims.ViewAll";

            /// <summary>
            /// Создание.
            /// </summary>
            public const string Create = "Permissions.UserClaims.Create";

            /// <summary>
            /// Редактирование.
            /// </summary>
            public const string Edit = "Permissions.UserClaims.Edit";

            /// <summary>
            /// Удаление.
            /// </summary>
            public const string Delete = "Permissions.UserClaims.Delete";
        }

        /// <summary>
        /// Персональные настройки.
        /// </summary>
        public static class Preferences
        {
            // TODO: добавить разрешения
        }

        /// <summary>
        /// Логи событий.
        /// </summary>
        [DisplayName("Event Logs")]
        [Description("Event Logs Permissions")]
        public static class EventLogs
        {
            /// <summary>
            /// Просмотр.
            /// </summary>
            public const string View = "Permissions.EventLogs.View";

            /// <summary>
            /// Просмотр всех.
            /// </summary>
            public const string ViewAll = "Permissions.EventLogs.ViewAll";

            /// <summary>
            /// Создание.
            /// </summary>
            public const string Create = "Permissions.EventLogs.Create";

            /// <summary>
            /// Экспорт.
            /// </summary>
            public const string Export = "Permissions.EventLogs.Export";
        }

        /// <summary>
        /// Тип слоя изображения NFT.
        /// </summary>
        [DisplayName("NFT Image Layer Types")]
        [Description("NFT Image Layer Types Permissions")]
        public static class NftImageLayerTypes
        {
            /// <summary>
            /// Просмотр.
            /// </summary>
            public const string View = "Permissions.NftImageLayerTypes.View";

            /// <summary>
            /// Просмотр всех.
            /// </summary>
            public const string ViewAll = "Permissions.NftImageLayerTypes.ViewAll";

            /// <summary>
            /// Добавление.
            /// </summary>
            public const string Add = "Permissions.NftImageLayerTypes.Add";

            /// <summary>
            /// Обновление.
            /// </summary>
            public const string Update = "Permissions.NftImageLayerTypes.Update";

            /// <summary>
            /// Удаление.
            /// </summary>
            public const string Remove = "Permissions.NftImageLayerTypes.Remove";

            /// <summary>
            /// Экспорт.
            /// </summary>
            public const string Export = "Permissions.NftImageLayerTypes.Export";

            /// <summary>
            /// Импорт.
            /// </summary>
            public const string Import = "Permissions.NftImageLayerTypes.Import";
        }

        /// <summary>
        /// Basic авторизация к ресурсам сервера.
        /// </summary>
        [DisplayName("Basic Auth")]
        [Description("Basic Auth Permissions")]
        public static class BasicAuth
        {
            /// <summary>
            /// Доступ к swagger UI.
            /// </summary>
            // ReSharper disable once InconsistentNaming
            public const string SwaggerUI = "Permissions.BasicAuth.SwaggerUI";

            /// <summary>
            /// Доступ к ReDoc.
            /// </summary>
            // ReSharper disable once InconsistentNaming
            public const string ReDoc = "Permissions.BasicAuth.ReDoc";

            /// <summary>
            /// Доступ к Health Checks.
            /// </summary>
            public const string HealthChecks = "Permissions.BasicAuth.HealthChecks";

            /// <summary>
            /// Доступ к Hangfire.
            /// </summary>
            public const string Hangfire = "Permissions.BasicAuth.Hangfire";

            /// <summary>
            /// Доступ к MiniProfiler.
            /// </summary>
            public const string MiniProfiler = "Permissions.BasicAuth.MiniProfiler";
        }
    }
}