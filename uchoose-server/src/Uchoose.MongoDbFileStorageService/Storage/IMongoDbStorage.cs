// ------------------------------------------------------------------------------------------------------
// <copyright file="IMongoDbStorage.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using MongoDB.Driver.GridFS;

namespace Uchoose.MongoDbFileStorageService.Storage
{
    /// <summary>
    /// Хранилище данных MongoDb.
    /// </summary>
    internal interface IMongoDbStorage
    {
        /// <summary>
        /// <see cref="IGridFSBucket"/>.
        /// </summary>
        IGridFSBucket GridFs { get; }
    }
}