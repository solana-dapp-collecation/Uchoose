// ------------------------------------------------------------------------------------------------------
// <copyright file="MiniProfilerSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Api.Common.Settings
{
    /// <summary>
    /// Настройки MiniProfiler.
    /// </summary>
    public class MiniProfilerSettings :
        ISettings
    {
        /// <summary>
        /// Использовать MiniProfiler.
        /// </summary>
        public bool UseMiniProfiler { get; set; }

        /// <summary>
        /// Базовый путь для отображения данных MiniProfiler.
        /// </summary>
        public string RouteBasePath { get; set; }

        /// <summary>
        /// Использовать хранилище для данных MiniProfiler.
        /// </summary>
        public bool UseDatabaseStorage { get; set; }

        /// <summary>
        /// Игнорируемые профилировщиком пути.
        /// </summary>
        public List<string> IgnoredPaths { get; set; } = new();

        /// <summary>
        /// Имя таблицы, используемой для данных MiniProfiler.
        /// </summary>
        public string ProfilersTable { get; set; }

        /// <summary>
        /// Имя таблицы, используемой для таймингов MiniProfiler.
        /// </summary>
        public string TimingsTable { get; set; }

        /// <summary>
        /// Имя таблицы, используемой для клиентских таймингов MiniProfiler.
        /// </summary>
        public string ClientTimingsTable { get; set; }

        /// <summary>
        /// Имя схемы создаваемых таблиц.
        /// </summary>
        public string SchemaName { get; set; }
    }
}