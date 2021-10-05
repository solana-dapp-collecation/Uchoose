// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerAddedEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
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

namespace Uchoose.Domain.Marketplace.Events.NftImageLayer
{
    /// <summary>
    /// Событие добавления типа слоя изображения NFT.
    /// </summary>
    public class NftImageLayerAddedEvent :
        DomainEvent<Entities.NftImageLayer>,
        IMapFromTo<Entities.NftImageLayer, NftImageLayerAddedEvent>,
        IHasName,
        IHasIsActive,
        IHasIsReadOnly
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerAddedEvent"/>.
        /// </summary>
        /// <remarks>
        /// Для <see cref="IMapFromTo{TSource,TDestination}"/>.
        /// </remarks>
        public NftImageLayerAddedEvent()
            : base(Guid.Empty, null, null)
        {
            Name = string.Empty;
            NftImageLayerUri = string.Empty;
            ArtistDid = string.Empty;
        }

        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerAddedEvent"/>.
        /// </summary>
        /// <param name="nftImageLayer">Слой изображения NFT.</param>
        /// <param name="eventDescription">Описание события.</param>
        public NftImageLayerAddedEvent(Entities.NftImageLayer nftImageLayer, string eventDescription)
            : base(
                nftImageLayer.Id,
                eventDescription,
                nftImageLayer.Version,
                typeof(Entities.NftImageLayer))
        {
            Id = nftImageLayer.Id;
            Name = nftImageLayer.Name;
            TypeId = nftImageLayer.TypeId;
            NftImageLayerUri = nftImageLayer.NftImageLayerUri;
            ArtistDid = nftImageLayer.ArtistDid;
            IsReadOnly = nftImageLayer.IsReadOnly;
            IsActive = nftImageLayer.IsActive;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public Guid Id { get; private set; }

        /// <inheritdoc cref="Entities.NftImageLayer.Name"/>
        [JsonInclude]
        public string Name { get; private set; }

        /// <inheritdoc cref="Entities.NftImageLayer.TypeId"/>
        [JsonInclude]
        public Guid TypeId { get; private set; }

        /// <inheritdoc cref="Entities.NftImageLayer.NftImageLayerUri"/>
        [JsonInclude]
        public string NftImageLayerUri { get; private set; }

        /*/// <summary>
        /// Слой изображения NFT.
        /// </summary>
        [JsonInclude]
        public FileUploadRequest NftImageLayer { get; private set; }*/

        /// <inheritdoc cref="Entities.NftImageLayer.ArtistDid"/>
        [JsonInclude]
        public string ArtistDid { get; private set; }

        /// <inheritdoc cref="Entities.NftImageLayer.IsReadOnly"/>
        [JsonInclude]
        public bool IsReadOnly { get; private set; }

        /// <inheritdoc cref="Entities.NftImageLayer.IsActive"/>
        [JsonInclude]
        public bool IsActive { get; private set; }

        /// <inheritdoc/>
        void IMapFromTo<Entities.NftImageLayer, NftImageLayerAddedEvent>.Mapping(Profile profile, bool useReverseMap)
        {
            // меняем порядок сопоставления
            profile.CreateMap<NftImageLayerAddedEvent, Entities.NftImageLayer>()
                .ForMember(dest => dest.Version, source => source.MapFrom(c => c.AggregateVersion));
        }
    }
}