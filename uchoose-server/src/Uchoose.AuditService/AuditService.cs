// ------------------------------------------------------------------------------------------------------
// <copyright file="AuditService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Uchoose.AuditService.Interfaces;
using Uchoose.AuditService.Interfaces.Requests;
using Uchoose.AuditService.Interfaces.Responses;
using Uchoose.AuditService.Specifications;
using Uchoose.DataAccess.Interfaces;
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.Domain.Entities;
using Uchoose.ExcelService.Interfaces;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Mappings.Converters;
using Uchoose.Utils.Specifications;
using Uchoose.Utils.Wrapper;

namespace Uchoose.AuditService
{
    /// <inheritdoc cref="IAuditService{TDbContext}"/>
    internal sealed class AuditService
    {
        // for localization
    }

    /// <inheritdoc cref="IAuditService{TDbContext}"/>
    /// <typeparam name="TIDbContext">Тип интерфейса контекста доступа к данным.</typeparam>
    /// <typeparam name="TDbContext">Тип контекста доступа к данным.</typeparam>
    internal sealed class AuditService<TIDbContext, TDbContext> :
        IAuditService<TIDbContext>
            where TIDbContext : IAuditableDbContext, IDbContextInterface
            where TDbContext : TIDbContext
    {
        private readonly TDbContext _context;
        private readonly IExcelService _excelService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AuditService> _localizer;
        private readonly IStringLocalizer<Audit> _auditLocalizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="AuditService"/>.
        /// </summary>
        /// <param name="context">Контекст доступа к данным.</param>
        /// <param name="excelService"><see cref="IExcelService"/>.</param>
        /// <param name="mapper"><see cref="IMapper"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="auditLocalizer"><see cref="IStringLocalizer{T}"/> для <see cref="Audit"/>.</param>
        public AuditService(
            TDbContext context,
            IExcelService excelService,
            IMapper mapper,
            IStringLocalizer<AuditService> localizer,
            IStringLocalizer<Audit> auditLocalizer)
        {
            _context = context;
            _excelService = excelService;
            _mapper = mapper;
            _localizer = localizer;
            _auditLocalizer = auditLocalizer;
        }

        /// <inheritdoc/>
        public async Task<PaginatedResult<AuditResponse>> GetAllAsync(GetAuditTrailsRequest request)
        {
            var queryable = _context.AuditTrails.AsNoTracking().AsQueryable();

            if (request.UserId != Guid.Empty)
            {
                queryable = queryable.Where(x => x.UserId.Equals(request.UserId));
            }

            var searchSpecification = new SearchSpecification<Audit>(request.Search);
            var dateRangeSpecification = new AuditByDateRangeSpecification(request.StartDateRange, request.EndDateRange);

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = ordering.IsPresent() ? queryable.OrderBy(ordering) : queryable.OrderByDescending(a => a.Id);

            return await queryable
                .Specify(dateRangeSpecification && searchSpecification)
                .Select(e => _mapper.Map<AuditResponse>(e))
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        /// <inheritdoc/>
        public async Task<IResult<string>> ExportToExcelAsync(ExportAuditTrailsRequest request)
        {
            var auditTrails = await GetAllAsync(_mapper.Map<GetAuditTrailsRequest>(request));

            return await _excelService.ExportAsync<Guid, Audit>(new()
            {
                Data = _mapper.Map<List<Audit>>(auditTrails.Data),
                Mappers = IExportable<Guid, Audit>.ExportMappers(request.Properties, _auditLocalizer),
                TitlesRowNumber = request.TitlesRowNumber,
                TitlesFirstColNumber = request.TitlesFirstColNumber,
                DataFirstRowNumber = request.DataFirstRowNumber,
                SheetName = string.IsNullOrWhiteSpace(request.SchemaName)
                    ? _localizer["Audit trails"]
                    : $"{_localizer["Audit trails"]} - {_localizer[request.SchemaName]}",
                CheckProperties = request.Properties.Count > 0
            });
        }
    }
}