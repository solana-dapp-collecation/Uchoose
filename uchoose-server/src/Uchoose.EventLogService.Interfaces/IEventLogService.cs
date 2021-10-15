// ------------------------------------------------------------------------------------------------------
// <copyright file="IEventLogService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Uchoose.EventLogService.Interfaces.Requests;
using Uchoose.EventLogService.Interfaces.Responses;
using Uchoose.Utils.Contracts.Events;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.EventLogService.Interfaces
{
    /// <summary>
    /// Сервис для работы с логами событий.
    /// </summary>
    public interface IEventLogService :
        IApplicationService
    {
        /// <summary>
        /// Получить данные лога событий по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор лога события.</param>
        /// <returns>Возвращает данные лога событий.</returns>
        Task<Result<EventLogResponse>> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить данные события по идентификатору лога этого события.
        /// </summary>
        /// <typeparam name="TEvent">Тип события.</typeparam>
        /// <param name="id">Идентификатор лога события.</param>
        /// <returns>Возвращает данные события по идентификатору лога этого события.</returns>
        Task<Result<TEvent>> GetEventByLogIdAsync<TEvent>(Guid id)
            where TEvent : class, IEvent, new();

        /// <summary>
        /// Получить список всех данных логов событий.
        /// </summary>
        /// <param name="request">Запрос для получения логов событий с пагинацией.</param>
        /// <returns>Возвращает список данных логов событий.</returns>
        Task<PaginatedResult<EventLogResponse>> GetAllAsync(GetEventLogsRequest request);

        /// <summary>
        /// Добавить пользовательское событие в логи событий.
        /// </summary>
        /// <param name="request">Запрос на добавление пользовательского события в логи событий.</param>
        /// <returns>Возвращает идентификатор добавленного события.</returns>
        Task<Result<Guid>> LogCustomEventAsync(LogEventRequest request);

        /// <summary>
        /// Экспортировать логи событий в excel файл.
        /// </summary>
        /// <param name="request">Запрос для экспорта логов событий.</param>
        /// <returns>Возвращает экспортируемые в excel файл данные в виде base64 строки.</returns>
        Task<IResult<string>> ExportToExcelAsync(ExportEventLogsRequest request);
    }
}