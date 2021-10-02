// ------------------------------------------------------------------------------------------------------
// <copyright file="IFileUploadRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Utils.Contracts.Uploading
{
    /// <summary>
    /// Запрос на загрузку файла.
    /// </summary>
    public interface IFileUploadRequest
    {
        /// <summary>
        /// Имя файла.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Расширение файла (с точкой в начале).
        /// </summary>
        public string Extension { get; }

        /// <summary>
        /// Данные файла.
        /// </summary>
        public byte[] Data { get; }
    }
}