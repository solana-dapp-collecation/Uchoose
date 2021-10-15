// ------------------------------------------------------------------------------------------------------
// <copyright file="IExcelService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using Uchoose.ExcelService.Interfaces.Requests;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Importing;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.ExcelService.Interfaces
{
    /// <summary>
    /// Сервис для работы с excel файлами.
    /// </summary>
    public interface IExcelService : IApplicationService
    {
        /// <summary>
        /// Экспортировать данные в excel файл.
        /// </summary>
        /// <typeparam name="TEntityId">Тип идентификатора экспортируемой сущности.</typeparam>
        /// <typeparam name="TEntity">Тип экспортируемой сущности.</typeparam>
        /// <param name="request">Запрос на экспорт сущностей в файл.</param>
        /// <returns>Возвращает экспортируемые в excel файл данные в виде base64 строки.</returns>
        Task<IResult<string>> ExportAsync<TEntityId, TEntity>(ExportRequest<TEntityId, TEntity> request)
            where TEntity : IEntity<TEntityId>, IExportable<TEntityId, TEntity>, new();

        /// <summary>
        /// Импортировать данные из excel файла.
        /// </summary>
        /// <typeparam name="TEntityId">Тип идентификатора экспортируемой сущности.</typeparam>
        /// <typeparam name="TEntity">Тип импортируемой сущности.</typeparam>
        /// <param name="request">Запрос на импорт сущностей из файла.</param>
        /// <returns>Возвращает коллекцию импортированных сущностей.</returns>
        Task<IResult<IEnumerable<TEntity>>> ImportAsync<TEntityId, TEntity>(ImportRequest<TEntityId, TEntity> request)
                where TEntity : IEntity<TEntityId>, IImportable<TEntityId, TEntity>, new();
    }
}