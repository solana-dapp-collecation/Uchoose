// ------------------------------------------------------------------------------------------------------
// <copyright file="ImportEntitiesCommand.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Uchoose.Utils.Attributes.Logging;
using Uchoose.Utils.Contracts.Importing;

namespace Uchoose.UseCases.Common.Features.Common.Commands
{
    /// <summary>
    /// Команда для импорта сущностей из файла.
    /// </summary>
    public abstract class ImportEntitiesCommand :
        IImportableRequest
    {
        /// <inheritdoc/>
        /// <example>1</example>
        public int TitlesRowNumber { get; set; } = 1;

        /// <inheritdoc/>
        /// <example>1</example>
        public int TitlesFirstColNumber { get; set; } = 1;

        /// <inheritdoc/>
        /// <example>null</example>
        public int? TitlesLastColNumber { get; set; } = null;

        /// <inheritdoc/>
        /// <example>2</example>
        public int DataFirstRowNumber { get; set; } = 2;

        /// <inheritdoc/>
        /// <example>null</example>
        public int? DataLastRowNumber { get; set; } = null;

        /// <summary>
        /// Данные из файла.
        /// </summary>
        /// <example>0</example>
        [LogMasked(ShowFirst = 10)]
        public byte[] Data { get; set; }

        /// <summary>
        /// Список названий импортируемых свойств.
        /// </summary>
        /// <remarks>
        /// Если пуст, то берётся список необходимых свойств по умолчанию.
        /// </remarks>
        /// <example>null</example>
        public List<string> Properties { get; set; } = new();
    }
}