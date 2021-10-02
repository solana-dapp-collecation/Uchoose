// ------------------------------------------------------------------------------------------------------
// <copyright file="IPaginated.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Utils.Contracts.Common
{
    /// <summary>
    /// Элемент с пагинацией.
    /// </summary>
    public interface IPaginated
    {
        /// <summary>
        /// Текущая страница.
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// Количество единиц данных на страницу.
        /// </summary>
        public int PageSize { get; }
    }
}