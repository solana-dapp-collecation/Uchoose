// ------------------------------------------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Uchoose.UserService.Interfaces.Requests;
using Uchoose.UserService.Interfaces.Responses;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UserService.Interfaces
{
    /// <summary>
    /// Сервис для работы с пользователями.
    /// </summary>
    public interface IUserService :
        IApplicationService
    {
        /// <summary>
        /// Получить список всех пользователей.
        /// </summary>
        /// <returns>Возвращает список данных о пользователях.</returns>
        Task<Result<List<UserResponse>>> GetAllAsync();

        /// <summary>
        /// Получить количество всех пользователей.
        /// </summary>
        /// <returns>Возвращает количество всех пользователей.</returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// Получить данные указанного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Возвращает данные о пользователе.</returns>
        Task<IResult<UserResponse>> GetAsync(Guid userId);

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Возвращает идентификатор удалённого пользователя.</returns>
        Task<IResult<Guid>> DeleteAsync(Guid userId);

        /// <summary>
        /// Получить список ролей указанного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Возвращает список ролей пользователя.</returns>
        Task<IResult<UserRolesResponse>> GetRolesAsync(Guid userId);

        /// <summary>
        /// Обновить роли пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="request">Запрос со списком ролей пользователя.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        Task<IResult<Guid>> UpdateUserRolesAsync(Guid userId, UserRolesRequest request);

        /// <summary>
        /// Поменять статус пользователя.
        /// </summary>
        /// <param name="request">Запрос на изменение статуса пользователя.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        Task<IResult<Guid>> ToggleUserStatusAsync(ToggleUserStatusRequest request);
    }
}