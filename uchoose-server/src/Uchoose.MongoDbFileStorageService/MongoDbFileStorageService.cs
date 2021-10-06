// ------------------------------------------------------------------------------------------------------
// <copyright file="MongoDbFileStorageService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Uchoose.FileStorageService.Interfaces;
using Uchoose.MongoDbFileStorageService.Storage;
using Uchoose.Utils.Contracts.Services;
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
        public async Task<string> UploadAsync<T>(IFormFile file, FileType supportedFileType, CancellationToken cancellationToken = default)
            where T : class
        {
            var imageId = await _mongoDbStorage.GridFs.UploadFromStreamAsync(file.FileName, file.OpenReadStream(), cancellationToken: cancellationToken);

            return imageId.ToString();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string fileId, CancellationToken cancellationToken = default)
        {
            await _mongoDbStorage.GridFs.DeleteAsync(new(fileId), cancellationToken: cancellationToken);
        }
    }
}