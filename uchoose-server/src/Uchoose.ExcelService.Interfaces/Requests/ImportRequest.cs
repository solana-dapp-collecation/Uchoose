// ------------------------------------------------------------------------------------------------------
// <copyright file="ImportRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Importing;

namespace Uchoose.ExcelService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на импорт сущностей из файла.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора экспортируемой сущности.</typeparam>
    /// <typeparam name="TEntity">Тип импортируемой сущности.</typeparam>
    public class ImportRequest<TEntityId, TEntity> :
        IImportableRequest
            where TEntity : IEntity<TEntityId>, IImportable<TEntityId, TEntity>, new()
    {
        /// <summary>
        /// Импортируемые данные в виде <see cref="Stream"/>.
        /// </summary>
        public Stream DataStream { get; set; }

        /// <summary>
        /// Словарь для сопоставления импортируемых из excel данных с данными сущности.
        /// </summary>
        public Dictionary<string, Func<DataRow, TEntity, (object Object, int Order)>> Mappers { get; set; }

        /// <inheritdoc/>
        public int TitlesRowNumber { get; set; } = 1;

        /// <inheritdoc/>
        public int TitlesFirstColNumber { get; set; } = 1;

        /// <inheritdoc/>
        public int? TitlesLastColNumber { get; set; }

        /// <inheritdoc/>
        public int DataFirstRowNumber { get; set; } = 2;

        /// <inheritdoc/>
        public int? DataLastRowNumber { get; set; }

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