// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerQueryHandler.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Uchoose.DataAccess.Marketplace.Interfaces.Contexts;
using Uchoose.Domain.Exceptions;
using Uchoose.ExcelService.Interfaces;
using Uchoose.ExcelService.Interfaces.Requests;
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries.Responses;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Mappings.Converters;
using Uchoose.Utils.Specifications;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Queries
{
    /// <summary>
    /// Обработчик запросов для слоёв изображений NFT.
    /// </summary>
    internal class NftImageLayerQueryHandler :
        IRequestHandler<GetNftImageLayersQuery, PaginatedResult<NftImageLayerResponse>>,
        IRequestHandler<GetNftImageLayerByIdQuery, Result<NftImageLayerResponse>>,
        IRequestHandler<ExportNftImageLayersQuery, Result<string>>
    {
        private readonly IMarketplaceDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<NftImageLayerQueryHandler> _localizer;
        private readonly IStringLocalizer<Domain.Marketplace.Entities.NftImageLayer> _nftImageLayerLocalizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerQueryHandler"/>.
        /// </summary>
        /// <param name="context"><see cref="IMarketplaceDbContext"/>.</param>
        /// <param name="mapper"><see cref="IMapper"/>.</param>
        /// <param name="excelService"><see cref="IExcelService"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="nftImageLayerLocalizer"><see cref="IStringLocalizer{T}"/> для <see cref="NftImageLayer"/>.</param>
        public NftImageLayerQueryHandler(
            IMarketplaceDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<NftImageLayerQueryHandler> localizer,
            IStringLocalizer<Domain.Marketplace.Entities.NftImageLayer> nftImageLayerLocalizer)
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
            _nftImageLayerLocalizer = nftImageLayerLocalizer;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<NftImageLayerResponse>> Handle(GetNftImageLayersQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var queryable = _context.NftImageLayers.AsNoTracking().AsQueryable();

            if (query.IsReadOnly != null)
            {
                queryable = queryable.Where(x => x.IsReadOnly.Equals(query.IsReadOnly));
            }

            if (query.IsActive != null)
            {
                queryable = queryable.Where(x => x.IsActive.Equals(query.IsActive));
            }

            var searchSpecification = new SearchSpecification<Domain.Marketplace.Entities.NftImageLayer>(query.Search);

            string ordering = new OrderByConverter().Convert(query.OrderBy);
            queryable = ordering.IsPresent() ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            var nftImageLayerList = await queryable
                .Specify(searchSpecification)
                .Select(e => _mapper.Map<NftImageLayerResponse>(e))
                .ToPaginatedListAsync(query.PageNumber, query.PageSize);

            return _mapper.Map<PaginatedResult<NftImageLayerResponse>>(nftImageLayerList);
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<NftImageLayerResponse>> Handle(GetNftImageLayerByIdQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var nftImageLayer = await _context.NftImageLayers.AsNoTracking().Where(c => c.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            if (nftImageLayer == null)
            {
                throw new EntityNotFoundException<Guid, Domain.Marketplace.Entities.NftImageLayer>(query.Id, _localizer);
            }

            var mappedNftImageLayer = _mapper.Map<NftImageLayerResponse>(nftImageLayer);
            return await Result<NftImageLayerResponse>.SuccessAsync(mappedNftImageLayer);
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<string>> Handle(ExportNftImageLayersQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var queryable = _context.NftImageLayers.AsNoTracking().AsQueryable();

            if (query.IsReadOnly != null)
            {
                queryable = queryable.Where(x => x.IsReadOnly.Equals(query.IsReadOnly));
            }

            if (query.IsActive != null)
            {
                queryable = queryable.Where(x => x.IsActive.Equals(query.IsActive));
            }

            var searchSpecification = new SearchSpecification<Domain.Marketplace.Entities.NftImageLayer>(query.Search);

            string ordering = new OrderByConverter().Convert(query.OrderBy);
            queryable = ordering.IsPresent() ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            var nftImageLayerList = await queryable
                .Specify(searchSpecification)
                .ToPaginatedListAsync(query.PageNumber, query.PageSize);

            var exportRequest = _mapper.Map<ExportRequest<Guid, Domain.Marketplace.Entities.NftImageLayer>>(query);
            exportRequest.Data = nftImageLayerList.Data;
            exportRequest.Mappers = IExportable<Guid, Domain.Marketplace.Entities.NftImageLayer>.ExportMappers(query.Properties, _nftImageLayerLocalizer);
            exportRequest.SheetName = _localizer["NftImageLayers"];
            exportRequest.CheckProperties = query.Properties.Count > 0;

            var exportResult = await _excelService.ExportAsync(exportRequest);

            if (exportResult.Succeeded)
            {
                return await Result<string>.SuccessAsync(exportResult.Data, _localizer["Nft Image Layers Exported."]);
            }

            return await Result<string>.FailAsync(exportResult.Data, exportResult.Messages);
        }
    }
}