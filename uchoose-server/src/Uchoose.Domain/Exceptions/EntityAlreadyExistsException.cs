// ------------------------------------------------------------------------------------------------------
// <copyright file="EntityAlreadyExistsException.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;

using Microsoft.Extensions.Localization;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Extensions;

namespace Uchoose.Domain.Exceptions
{
    /// <summary>
    /// Исключение, указывающее, что сущность уже существует.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public class EntityAlreadyExistsException<TEntityId, TEntity> : DomainException
        where TEntity : IEntity<TEntityId>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="EntityAlreadyExistsException{TEntityId, TEntity}"/>.
        /// </summary>
        /// <param name="property">Наименование свойства сущности.</param>
        /// <param name="value">Значение свойства сущности.</param>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        public EntityAlreadyExistsException(string property, string value, IStringLocalizer localizer)
            : base(string.Format(localizer["{0} with {1} : '{2}' already Exists."], typeof(TEntity).GetGenericTypeName(), property, value), statusCode: HttpStatusCode.BadRequest)
        {
        }
    }
}