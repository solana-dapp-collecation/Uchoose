// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayersExportPaginationFilter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Utils.Filters;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries.Validators
{
    /// <summary>
    /// Фильтр для экспорта в файл слоёв изображений NFT с пагинацией.
    /// </summary>
    public class NftImageLayersExportPaginationFilter :
        ExportPaginationFilter<Guid, Domain.Marketplace.Entities.NftImageLayer>
    {
    }
}