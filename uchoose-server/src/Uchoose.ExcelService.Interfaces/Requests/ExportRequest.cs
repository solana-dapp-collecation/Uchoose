// ------------------------------------------------------------------------------------------------------
// <copyright file="ExportRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Exporting;

namespace Uchoose.ExcelService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на экспорт сущностей в файл.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора экспортируемой сущности.</typeparam>
    /// <typeparam name="TEntity">Тип экспортируемой сущности.</typeparam>
    public class ExportRequest<TEntityId, TEntity> :
        IExportableRequest
            where TEntity : IEntity<TEntityId>, IExportable<TEntityId, TEntity>, new()
    {
        /// <summary>
        /// Экспортируемые данные.
        /// </summary>
        public IEnumerable<TEntity> Data { get; set; }

        /// <summary>
        /// Словарь для сопоставления экспортируемых данных с данными в excel.
        /// </summary>
        public Dictionary<string, Func<TEntity, (object Object, int Order)>> Mappers { get; set; }

        /// <inheritdoc/>
        public int TitlesRowNumber { get; set; } = 1;

        /// <inheritdoc/>
        public int TitlesFirstColNumber { get; set; } = 1;

        /// <inheritdoc/>
        public int DataFirstRowNumber { get; set; } = 2;

        /// <summary>
        /// Название листа.
        /// </summary>
        public string SheetName { get; set; } = "Sheet1";

        /// <summary>
        /// Проверить, что словарь для сопоставления содержит только имена свойств указанного типа.
        /// </summary>
        public bool CheckProperties { get; set; }
    }
}