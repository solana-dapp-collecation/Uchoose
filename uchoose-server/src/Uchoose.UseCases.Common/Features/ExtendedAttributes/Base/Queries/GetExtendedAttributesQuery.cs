// ------------------------------------------------------------------------------------------------------
// <copyright file="GetExtendedAttributesQuery.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using MediatR;
using Uchoose.Domain.Contracts;
using Uchoose.Domain.Enums;
using Uchoose.Domain.Filters;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries.Responses;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Filters;
using Uchoose.Utils.Mappings.Converters;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries
{
    /// <summary>
    /// Запрос на получение всех расширенных атрибутов сущности с пагинацеией.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public class GetExtendedAttributesQuery<TEntityId, TEntity> :
        IRequest<PaginatedResult<ExtendedAttributeResponse<TEntityId>>>,
        IPaginated,
        ISearchableRequest
            where TEntity : class, IEntity<TEntityId>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="GetExtendedAttributesQuery{TEntityId, TEntity}"/>.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        public GetExtendedAttributesQuery(ExtendedAttributePaginationFilter<TEntityId, TEntity> filter)
        {
            PageNumber = filter.PageNumber;
            PageSize = filter.PageSize;
            Search = filter.Search;
            OrderBy = new OrderByConverter().Convert(filter.OrderBy);
            EntityId = filter.EntityId;
            Type = filter.Type;
            ExternalId = filter.ExternalId;
            Group = filter.Group;
        }

        /// <inheritdoc cref="IPaginated.PageNumber"/>
        public int PageNumber { get; }

        /// <inheritdoc cref="IPaginated.PageSize"/>
        public int PageSize { get; }

        /// <inheritdoc cref="SearchFilter"/>
        public SearchFilter? Search { get; }

        /// <summary>
        /// Массив строк с полями для сортировки.
        /// </summary>
        public string[] OrderBy { get; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.EntityId"/>
        public TEntityId? EntityId { get; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Type"/>
        public ExtendedAttributeType? Type { get; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.ExternalId"/>
        public string? ExternalId { get; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Group"/>
        public string? Group { get; }
    }
}