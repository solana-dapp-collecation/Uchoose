// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerTypeResponse.cs" company="Life Loop">
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

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries.Responses
{
    /// <summary>
    /// Ответ на запрос на получение типа слоя изображения NFT.
    /// </summary>
    /// <param name="Id">Идентификатор типа слоя изображения NFT.</param>
    /// <param name="Name">Наименование типа слоя изображения NFT.</param>
    /// <param name="Description">Описание типа слоя изображения NFT.</param>
    /// <param name="IsReadOnly">Только для чтения.</param>
    /// <param name="IsActive">Тип слоя изображения NFT активен.</param>
    public record NftImageLayerTypeResponse(
            Guid Id,
            string Name,
            string? Description,
            bool IsReadOnly,
            bool IsActive)
        : IMapFromTo<Domain.Marketplace.Entities.NftImageLayerType, NftImageLayerTypeResponse>,
            ILoggable,
            IHasName,
            IHasIsActive,
            IHasIsReadOnly
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerTypeResponse"/>.
        /// </summary>
        /// <remarks>
        /// Конструктор без параметров нужен, чтобы можно было через рефлексию добавить
        /// сопоставление с помощью интерфейса <see cref="IMapFromTo{TSource,TDestination}"/>.
        /// </remarks>
        public NftImageLayerTypeResponse()
            : this(default, string.Empty, default, default, default)
        {
        }
    }
}