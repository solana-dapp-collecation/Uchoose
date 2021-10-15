// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;

using Uchoose.Utils.Contracts.Logging;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries.Responses
{
    /// <summary>
    /// Ответ на запрос на получение слоя изображения NFT.
    /// </summary>
    /// <param name="Id">Идентификатор слоя изображения NFT.</param>
    /// <param name="Name">Наименование слоя изображения NFT.</param>
    /// <param name="TypeId">Идентификатор типа слоя изображения.</param>
    /// <param name="NftImageLayerUri">Путь к слою изображения NFT.</param>
    /// <param name="ArtistDid">DID художника, который создал слой изображения NFT.</param>
    /// <param name="IsReadOnly">Только для чтения.</param>
    /// <param name="IsActive">Слой изображения NFT активен.</param>
    public record NftImageLayerResponse(
            Guid Id,
            string Name,
            Guid TypeId,
            string NftImageLayerUri,
            string ArtistDid,
            bool IsReadOnly,
            bool IsActive)
        : IMapFromTo<Domain.Marketplace.Entities.NftImageLayer, NftImageLayerResponse>,
            ILoggable,
            IHasName,
            IHasIsActive,
            IHasIsReadOnly
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerResponse"/>.
        /// </summary>
        /// <remarks>
        /// Конструктор без параметров нужен, чтобы можно было через рефлексию добавить
        /// сопоставление с помощью интерфейса <see cref="IMapFromTo{TSource,TDestination}"/>.
        /// </remarks>
        public NftImageLayerResponse()
            : this(default, string.Empty, default, string.Empty, string.Empty, default, default)
        {
        }
    }
}