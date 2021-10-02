// ------------------------------------------------------------------------------------------------------
// <copyright file="FileType.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace Uchoose.Utils.Enums
{
    /// <summary>
    /// Тип загружаемого файла.
    /// </summary>
    public enum FileType : byte
    {
        /// <summary>
        /// Изображение.
        /// </summary>
        [Description(".jpg,.png,.jpeg")]
        Image = 0,

        /// <summary>
        /// Шаблон.
        /// </summary>
        [Description(".html,.cshtml")]
        Template = 1
    }
}