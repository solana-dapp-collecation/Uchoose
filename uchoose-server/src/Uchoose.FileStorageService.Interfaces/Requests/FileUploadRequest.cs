﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="FileUploadRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Uploading;

namespace Uchoose.FileStorageService.Interfaces.Requests
{
    /// <inheritdoc cref="IFileUploadRequest"/>
    public class FileUploadRequest :
        IFileUploadRequest
    {
        /// <inheritdoc/>
        /// <example>FileName</example>
        public string Name { get; set; }

        /// <inheritdoc/>
        /// <example>.png</example>
        public string Extension { get; set; }

        /// <inheritdoc/>
        /// <example>iVBOR...QmCC</example>
        public byte[] Data { get; set; }
    }
}