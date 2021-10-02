// ------------------------------------------------------------------------------------------------------
// <copyright file="EntityIsDeletedException.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;

using Microsoft.Extensions.Localization;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Extensions;

namespace Uchoose.Domain.Exceptions
{
    /// <summary>
    /// Исключение, указывающее, что сущность уже удалена.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public class EntityIsDeletedException<TEntityId, TEntity> : DomainException
        where TEntity : IEntity<TEntityId>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="EntityIsDeletedException{TEntityId, TEntity}"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        public EntityIsDeletedException(IStringLocalizer localizer)
            : base(string.Format(localizer["{0} is Deleted."], typeof(TEntity).GetGenericTypeName()), statusCode: HttpStatusCode.BadRequest)
        {
        }
    }
}