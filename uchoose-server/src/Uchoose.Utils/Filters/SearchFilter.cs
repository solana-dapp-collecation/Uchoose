// ------------------------------------------------------------------------------------------------------
// <copyright file="SearchFilter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System.Collections.Generic;

using Uchoose.Utils.Filters.Contracts;

namespace Uchoose.Utils.Filters
{
    /// <summary>
    /// Фильтр для поиска.
    /// </summary>
    public sealed class SearchFilter :
        ISearchFilter
    {
        /// <summary>
        /// Поля и свойства, в которых осуществляется поиск.
        /// </summary>
        public List<string> Fields { get; set; } = new();

        /// <summary>
        /// Искомое значение.
        /// </summary>
        public string? Keyword { get; set; }
    }
}