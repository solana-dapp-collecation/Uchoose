// ------------------------------------------------------------------------------------------------------
// <copyright file="PaginationFilter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;

#nullable enable

namespace Uchoose.Utils.Filters
{
    /// <summary>
    /// Фильтр с пагинацией.
    /// </summary>
    public abstract class PaginationFilter :
        BaseFilter,
        IPaginated
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="PaginationFilter"/>.
        /// </summary>
        protected PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        /// <summary>
        /// Инициализирует экземпляр <see cref="PaginationFilter"/>.
        /// </summary>
        /// <param name="pageNumber">Номер страницы.</param>
        /// <param name="pageSize">Размер страницы.</param>
        protected PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
        }

        /// <summary>
        /// Номер страницы.
        /// </summary>
        /// <example>1</example>
        public int PageNumber { get; set; }

        /// <summary>
        /// Размер страницы.
        /// </summary>
        /// <example>10</example>
        public int PageSize { get; set; }

        /// <summary>
        /// Сортировать по.
        /// </summary>
        /// <example>id desc</example>
        public string? OrderBy { get; set; }
    }
}