// ------------------------------------------------------------------------------------------------------
// <copyright file="RemoveExtendedAttributeCommand.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using MediatR;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Commands
{
    /// <summary>
    /// Команда для удаления расширенного атрибута сущности.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    // ReSharper disable once UnusedTypeParameter
    public class RemoveExtendedAttributeCommand<TEntityId, TEntity> :
        IRequest<Result<Guid>>,
        IHasId<Guid>
            where TEntity : class, IEntity<TEntityId>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="RemoveExtendedAttributeCommand{TEntityId, TEntity}"/>.
        /// </summary>
        /// <param name="id">Идентификатор расширенного атрибута сущности.</param>
        public RemoveExtendedAttributeCommand(Guid id)
        {
            Id = id;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        public Guid Id { get; }
    }
}