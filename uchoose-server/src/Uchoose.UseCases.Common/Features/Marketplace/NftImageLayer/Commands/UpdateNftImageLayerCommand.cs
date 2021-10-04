// ------------------------------------------------------------------------------------------------------
// <copyright file="UpdateNftImageLayerCommand.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using MediatR;
using Uchoose.Utils.Attributes.Mappings;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Logging;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Commands
{
    /// <summary>
    /// Команда для обновления слоя изображения NFT.
    /// </summary>
    [UseReverseMap(typeof(Domain.Marketplace.Entities.NftImageLayer))]
    public class UpdateNftImageLayerCommand :
        IRequest<Result<Guid>>,
        IMapFromTo<Domain.Marketplace.Entities.NftImageLayer, UpdateNftImageLayerCommand>,
        ILoggable,
        IHasName,
        IHasIsActive,
        IHasIsReadOnly
    {
        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public Guid Id { get; set; }

        /// <inheritdoc cref="Domain.Marketplace.Entities.NftImageLayer.Name"/>
        /// <example>Example</example>
#pragma warning disable 8618
        public string Name { get; set; }
#pragma warning restore 8618

        /// <inheritdoc cref="Domain.Marketplace.Entities.NftImageLayer.TypeId"/>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public Guid TypeId { get; set; }

        // TODO - вместо этого нужно загружать изображение (и его имя, расширение)

        /// <inheritdoc cref="Domain.Marketplace.Entities.NftImageLayer.NftImageLayerUri"/>
        /// <example>Example</example>
        public string NftImageLayerUri { get; set; }

        /*/// <summary>
        /// Слой изображения NFT.
        /// </summary>
        public FileUploadRequest NftImageLayer { get; set; }*/

        /// <inheritdoc cref="Domain.Marketplace.Entities.NftImageLayer.ArtistDid"/>
        /// <example>Example</example>
        public string ArtistDid { get; set; }

        /// <inheritdoc cref="Domain.Marketplace.Entities.NftImageLayer.IsReadOnly"/>
        /// <example>false</example>
        public bool IsReadOnly { get; set; }

        /// <inheritdoc cref="Domain.Marketplace.Entities.NftImageLayer.IsActive"/>
        /// <example>true</example>
        public bool IsActive { get; set; }
    }
}