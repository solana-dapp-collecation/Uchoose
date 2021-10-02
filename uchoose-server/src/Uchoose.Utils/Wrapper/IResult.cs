// ------------------------------------------------------------------------------------------------------
// <copyright file="IResult.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Uchoose.Utils.Wrapper
{
    /// <summary>
    /// Результат операции.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Список сообщений.
        /// </summary>
        List<string> Messages { get; set; }

        /// <summary>
        /// Операция успешна.
        /// </summary>
        bool Succeeded { get; init; }
    }

    /// <summary>
    /// Результат операции с возвращаемыми данными.
    /// </summary>
    /// <typeparam name="T">Тип возвращаемых данных.</typeparam>
    public interface IResult<T> : IResult
    {
        /// <summary>
        /// Возвращаемые данные.
        /// </summary>
        T Data { get; init; }
    }
}