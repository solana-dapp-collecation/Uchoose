// ------------------------------------------------------------------------------------------------------
// <copyright file="EventLogService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.DataAccess.Interfaces.EventLogging;
using Uchoose.Domain.Entities;
using Uchoose.Domain.Exceptions;
using Uchoose.EventLogService.Interfaces;
using Uchoose.EventLogService.Interfaces.Requests;
using Uchoose.EventLogService.Specifications;
using Uchoose.ExcelService.Interfaces;
using Uchoose.SerializationService.Interfaces;
using Uchoose.Utils.Contracts.Events;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Exceptions;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Mappings.Converters;
using Uchoose.Utils.Specifications;
using Uchoose.Utils.Wrapper;

namespace Uchoose.EventLogService
{
    /// <inheritdoc cref="IEventLogService"/>
    internal sealed class EventLogService :
        IEventLogService,
        ITransientService
    {
        private readonly IStringLocalizer<EventLogService> _localizer;
        private readonly IStringLocalizer<EventLog> _eventLogLocalizer;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEventLogger _eventLogger;
        private readonly IExcelService _excelService;
        private readonly IJsonSerializer _jsonSerializer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="EventLogService"/>.
        /// </summary>
        /// <param name="context"><see cref="IApplicationDbContext"/>.</param>
        /// <param name="mapper"><see cref="IMapper"/>.</param>
        /// <param name="eventLogger"><see cref="IEventLogger"/>.</param>
        /// <param name="excelService"><see cref="IExcelService"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="eventLogLocalizer"><see cref="IStringLocalizer{T}"/> для <see cref="EventLog"/>.</param>
        public EventLogService(
            IApplicationDbContext context,
            IMapper mapper,
            IEventLogger eventLogger,
            IExcelService excelService,
            IJsonSerializer jsonSerializer,
            IStringLocalizer<EventLogService> localizer,
            IStringLocalizer<EventLog> eventLogLocalizer)
        {
            _localizer = localizer;
            _eventLogLocalizer = eventLogLocalizer;
            _context = context;
            _mapper = mapper;
            _eventLogger = eventLogger;
            _excelService = excelService;
            _jsonSerializer = jsonSerializer;
        }

        /// <inheritdoc/>
        public async Task<Result<EventLog>> GetByIdAsync(Guid id)
        {
            var eventLog = await _context.EventLogs.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (eventLog == null)
            {
                throw new EntityNotFoundException<Guid, EventLog>(id, _eventLogLocalizer);
            }

            return await Result<EventLog>.SuccessAsync(eventLog);
        }

        /// <inheritdoc/>
        public async Task<Result<TEvent>> GetEventByLogIdAsync<TEvent>(Guid id)
            where TEvent : class, IEvent, new()
        {
            var eventLog = await GetByIdAsync(id);
            if (eventLog.Succeeded)
            {
                if (typeof(TEvent).GetGenericTypeName().Equals(eventLog.Data.MessageType))
                {
                    var @event = _jsonSerializer.Deserialize<TEvent>(eventLog.Data.Data);
                    return await Result<TEvent>.SuccessAsync(@event);
                }
                else
                {
                    throw new BadRequestException(_localizer["Wrong entity type!"]);
                }
            }

            // TODO - исключение?
            return await Result<TEvent>.FailAsync(eventLog.Messages);
        }

        /// <inheritdoc/>
        public async Task<PaginatedResult<EventLog>> GetAllAsync(GetEventLogsRequest request)
        {
            var queryable = _context.EventLogs.AsNoTracking().AsQueryable();

            if (request.UserId != Guid.Empty)
            {
                queryable = queryable.Where(x => x.UserId.Equals(request.UserId));
            }

            if (request.Email.IsPresent())
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.Email.ToLower(), $"%{request.Email.ToLower()}%"));
            }

            if (request.MessageType.IsPresent())
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.MessageType.ToLower(), $"%{request.MessageType.ToLower()}%"));
            }

            var searchSpecification = new SearchSpecification<EventLog>(request.Search);
            var dateRangeSpecification = new EventLogByDateRangeSpecification(request.StartDateRange, request.EndDateRange);
            var aggregateVersionRangeSpecification = new EventLogByAggregateVersionRangeSpecification(request.StartAggregateVersionRange, request.EndAggregateVersionRange);

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = ordering.IsPresent() ? queryable.OrderBy(ordering) : queryable.OrderByDescending(a => a.Timestamp);

            return await queryable
                .Specify(dateRangeSpecification && searchSpecification && aggregateVersionRangeSpecification)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        /// <inheritdoc/>
        public async Task<Result<Guid>> LogCustomEventAsync(LogEventRequest request)
        {
            var eventLog = _mapper.Map<EventLog>(request);
            await _eventLogger.SaveAsync(eventLog, default);
            return await Result<Guid>.SuccessAsync(eventLog.Id, _localizer["Custom Event Logged."]);
        }

        /// <inheritdoc/>
        public async Task<IResult<string>> ExportToExcelAsync(ExportEventLogsRequest request)
        {
            var eventLogs = await GetAllAsync(_mapper.Map<GetEventLogsRequest>(request));

            return await _excelService.ExportAsync<Guid, EventLog>(new()
            {
                Data = eventLogs.Data,
                Mappers = IExportable<Guid, EventLog>.ExportMappers(request.Properties, _eventLogLocalizer),
                TitlesRowNumber = request.TitlesRowNumber,
                TitlesFirstColNumber = request.TitlesFirstColNumber,
                DataFirstRowNumber = request.DataFirstRowNumber,
                SheetName = _localizer["Event Logs"],
                CheckProperties = request.Properties.Count > 0
            });
        }
    }
}