// ------------------------------------------------------------------------------------------------------
// <copyright file="ISearchFilter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System.Collections.Generic;

namespace Uchoose.Utils.Filters.Contracts
{
    /// <summary>
    /// Фильтр для поиска по сущности.
    /// </summary>
    public interface ISearchFilter
    {
        /// <summary>
        /// Поля и свойства, в которых осуществляется поиск.
        /// </summary>
        public List<string> Fields { get; }

        /// <summary>
        /// Искомое значение.
        /// </summary>
        public string? Keyword { get; }
    }
}