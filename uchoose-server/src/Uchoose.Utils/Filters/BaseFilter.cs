// ------------------------------------------------------------------------------------------------------
// <copyright file="BaseFilter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Searching;

namespace Uchoose.Utils.Filters
{
    /// <summary>
    /// Базовый фильтр.
    /// </summary>
    public abstract class BaseFilter :
        ISearchableRequest
    {
        /// <inheritdoc cref="SearchFilter"/>
        public SearchFilter Search { get; set; }
    }
}