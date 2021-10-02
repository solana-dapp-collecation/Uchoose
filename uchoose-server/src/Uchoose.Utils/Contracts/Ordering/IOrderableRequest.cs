// ------------------------------------------------------------------------------------------------------
// <copyright file="IOrderableRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable

namespace Uchoose.Utils.Contracts.Ordering
{
    /// <summary>
    /// Запрос с сортировкой.
    /// </summary>
    public interface IOrderableRequest
    {
        /// <summary>
        /// Массив строк с полями для сортировки.
        /// </summary>
        public string[]? OrderBy { get; }
    }
}