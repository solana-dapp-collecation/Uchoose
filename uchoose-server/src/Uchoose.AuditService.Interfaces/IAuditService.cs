// ------------------------------------------------------------------------------------------------------
// <copyright file="IAuditService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Uchoose.AuditService.Interfaces.Requests;
using Uchoose.DataAccess.Interfaces;
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.Domain.Entities;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.AuditService.Interfaces
{
    /// <summary>
    /// Сервис аудита для указанного контекста доступа к данным аудита.
    /// </summary>
    /// <typeparam name="TIDbContext">Тип интерфейса контекста доступа к данным аудита.</typeparam>
    // ReSharper disable once UnusedTypeParameter
    public interface IAuditService<TIDbContext> : IApplicationService
        where TIDbContext : IAuditableDbContext, IDbContextInterface
    {
        /// <summary>
        /// Получить данные аудита по идентификатору пользователя.
        /// </summary>
        /// <param name="request">Запрос для получения данных аудита.</param>
        /// <returns>Возвращает коллекцию с данными аудита.</returns>
        Task<PaginatedResult<Audit>> GetAllAsync(GetAuditTrailsRequest request);

        /// <summary>
        /// Экспортировать данные аудита в excel файл.
        /// </summary>
        /// <param name="request">Запрос для экспорта данных аудита.</param>
        /// <returns>Возвращает экспортируемые в excel файл данные в виде base64 строки.</returns>
        Task<IResult<string>> ExportToExcelAsync(ExportAuditTrailsRequest request);
    }
}