// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerPaginationFilter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Filters;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries.Filters
{
    /// <summary>
    /// Фильтр для получения слоёв изображений NFT с пагинацией.
    /// </summary>
    public class NftImageLayerPaginationFilter :
        PaginationFilter,
        IHasIsActive<bool?>,
        IHasIsReadOnly<bool?>
    {
        /// <inheritdoc/>
        /// <example>null</example>
        public bool? IsReadOnly { get; set; }

        /// <inheritdoc/>
        /// <example>null</example>
        public bool? IsActive { get; set; }
    }
}