// ------------------------------------------------------------------------------------------------------
// <copyright file="ExportDefaultOrderAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.Utils.Attributes.Ordering
{
    /// <summary>
    /// Атрибут, указывающий относительный порядок по умолчанию для свойства при экспорте в файл.
    /// </summary>
    /// <remarks>
    /// Свойства с меньшим значением относительного порядка будут добавлены в файл раньше.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ExportDefaultOrderAttribute : Attribute
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="ExportDefaultOrderAttribute"/>.
        /// </summary>
        /// <param name="value">Относительный порядок при импорте/экспорте.</param>
        public ExportDefaultOrderAttribute(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Относительный порядок при импорте/экспорте.
        /// </summary>
        public int Value { get; }
    }
}