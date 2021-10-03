// ------------------------------------------------------------------------------------------------------
// <copyright file="SwaggerSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Api.Common.Settings
{
    /// <summary>
    /// Настройки swagger.
    /// </summary>
    public class SwaggerSettings :
        ISettings
    {
        /// <summary>
        /// Включить в swagger xml документацию, включая remarks.
        /// </summary>
        public bool IncludeXmlCommentsWithRemarks { get; set; }

        /// <summary>
        /// Включить в swagger xml документацию из inheritdocs.
        /// </summary>
        public bool IncludeXmlCommentsFromInheritDocs { get; set; }

        /// <summary>
        /// Включить в swagger человекочитаемую xml документацию для перечислений.
        /// </summary>
        public bool AddEnumsWithValuesFixFilters { get; set; }

        /// <summary>
        /// Включить кэширование swagger документации.
        /// </summary>
        public bool UseCaching { get; set; }

        /// <summary>
        /// Использовать тёмную тему для swagger UI.
        /// </summary>
        public bool UseDarkTheme { get; set; }

        /// <summary>
        /// Использовать swagger UI.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public bool UseSwaggerUI { get; set; } = true;

        /// <summary>
        /// Префикс для роутинга swagger UI.
        /// </summary>
        public string SwaggerUiRoutePrefix { get; set; } = "swagger";

        /// <summary>
        /// Заголовок для swagger UI.
        /// </summary>
        public string SwaggerUiDocumentTitle { get; set; }

        /// <summary>
        /// Использовать ReDoc.
        /// </summary>
        public bool UseReDoc { get; set; }

        /// <summary>
        /// Префикс для роутинга ReDoc.
        /// </summary>
        public string ReDocRoutePrefix { get; set; } = "api-docs";

        /// <summary>
        /// Заголовок для ReDoc.
        /// </summary>
        public string ReDocDocumentTitle { get; set; }

        /// <summary>
        /// Список данных документов swagger.
        /// </summary>
        public List<SwaggerDoc> SwaggerDocs { get; set; }

        /// <summary>
        /// Данные документа swagger.
        /// </summary>
        public class SwaggerDoc
        {
            /// <summary>
            /// Документ используется.
            /// </summary>
            public bool Enabled { get; set; }

            /// <summary>
            /// Относительный Url к swagger спецификации.
            /// </summary>
            public string Url { get; set; }

            /// <summary>
            /// Название для swagger спецификации.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Версия swagger документа (обязательный).
            /// </summary>
            public string Version { get; set; }

            /// <summary>
            /// Заголовок приложения (обязательный).
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// Краткое описание для приложения.
            /// </summary>
            public string Description { get; set; }
        }
    }
}