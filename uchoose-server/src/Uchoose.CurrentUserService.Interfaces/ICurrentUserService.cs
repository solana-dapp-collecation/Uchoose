// ------------------------------------------------------------------------------------------------------
// <copyright file="ICurrentUserService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;
using Uchoose.Utils.Contracts.Services;

namespace Uchoose.CurrentUserService.Interfaces
{
    /// <summary>
    /// Сервис для работы с текущим пользователем.
    /// </summary>
    public interface ICurrentUserService :
        IInfrastructureService
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Получить идентификатор пользователя.
        /// </summary>
        /// <returns>Возвращает идентификатор пользователя.</returns>
        Guid GetUserId();

        /// <summary>
        /// Получить email пользователя.
        /// </summary>
        /// <returns>Возвращает email пользователя.</returns>
        string GetUserEmail();

        /// <summary>
        /// Проверить, аутентифицирован ли пользователь.
        /// </summary>
        /// <returns>Возвращает <c>true</c>, если пользователь аутентифицирован. Иначе - <c>false</c>.</returns>
        bool IsAuthenticated();

        /// <summary>
        /// Проверить, имеет ли пользователь указанную роль.
        /// </summary>
        /// <param name="role">Роль для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если пользователь имеет указанную роль. Иначе - <c>false</c>.</returns>
        bool IsInRole(string role);

        /// <summary>
        /// Получить коллекцию разрешений пользователя.
        /// </summary>
        /// <returns>Возвращает коллекцию разрешений пользователя.</returns>
        IEnumerable<Claim> GetUserClaims();

        /// <summary>
        /// Получить <see cref="HttpContext"/>.
        /// </summary>
        /// <returns>Возвращает <see cref="HttpContext"/>.</returns>
        HttpContext GetHttpContext();

        /// <summary>
        /// Получить дополнительные свойства для логирования.
        /// </summary>
        /// <returns>Возвращает дополнительные свойства для логирования.</returns>
        Dictionary<string, string> GetAdditionalPropertiesForLogging();
    }
}