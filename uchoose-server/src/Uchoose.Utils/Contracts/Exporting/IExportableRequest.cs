// ------------------------------------------------------------------------------------------------------
// <copyright file="IExportableRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Utils.Contracts.Exporting
{
    /// <summary>
    /// Запрос для экспорта данных.
    /// </summary>
    public interface IExportableRequest
    {
        /// <summary>
        /// Номер строки с заголовками в файле.
        /// </summary>
        public int TitlesRowNumber { get; }

        /// <summary>
        /// Номер первого столбца с заголовками в файле.
        /// </summary>
        public int TitlesFirstColNumber { get; }

        /// <summary>
        /// Номер первой строки с данными в файле.
        /// </summary>
        public int DataFirstRowNumber { get; }
    }
}