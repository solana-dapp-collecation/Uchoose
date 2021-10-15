// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerTypeUpdatedEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Text.Json.Serialization;

using AutoMapper;
using Uchoose.Domain.Abstractions;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.Domain.Marketplace.Events.NftImageLayerType
{
    /// <summary>
    /// Событие обновления типа слоя изображения NFT.
    /// </summary>
    public class NftImageLayerTypeUpdatedEvent :
        DomainEvent<Entities.NftImageLayerType>,
        IMapFromTo<Entities.NftImageLayerType, NftImageLayerTypeUpdatedEvent>,
        IHasName,
        IHasIsActive,
        IHasIsReadOnly
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerTypeUpdatedEvent"/>.
        /// </summary>
        /// <remarks>
        /// Для <see cref="IMapFromTo{TSource,TDestination}"/>.
        /// </remarks>
        public NftImageLayerTypeUpdatedEvent()
            : base(Guid.Empty, null, null)
        {
            Name = string.Empty;
        }

        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerTypeUpdatedEvent"/>.
        /// </summary>
        /// <param name="nftImageLayerType">Тип слоя изображения NFT.</param>
        /// <param name="eventDescription">Описание события.</param>
        public NftImageLayerTypeUpdatedEvent(Entities.NftImageLayerType nftImageLayerType, string eventDescription)
            : base(
                nftImageLayerType.Id,
                eventDescription,
                nftImageLayerType.Version,
                typeof(Entities.NftImageLayerType))
        {
            Id = nftImageLayerType.Id;
            Name = nftImageLayerType.Name;
            Description = nftImageLayerType.Description;
            IsReadOnly = nftImageLayerType.IsReadOnly;
            IsActive = nftImageLayerType.IsActive;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public Guid Id { get; private set; }

        /// <inheritdoc cref="Entities.NftImageLayerType.Name"/>
        [JsonInclude]
        public string Name { get; private set; }

        /// <inheritdoc cref="Entities.NftImageLayerType.Description"/>
        [JsonInclude]
        public string? Description { get; private set; }

        /// <inheritdoc cref="Entities.NftImageLayerType.IsReadOnly"/>
        [JsonInclude]
        public bool IsReadOnly { get; private set; }

        /// <inheritdoc cref="Entities.NftImageLayerType.IsActive"/>
        [JsonInclude]
        public bool IsActive { get; private set; }

        /// <inheritdoc/>
        void IMapFromTo<Entities.NftImageLayerType, NftImageLayerTypeUpdatedEvent>.Mapping(Profile profile, bool useReverseMap)
        {
            // меняем порядок сопоставления
            profile.CreateMap<NftImageLayerTypeUpdatedEvent, Entities.NftImageLayerType>()
                .ForMember(dest => dest.Version, source => source.MapFrom(c => c.AggregateVersion));
        }
    }
}