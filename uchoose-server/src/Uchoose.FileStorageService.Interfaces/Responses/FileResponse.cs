// ------------------------------------------------------------------------------------------------------
// <copyright file="FileResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Enums;

namespace Uchoose.FileStorageService.Interfaces.Responses
{
    /// <summary>
    /// Данные файла.
    /// </summary>
    public class FileResponse
    {
        /// <summary>
        /// Имя файла с расширением.
        /// </summary>
        /// <example>FileName.png</example>
        public string Name { get; set; }

        /// <summary>
        /// Данные файла.
        /// </summary>
        /// <example>iVBOR...QmCC</example>
        public byte[] Data { get; set; }

        /// <summary>
        /// Тип файла.
        /// </summary>
        public FileType FileType { get; set; }
    }
}