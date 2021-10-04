// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerExtendedAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Domain.Abstractions;

namespace Uchoose.Domain.Marketplace.Entities.ExtendedAttributes
{
    /// <summary>
    /// Расширенный атрибут типа слоя изображения NFT.
    /// </summary>
    public class NftImageLayerExtendedAttribute :
        ExtendedAttribute<Guid, NftImageLayer>
    {
        /// <inheritdoc/>
        public override Guid GenerateNewId()
        {
            return Guid.NewGuid();
        }
    }
}