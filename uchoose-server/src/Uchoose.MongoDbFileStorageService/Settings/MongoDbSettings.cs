// ------------------------------------------------------------------------------------------------------
// <copyright file="MongoDbSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.MongoDbFileStorageService.Settings
{
    /// <summary>
    /// Настройки MongoDb.
    /// </summary>
    public class MongoDbSettings :
        ISettings
    {
        /// <summary>
        /// Наименование базы данных.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Строка подключения.
        /// </summary>
        public string ConnectionString { get; set; }
    }
}