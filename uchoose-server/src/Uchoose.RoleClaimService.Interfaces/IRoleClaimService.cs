// ------------------------------------------------------------------------------------------------------
// <copyright file="IRoleClaimService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Uchoose.RoleClaimService.Interfaces.Requests;
using Uchoose.RoleClaimService.Interfaces.Responses;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.RoleClaimService.Interfaces
{
    /// <summary>
    /// Сервис для работы с разрешениями ролей пользователей.
    /// </summary>
    public interface IRoleClaimService :
        IApplicationService
    {
        /// <summary>
        /// Получить разрешение роли по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор разрешения роли.</param>
        /// <returns>Возвращает данные разрешения роли пользователя.</returns>
        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        /// <summary>
        /// Получить список всех разрешений ролей из БД.
        /// </summary>
        /// <returns>Возвращает список с данными разрешений ролей.</returns>
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        /// <summary>
        /// Получить список всех разрешений указанной роли из БД.
        /// </summary>
        /// <param name="roleId">Идентификатор роли.</param>
        /// <returns>Возвращает список с данными разрешений роли.</returns>
        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(Guid roleId);

        /// <summary>
        /// Получить количество всех разрешений ролей.
        /// </summary>
        /// <returns>Возвращает количество разрешений ролей.</returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// Сохранить данные разрешения роли.
        /// </summary>
        /// <param name="request">Запрос с данными разрешения роли пользователя.</param>
        /// <returns>Возвращает идентификатор разрешения роли.</returns>
        Task<Result<int>> SaveAsync(RoleClaimRequest request);

        /// <summary>
        /// Удалить разрешение роли.
        /// </summary>
        /// <param name="id">Идентификатор разрешения роли.</param>
        /// <returns>Возвращает идентификатор удалённого разрешения роли.</returns>
        Task<Result<int>> DeleteAsync(int id);

        /// <summary>
        /// Получить список разрешений указанной роли.
        /// </summary>
        /// <param name="roleId">Идентификатор роли.</param>
        /// <returns>Возвращает список разрешений указанной роли.</returns>
        Task<Result<RolePermissionsResponse>> GetAllPermissionsAsync(Guid roleId);

        /// <summary>
        /// Обновить разрешения указанной роли.
        /// </summary>
        /// <param name="request">Запрос с данными разрешений роли пользователя.</param>
        /// <returns>Возвращает результат выполнения операций.</returns>
        Task<IResult> UpdatePermissionsAsync(RolePermissionRequest request);
    }
}