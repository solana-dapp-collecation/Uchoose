// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasExportableProperties.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Uchoose.Utils.Contracts.Exporting
{
    /// <summary>
    /// Содержит список названий экспортируемых свойств.
    /// </summary>
    public interface IHasExportableProperties
    {
        /// <summary>
        /// Список названий экспортируемых свойств.
        /// </summary>
        /// <remarks>
        /// Если пуст, то берётся список необходимых свойств по умолчанию.
        /// <para>Свойства будут экспортированы в файл в таком порядке, в котором они указаны.</para>
        /// <para>Регистр свойств учитывается (должны начинаться с большой буквы).</para>
        /// </remarks>
        public List<string> Properties { get; }
    }
}