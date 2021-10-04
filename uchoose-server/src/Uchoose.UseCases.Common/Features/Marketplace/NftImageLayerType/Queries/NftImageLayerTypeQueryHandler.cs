// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerTypeQueryHandler.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
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
using Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries.Responses;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Mappings.Converters;
using Uchoose.Utils.Specifications;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Queries
{
    /// <summary>
    /// Обработчик запросов для типов слоёв изображений NFT.
    /// </summary>
    internal class NftImageLayerTypeQueryHandler :
        IRequestHandler<GetNftImageLayerTypesQuery, PaginatedResult<NftImageLayerTypeResponse>>,
        IRequestHandler<GetNftImageLayerTypeByIdQuery, Result<NftImageLayerTypeResponse>>,
        IRequestHandler<ExportNftImageLayerTypesQuery, Result<string>>
    {
        private readonly IMarketplaceDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<NftImageLayerTypeQueryHandler> _localizer;
        private readonly IStringLocalizer<Domain.Marketplace.Entities.NftImageLayerType> _nftImageLayerTypeLocalizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerTypeQueryHandler"/>.
        /// </summary>
        /// <param name="context"><see cref="IMarketplaceDbContext"/>.</param>
        /// <param name="mapper"><see cref="IMapper"/>.</param>
        /// <param name="excelService"><see cref="IExcelService"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="nftImageLayerTypeLocalizer"><see cref="IStringLocalizer{T}"/> для <see cref="NftImageLayerType"/>.</param>
        public NftImageLayerTypeQueryHandler(
            IMarketplaceDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<NftImageLayerTypeQueryHandler> localizer,
            IStringLocalizer<Domain.Marketplace.Entities.NftImageLayerType> nftImageLayerTypeLocalizer)
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
            _nftImageLayerTypeLocalizer = nftImageLayerTypeLocalizer;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<NftImageLayerTypeResponse>> Handle(GetNftImageLayerTypesQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            Expression<Func<Domain.Marketplace.Entities.NftImageLayerType, NftImageLayerTypeResponse>> expression = e =>
                _mapper.Map<NftImageLayerTypeResponse>(e);
            var queryable = _context.NftImageLayerTypes.AsNoTracking().AsQueryable();

            if (query.IsReadOnly != null)
            {
                queryable = queryable.Where(x => x.IsReadOnly.Equals(query.IsReadOnly));
            }

            if (query.IsActive != null)
            {
                queryable = queryable.Where(x => x.IsActive.Equals(query.IsActive));
            }

            var searchSpecification = new SearchSpecification<Domain.Marketplace.Entities.NftImageLayerType>(query.Search);

            string ordering = new OrderByConverter().Convert(query.OrderBy);
            queryable = ordering.IsPresent() ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            var nftImageLayerTypeList = await queryable
                .Specify(searchSpecification)
                .Select(expression)
                .ToPaginatedListAsync(query.PageNumber, query.PageSize);

            return _mapper.Map<PaginatedResult<NftImageLayerTypeResponse>>(nftImageLayerTypeList);
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<NftImageLayerTypeResponse>> Handle(GetNftImageLayerTypeByIdQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var nftImageLayerType = await _context.NftImageLayerTypes.AsNoTracking().Where(c => c.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            if (nftImageLayerType == null)
            {
                throw new EntityNotFoundException<Guid, Domain.Marketplace.Entities.NftImageLayerType>(query.Id, _localizer);
            }

            var mappedNftImageLayerType = _mapper.Map<NftImageLayerTypeResponse>(nftImageLayerType);
            return await Result<NftImageLayerTypeResponse>.SuccessAsync(mappedNftImageLayerType);
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<string>> Handle(ExportNftImageLayerTypesQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var queryable = _context.NftImageLayerTypes.AsNoTracking().AsQueryable();

            if (query.IsReadOnly != null)
            {
                queryable = queryable.Where(x => x.IsReadOnly.Equals(query.IsReadOnly));
            }

            if (query.IsActive != null)
            {
                queryable = queryable.Where(x => x.IsActive.Equals(query.IsActive));
            }

            var searchSpecification = new SearchSpecification<Domain.Marketplace.Entities.NftImageLayerType>(query.Search);

            string ordering = new OrderByConverter().Convert(query.OrderBy);
            queryable = ordering.IsPresent() ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            var nftImageLayerTypeList = await queryable
                .Specify(searchSpecification)
                .ToPaginatedListAsync(query.PageNumber, query.PageSize);

            var exportRequest = _mapper.Map<ExportRequest<Guid, Domain.Marketplace.Entities.NftImageLayerType>>(query);
            exportRequest.Data = nftImageLayerTypeList.Data;
            exportRequest.Mappers = IExportable<Guid, Domain.Marketplace.Entities.NftImageLayerType>.ExportMappers(query.Properties, _nftImageLayerTypeLocalizer);
            exportRequest.SheetName = _localizer["NftImageLayerTypes"];
            exportRequest.CheckProperties = query.Properties.Count > 0;

            var exportResult = await _excelService.ExportAsync(exportRequest);

            if (exportResult.Succeeded)
            {
                return await Result<string>.SuccessAsync(exportResult.Data, _localizer["Nft Image Layer Types Exported."]);
            }

            return await Result<string>.FailAsync(exportResult.Data, exportResult.Messages);
        }
    }
}