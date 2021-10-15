// ------------------------------------------------------------------------------------------------------
// <copyright file="IUserClaimService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Uchoose.UserClaimService.Interfaces.Requests;
using Uchoose.UserClaimService.Interfaces.Responses;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UserClaimService.Interfaces
{
    /// <summary>
    /// Сервис для работы с разрешениями пользователей.
    /// </summary>
    public interface IUserClaimService :
        IApplicationService
    {
        /// <summary>
        /// Получить разрешение пользователя по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор разрешения пользователя.</param>
        /// <returns>Возвращает данные разрешения пользователя.</returns>
        Task<Result<UserClaimResponse>> GetByIdAsync(int id);

        /// <summary>
        /// Получить список всех разрешений пользователей из БД.
        /// </summary>
        /// <returns>Возвращает список с данными разрешений пользователей.</returns>
        Task<Result<List<UserClaimResponse>>> GetAllAsync();

        /// <summary>
        /// Получить список всех разрешений указанного пользователя из БД.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Возвращает список с данными разрешений пользователя.</returns>
        Task<Result<List<UserClaimResponse>>> GetAllByUserIdAsync(Guid userId);

        /// <summary>
        /// Получить количество всех разрешений пользователей.
        /// </summary>
        /// <returns>Возвращает количество разрешений пользователей.</returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// Сохранить данные разрешения пользователя.
        /// </summary>
        /// <param name="request">Запрос с данными разрешения пользователя.</param>
        /// <returns>Возвращает идентификатор разрешения пользователя.</returns>
        Task<Result<int>> SaveAsync(UserClaimRequest request);

        /// <summary>
        /// Удалить разрешение пользователя.
        /// </summary>
        /// <param name="id">Идентификатор разрешения пользователя.</param>
        /// <returns>Возвращает идентификатор удалённого разрешения роли.</returns>
        Task<Result<int>> DeleteAsync(int id);

        /// <summary>
        /// Получить список разрешений указанного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Возвращает список разрешений указанного пользователя.</returns>
        Task<Result<UserPermissionsResponse>> GetAllPermissionsAsync(Guid userId);

        /// <summary>
        /// Обновить разрешения указанного пользователя.
        /// </summary>
        /// <param name="request">Запрос с данными разрешений пользователя.</param>
        /// <returns>Возвращает результат выполнения операций.</returns>
        Task<IResult> UpdatePermissionsAsync(UserPermissionRequest request);
    }
}