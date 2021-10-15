// ------------------------------------------------------------------------------------------------------
// <copyright file="PaginatedResult.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Uchoose.Utils.Attributes.Logging;
using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Utils.Wrapper
{
    /// <summary>
    /// Результат операции с пагинацией возвращаемых данных.
    /// </summary>
    /// <typeparam name="T">Тип возвращаемых данных.</typeparam>
    public record PaginatedResult<T> : Result, IPaginated
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="PaginatedResult{T}"/>.
        /// </summary>
        /// <param name="data">Список возвращаемых данных.</param>
        public PaginatedResult(List<T> data)
        {
            Data = data;
        }

        /// <summary>
        /// Инициализирует экземпляр <see cref="PaginatedResult{T}"/>.
        /// </summary>
        /// <param name="succeeded">Операция успешна.</param>
        /// <param name="data">Список возвращаемых данных.</param>
        /// <param name="messages">Список сообщений.</param>
        /// <param name="count">Всего данных.</param>
        /// <param name="page">Текущая страница.</param>
        /// <param name="pageSize">Количество единиц данных на страницу.</param>
        internal PaginatedResult(
            bool succeeded,
            List<T> data = default,
            List<string> messages = null,
            int count = 0,
            int page = 1,
            int pageSize = 10)
        {
            Data = data;
            Messages = messages;
            PageNumber = page;
            Succeeded = succeeded;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        /// <summary>
        /// Список возвращаемых данных.
        /// </summary>
        public List<T> Data { get; }

        /// <inheritdoc cref="IPaginated.PageNumber"/>
        public int PageNumber { get; }

        /// <summary>
        /// Всего страниц.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Всего данных.
        /// </summary>
        public int TotalCount { get; }

        /// <inheritdoc cref="IPaginated.PageSize"/>
        public int PageSize { get; }

        /// <summary>
        /// Имеет предыдущую страницу.
        /// </summary>
        [NotLogged]
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Имеет следующую страницу.
        /// </summary>
        [NotLogged]
        public bool HasNextPage => PageNumber < TotalPages;

        /// <summary>
        /// Неуспешный результат операции с пагинацией возвращаемых данных.
        /// </summary>
        /// <param name="messages">Список сообщений (ошибок).</param>
        /// <returns>Возвращает <see cref="PaginatedResult{T}"/>.</returns>
        public static PaginatedResult<T> Failure(List<string> messages)
        {
            return new(false, default, messages);
        }

        /// <summary>
        /// Успешный результат операции с пагинацией возвращаемых данных.
        /// </summary>
        /// <param name="data">Список возвращаемых данных.</param>
        /// <param name="count">Всего данных.</param>
        /// <param name="page">Текущая страница.</param>
        /// <param name="pageSize">Количество единиц данных на страницу.</param>
        /// <returns>Возвращает <see cref="PaginatedResult{T}"/>.</returns>
        public static PaginatedResult<T> Success(List<T> data, int count, int page, int pageSize)
        {
            return new(true, data, null, count, page, pageSize);
        }
    }
}