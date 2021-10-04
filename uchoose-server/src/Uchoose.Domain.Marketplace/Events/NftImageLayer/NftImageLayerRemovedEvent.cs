// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerRemovedEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Text.Json.Serialization;

using AutoMapper;
using Uchoose.Domain.Abstractions;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Mappings;

namespace Uchoose.Domain.Marketplace.Events.NftImageLayer
{
    /// <summary>
    /// Событие удаления типа слоя изображения NFT.
    /// </summary>
    public class NftImageLayerRemovedEvent :
        DomainEvent<Entities.NftImageLayer>,
        IMapFromTo<Entities.NftImageLayer, NftImageLayerRemovedEvent>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerRemovedEvent"/>.
        /// </summary>
        /// <remarks>
        /// Для <see cref="IMapFromTo{TSource,TDestination}"/>.
        /// </remarks>
        public NftImageLayerRemovedEvent()
            : base(Guid.Empty, null, null)
        {
        }

        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerRemovedEvent"/>.
        /// </summary>
        /// <param name="id">Идентификатор слоя изображения NFT.</param>
        /// <param name="version">Текущая версия агрегата.</param>
        /// <param name="eventDescription">Описание события.</param>
        public NftImageLayerRemovedEvent(Guid id, int? version, string eventDescription)
            : base(
                id,
                eventDescription,
                version,
                typeof(Entities.NftImageLayer))
        {
            Id = id;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [JsonInclude]
        public Guid Id { get; private set; }

        /// <inheritdoc/>
        void IMapFromTo<Entities.NftImageLayer, NftImageLayerRemovedEvent>.Mapping(Profile profile, bool useReverseMap)
        {
            // меняем порядок сопоставления
            profile.CreateMap<NftImageLayerRemovedEvent, Entities.NftImageLayer>()
                .ForMember(dest => dest.Version, source => source.MapFrom(c => c.AggregateVersion));
        }
    }
}