// ------------------------------------------------------------------------------------------------------
// <copyright file="ISearchableRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using Uchoose.Utils.Filters;

namespace Uchoose.Utils.Contracts.Searching
{
    /// <summary>
    /// Запрос для поиска.
    /// </summary>
    public interface ISearchableRequest
    {
        /// <inheritdoc cref="SearchFilter"/>
        public SearchFilter? Search { get; }
    }
}