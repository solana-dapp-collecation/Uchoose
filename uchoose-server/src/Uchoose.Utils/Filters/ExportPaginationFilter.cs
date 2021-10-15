// ------------------------------------------------------------------------------------------------------
// <copyright file="ExportPaginationFilter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Exporting;

namespace Uchoose.Utils.Filters
{
    /// <summary>
    /// Фильтр для экспорта в с пагинацией.
    /// </summary>
    public abstract class ExportPaginationFilter<TEntityId, TEntity> :
        PaginationFilter,
        IExportableRequest,
        IHasExportableProperties
        where TEntity : class, IEntity<TEntityId>, IExportable<TEntityId, TEntity>, new()
    {
        /// <inheritdoc/>
        /// <example>1</example>
        public int TitlesRowNumber { get; set; } = 1;

        /// <inheritdoc/>
        /// <example>1</example>
        public int TitlesFirstColNumber { get; set; } = 1;

        /// <inheritdoc/>
        /// <example>2</example>
        public int DataFirstRowNumber { get; set; } = 2;

        /// <inheritdoc/>
        /// <example>null</example>
        public List<string> Properties { get; set; } = new();

        /// <summary>
        /// Вернуть результат экспорта данных в виде файла.
        /// </summary>
        /// <example>false</example>
        public bool ReturnAsFileStream { get; set; } = false;
    }
}