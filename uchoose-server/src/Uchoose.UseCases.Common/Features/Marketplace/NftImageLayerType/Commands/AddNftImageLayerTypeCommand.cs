// ------------------------------------------------------------------------------------------------------
// <copyright file="AddNftImageLayerTypeCommand.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;

using MediatR;
using Uchoose.Utils.Attributes.Mappings;
using Uchoose.Utils.Contracts.Logging;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Commands
{
    /// <summary>
    /// Команда для добавления типа слоя изображения NFT.
    /// </summary>
    [UseReverseMap(typeof(Domain.Marketplace.Entities.NftImageLayerType))]
    public class AddNftImageLayerTypeCommand :
        IRequest<Result<Guid>>,
        IMapFromTo<Domain.Marketplace.Entities.NftImageLayerType, AddNftImageLayerTypeCommand>,
        ILoggable,
        IHasName,
        IHasDescription<string?>,
        IHasIsActive,
        IHasIsReadOnly
    {
        /// <inheritdoc cref="Domain.Marketplace.Entities.NftImageLayerType.Name"/>
        /// <example>Example</example>
#pragma warning disable 8618
        public string Name { get; set; }
#pragma warning restore 8618

        /// <inheritdoc cref="Domain.Marketplace.Entities.NftImageLayerType.Description"/>
        /// <example>Example</example>
        public string? Description { get; set; }

        /// <inheritdoc cref="Domain.Marketplace.Entities.NftImageLayerType.IsReadOnly"/>
        /// <example>false</example>
        public bool IsReadOnly { get; set; }

        /// <inheritdoc cref="Domain.Marketplace.Entities.NftImageLayerType.IsActive"/>
        /// <example>true</example>
        public bool IsActive { get; set; }
    }
}