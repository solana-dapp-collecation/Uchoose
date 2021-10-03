// ------------------------------------------------------------------------------------------------------
// <copyright file="IRoleService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Uchoose.RoleService.Interfaces.Requests;
using Uchoose.RoleService.Interfaces.Responses;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.RoleService.Interfaces
{
    /// <summary>
    /// Сервис для работы с ролями пользователей.
    /// </summary>
    public interface IRoleService :
        IApplicationService
    {
        /// <summary>
        /// Получить роль пользователя по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор роли.</param>
        /// <returns>Возвращает данные роли пользователя.</returns>
        Task<Result<RoleResponse>> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить список всех ролей пользователей.
        /// </summary>
        /// <returns>Возвращает список ролей пользователей.</returns>
        Task<Result<List<RoleResponse>>> GetAllAsync();

        /// <summary>
        /// Получить список ролей пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Возвращает список ролей пользователя.</returns>
        Task<Result<List<RoleResponse>>> GetRolesByUserIdAsync(Guid userId);

        /// <summary>
        /// Получить количество всех ролей пользователей.
        /// </summary>
        /// <returns>Возвращает количество ролей пользователей.</returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// Сохранить данные роли пользователя.
        /// </summary>
        /// <param name="request">Запрос с данными роли пользователя.</param>
        /// <returns>Возвращает идентификатор добавленной/обновлённой роли пользователя.</returns>
        Task<Result<Guid>> SaveAsync(RoleRequest request);

        /// <summary>
        /// Удалить роль пользователя.
        /// </summary>
        /// <param name="id">Идентификатор роли.</param>
        /// <returns>Возвращает идентификатор удалённой роли пользователя.</returns>
        Task<Result<Guid>> DeleteAsync(Guid id);
    }
}