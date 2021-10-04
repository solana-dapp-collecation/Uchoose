// ------------------------------------------------------------------------------------------------------
// <copyright file="MongoDbStorage.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Uchoose.MongoDbFileStorageService.Settings;

namespace Uchoose.MongoDbFileStorageService.Storage
{
    /// <inheritdoc/>
    internal class MongoDbStorage :
        IMongoDbStorage
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="MongoDbStorage"/>.
        /// </summary>
        /// <param name="settings"><see cref="MongoDbSettings"/>.</param>
        public MongoDbStorage(
            IOptionsSnapshot<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = client.GetDatabase(settings.Value.DatabaseName);

            GridFs = new GridFSBucket(mongoDatabase);
        }

        /// <inheritdoc/>
        public IGridFSBucket GridFs { get; }
    }
}