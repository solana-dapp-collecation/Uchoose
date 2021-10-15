// ------------------------------------------------------------------------------------------------------
// <copyright file="GetExtendedAttributeByIdQuery.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using MediatR;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries.Responses;
using Uchoose.Utils.Contracts.Caching;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries
{
    /// <summary>
    /// Запрос на получение расширенного атрибута сущности по его идентификатору.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    // ReSharper disable once UnusedTypeParameter
    public class GetExtendedAttributeByIdQuery<TEntityId, TEntity> :
        IRequest<Result<ExtendedAttributeResponse<TEntityId>>>,
        ICacheableQuery
            where TEntity : class, IEntity<TEntityId>
    {
        /// <summary>
        /// Идентификатор расширенного атрибута сущности.
        /// </summary>
        public Guid Id { get; protected set; }

        /// <inheritdoc/>
        public bool BypassCache { get; protected set; }

        /// <inheritdoc/>
        public string CacheKey { get; protected set; }

        /// <inheritdoc/>
        public bool RefreshCachedEntry { get; protected set; }

        /// <inheritdoc/>
        public bool ReplaceCachedEntry { get; protected set; }

        /// <inheritdoc/>
        public TimeSpan? SlidingExpiration { get; protected set; }
    }
}