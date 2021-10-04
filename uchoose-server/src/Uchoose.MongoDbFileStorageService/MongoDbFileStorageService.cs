// ------------------------------------------------------------------------------------------------------
// <copyright file="MongoDbFileStorageService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Uchoose.FileStorageService.Interfaces;
using Uchoose.MongoDbFileStorageService.Storage;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Contracts.Uploading;
using Uchoose.Utils.Enums;

namespace Uchoose.MongoDbFileStorageService
{
    /// <summary>
    /// Сервис для доступа к хранилищу файлов в MongoDb.
    /// </summary>
    internal sealed class MongoDbFileStorageService :
        IFileStorageService,
        ITransientService
    {
        private readonly IMongoDbStorage _mongoDbStorage;

        /// <summary>
        /// Инициализирует экземпляр <see cref="MongoDbFileStorageService"/>.
        /// </summary>
        /// <param name="mongoDbStorage"><see cref="IMongoDbStorage"/>.</param>
        public MongoDbFileStorageService(
            IMongoDbStorage mongoDbStorage)
        {
            _mongoDbStorage = mongoDbStorage ?? throw new ArgumentNullException(nameof(mongoDbStorage));
        }

        /// <inheritdoc/>
        public async Task<byte[]> DownloadAsync(string fileId, CancellationToken cancellationToken = default)
        {
            return await _mongoDbStorage.GridFs.DownloadAsBytesAsync(new(fileId), cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<string> UploadAsync<T>(IFileUploadRequest request, FileType supportedFileType, CancellationToken cancellationToken = default)
            where T : class
        {
            await using var stream = new MemoryStream(request.Data);
            var imageId = await _mongoDbStorage.GridFs.UploadFromStreamAsync(request.Name, stream, cancellationToken: cancellationToken);

            return imageId.ToString();
        }
    }
}