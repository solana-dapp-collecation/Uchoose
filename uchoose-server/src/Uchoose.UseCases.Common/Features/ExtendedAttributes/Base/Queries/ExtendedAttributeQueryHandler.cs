// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeQueryHandler.cs" company="Life Loop">
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
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Exceptions;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries.Responses;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Mappings.Converters;
using Uchoose.Utils.Specifications;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries
{
    /// <summary>
    /// Обработчик запросов расширенных атрибутов сущностей.
    /// </summary>
    /// <remarks>
    /// Для локализации.
    /// </remarks>
    public class ExtendedAttributeQueryHandler
    {
        // for localization
    }

    /// <summary>
    /// Обработчик запросов расширенных атрибутов сущностей.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    /// <typeparam name="TExtendedAttribute">Тип расширенного атрибута сущности.</typeparam>
    internal class ExtendedAttributeQueryHandler<TEntityId, TEntity, TExtendedAttribute> :
        IRequestHandler<GetExtendedAttributesQuery<TEntityId, TEntity>, PaginatedResult<ExtendedAttributeResponse<TEntityId>>>,
        IRequestHandler<GetExtendedAttributeByIdQuery<TEntityId, TEntity>, Result<ExtendedAttributeResponse<TEntityId>>>
            where TEntity : class, IEntity<TEntityId>
            where TExtendedAttribute : ExtendedAttribute<TEntityId, TEntity>
    {
        private readonly IExtendedAttributeDbContext<TEntityId, TEntity, TExtendedAttribute> _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ExtendedAttributeQueryHandler> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ExtendedAttributeQueryHandler{TEntityId, TEntity, TExtendedAttribute}"/>.
        /// </summary>
        /// <param name="context"><see cref="IExtendedAttributeDbContext{TEntityId, TEntity, TExtendedAttribute}"/>.</param>
        /// <param name="mapper"><see cref="IMapper"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public ExtendedAttributeQueryHandler(
            IExtendedAttributeDbContext<TEntityId, TEntity, TExtendedAttribute> context,
            IMapper mapper,
            IStringLocalizer<ExtendedAttributeQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<ExtendedAttributeResponse<TEntityId>>> Handle(GetExtendedAttributesQuery<TEntityId, TEntity> query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var queryable = _context.ExtendedAttributes.AsNoTracking().AsQueryable();

            // применяем параметры фильтрации
            if (query.EntityId != null && !query.EntityId.Equals(default(TEntityId)))
            {
                queryable = queryable.Where(b => b.EntityId.Equals(query.EntityId));
            }

            if (query.Type != null)
            {
                queryable = queryable.Where(b => b.Type == query.Type);
            }

            if (query.ExternalId.IsPresent())
            {
                queryable = queryable.Where(x =>
                    x.ExternalId != null && EF.Functions.Like(x.ExternalId.ToLower(), $"%{query.ExternalId.ToLower()}%"));
            }

            if (query.Group.IsPresent())
            {
                queryable = queryable.Where(x =>
                    x.Group != null && EF.Functions.Like(x.Group.ToLower(), $"%{query.Group.ToLower()}%"));
            }

            var searchSpecification = new SearchSpecification<TExtendedAttribute>(query.Search);

            string ordering = new OrderByConverter().Convert(query.OrderBy);
            queryable = ordering.IsPresent() ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            var extendedAttributeList = await queryable
                .Specify(searchSpecification)
                .Select(e => _mapper.Map<ExtendedAttributeResponse<TEntityId>>(e))
                .AsNoTracking()
                .ToPaginatedListAsync(query.PageNumber, query.PageSize);

            return _mapper.Map<PaginatedResult<ExtendedAttributeResponse<TEntityId>>>(extendedAttributeList);
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<ExtendedAttributeResponse<TEntityId>>> Handle(GetExtendedAttributeByIdQuery<TEntityId, TEntity> query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var extendedAttribute = await _context.ExtendedAttributes.Where(b => b.Id == query.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (extendedAttribute == null)
            {
                throw new EntityNotFoundException<Guid, TExtendedAttribute>(query.Id, _localizer);
            }

            var mappedExtendedAttribute = _mapper.Map<ExtendedAttributeResponse<TEntityId>>(extendedAttribute);
            return await Result<ExtendedAttributeResponse<TEntityId>>.SuccessAsync(mappedExtendedAttribute);
        }
    }
}