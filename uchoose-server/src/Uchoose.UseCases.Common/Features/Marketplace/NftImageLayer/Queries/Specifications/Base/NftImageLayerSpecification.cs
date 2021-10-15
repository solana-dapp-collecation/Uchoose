// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerSpecification.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Specifications;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries.Specifications.Base
{
    /// <summary>
    /// Спецификация для <see cref="NftImageLayer"/>.
    /// </summary>
    internal abstract class NftImageLayerSpecification :
        UchooseSpecification<Domain.Marketplace.Entities.NftImageLayer>
    {
    }
}