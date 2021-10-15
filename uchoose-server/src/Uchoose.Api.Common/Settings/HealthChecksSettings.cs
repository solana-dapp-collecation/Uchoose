// ------------------------------------------------------------------------------------------------------
// <copyright file="HealthChecksSettings.cs" company="Life Loop">
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
    /// Настройки Health Checks.
    /// </summary>
    public class HealthChecksSettings :
        ISettings
    {
        /// <summary>
        /// Использовать вывод через HealthChecks-UI.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public bool UseHealthChecksUI { get; set; }

        /// <summary>
        /// Список email, на которые будут отправляться отчёты в случае недоступности по Health Check.
        /// </summary>
        public List<string> ReportingEmails { get; set; } = new();

        /// <summary>
        /// Список конечных точек для Health Checks.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public List<HealthCheckEndpoint> HealthCheckEndpoints { get; set; } = new();

        /// <summary>
        /// Интервал опроса Health Checks со стороны UI в секундах.
        /// </summary>
        public int EvaluationTimeInSeconds { get; set; }

        /// <summary>
        /// Максимальное количество записей в истории по конечной точке.
        /// </summary>
        public int MaximumHistoryEntriesPerEndpoint { get; set; }

        /// <summary>
        /// Минимальное количество секунд между уведомлениями об ошибках.
        /// </summary>
        public int MinimumSecondsBetweenFailureNotifications { get; set; }

        /// <summary>
        /// Конечная точка для Health Checks.
        /// </summary>
        public sealed class HealthCheckEndpoint
        {
            /// <summary>
            /// Наименование конечной точки.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Uri конечной точки.
            /// </summary>
            public string Uri { get; set; }

            /// <summary>
            /// Uri конечной точки для UI.
            /// </summary>
            // ReSharper disable once InconsistentNaming
            public string UIUri { get; set; }

            /// <summary>
            /// Список тегов, относящихся к конечной точке.
            /// </summary>
            public List<string> Tags { get; set; } = new();
        }
    }
}