// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerType.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Contracts;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.Domain.Marketplace.Entities
{
    /// <summary>
    /// Тип слоя изображения NFT.
    /// </summary>
    public class NftImageLayerType :
        AuditableAggregate,
        IHasName,
        IHasDescription<string?>,
        IHasIsActive,
        IHasIsReadOnly
    {
        /// <summary>
        /// Наименование типа слоя изображения NFT.
        /// </summary>
#pragma warning disable 8618
        public string Name { get; set; }
#pragma warning restore 8618

        /// <summary>
        /// Описание типа слоя изображения NFT.
        /// </summary>
        public string? Description { get; set; }

        /// <inheritdoc/>
        public bool IsReadOnly { get; set; }

        /// <inheritdoc/>
        public bool IsActive { get; set; }

        /// <inheritdoc/>
        protected override void Apply(IDomainEvent @event)
        {
            When((dynamic)@event);
        }
    }
}