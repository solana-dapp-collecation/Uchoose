// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributePaginationFilter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using Uchoose.Domain.Contracts;
using Uchoose.Domain.Enums;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Filters;

namespace Uchoose.Domain.Filters
{
    /// <summary>
    /// Фильтр для получения расширенных атрибутов сущности с пагинацией.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    // ReSharper disable once UnusedTypeParameter
    public class ExtendedAttributePaginationFilter<TEntityId, TEntity> :
        PaginationFilter
            where TEntity : class, IEntity<TEntityId>
    {
        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.EntityId"/>
        public TEntityId? EntityId { get; set; }

        /// <inheritdoc cref="System.Type"/>
        public ExtendedAttributeType? Type { get; set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.ExternalId"/>
        public string? ExternalId { get; set; }

        /// <inheritdoc cref="System.Text.RegularExpressions.Group"/>
        public string? Group { get; set; }
    }
}