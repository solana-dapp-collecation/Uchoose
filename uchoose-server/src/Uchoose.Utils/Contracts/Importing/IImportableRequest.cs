// ------------------------------------------------------------------------------------------------------
// <copyright file="IImportableRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Exporting;

namespace Uchoose.Utils.Contracts.Importing
{
    /// <summary>
    /// Запрос для импорта данных.
    /// </summary>
    public interface IImportableRequest :
        IExportableRequest
    {
        /// <summary>
        /// Номер последнего столбца с заголовками в файле (null - не учитывается).
        /// </summary>
        public int? TitlesLastColNumber { get; }

        /// <summary>
        /// Номер последней строки с импортируемыми данными в файле (null - не учитывается).
        /// </summary>
        public int? DataLastRowNumber { get; }
    }
}