// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeCommandHandler.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Events.ExtendedAttributes;
using Uchoose.Domain.Exceptions;
using Uchoose.Utils.Constants.Caching;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Commands
{
    /// <summary>
    /// Обработчик команд расширенных атрибутов сущностей.
    /// </summary>
    /// <remarks>
    /// Для локализации.
    /// </remarks>
    public class ExtendedAttributeCommandHandler
    {
        // for localization
    }

    /// <summary>
    /// Обработчик команд расширенных атрибутов сущностей.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    /// <typeparam name="TExtendedAttribute">Тип расширенного атрибута сущности.</typeparam>
    public class ExtendedAttributeCommandHandler<TEntityId, TEntity, TExtendedAttribute> :
        IRequestHandler<RemoveExtendedAttributeCommand<TEntityId, TEntity>, Result<Guid>>,
        IRequestHandler<AddExtendedAttributeCommand<TEntityId, TEntity>, Result<Guid>>,
        IRequestHandler<UpdateExtendedAttributeCommand<TEntityId, TEntity>, Result<Guid>>
            where TEntity : class, IEntity<TEntityId>
            where TExtendedAttribute : ExtendedAttribute<TEntityId, TEntity>
    {
        private readonly IDistributedCache _cache;
        private readonly IExtendedAttributeDbContext<TEntityId, TEntity, TExtendedAttribute> _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ExtendedAttributeCommandHandler> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ExtendedAttributeCommandHandler{TEntityId, TEntity, TExtendedAttribute}"/>.
        /// </summary>
        /// <param name="context"><see cref="IExtendedAttributeDbContext{TEntityId, TEntity, TExtendedAttribute}"/>.</param>
        /// <param name="mapper"><see cref="IMapper"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="cache"><see cref="IDistributedCache"/>.</param>
        public ExtendedAttributeCommandHandler(
            IExtendedAttributeDbContext<TEntityId, TEntity, TExtendedAttribute> context,
            IMapper mapper,
            IStringLocalizer<ExtendedAttributeCommandHandler> localizer,
            IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _cache = cache;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(AddExtendedAttributeCommand<TEntityId, TEntity> command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var entity = await _context.Entities.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(command.EntityId), cancellationToken);
            if (entity == null)
            {
                throw new EntityNotFoundException<TEntityId, TEntity>(command.EntityId, _localizer);
            }

            bool isKeyUsed = await _context.ExtendedAttributes.AsNoTracking()
                .AnyAsync(ea => ea.EntityId.Equals(command.EntityId) && ea.Key.Equals(command.Key), cancellationToken);
            if (isKeyUsed)
            {
                throw new EntityAlreadyExistsException<Guid, TExtendedAttribute>(nameof(ExtendedAttribute<TEntityId, TEntity>.Key), command.Key, _localizer);
            }

            var extendedAttribute = _mapper.Map<TExtendedAttribute>(command);
            extendedAttribute.AddDomainEvent(new ExtendedAttributeAddedEvent<TEntityId, TEntity>(extendedAttribute, string.Format(_localizer["'{0}' Extended Attribute '{1}' added."]!, typeof(TEntity).GetGenericTypeName(), extendedAttribute.Key), extendedAttribute.GetType().GetGenericTypeName()));
            await _context.ExtendedAttributes.AddAsync(extendedAttribute, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(extendedAttribute.Id, string.Format(_localizer["'{0}' Extended Attribute '{1}' added."], typeof(TEntity).GetGenericTypeName(), extendedAttribute.Key));
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveExtendedAttributeCommand<TEntityId, TEntity> command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var extendedAttribute = await _context.ExtendedAttributes.FirstOrDefaultAsync(ea => ea.Id == command.Id, cancellationToken);
            if (extendedAttribute == null)
            {
                throw new EntityNotFoundException<Guid, TExtendedAttribute>(command.Id, _localizer);
            }

            if (extendedAttribute.IsSoftDeleted())
            {
                throw new EntityIsDeletedException<Guid, TExtendedAttribute>(_localizer);
            }

            _context.ExtendedAttributes.Remove(extendedAttribute);
            extendedAttribute.AddDomainEvent(new ExtendedAttributeRemovedEvent<TEntity>(command.Id, string.Format(_localizer["'{0}' Extended Attribute '{1}' deleted."], typeof(TEntity).GetGenericTypeName(), extendedAttribute.Key), extendedAttribute.GetType().GetGenericTypeName()));
            await _context.SaveChangesAsync(cancellationToken);
            await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, ExtendedAttribute<TEntityId, TEntity>>(command.Id), cancellationToken);
            return await Result<Guid>.SuccessAsync(extendedAttribute.Id, string.Format(_localizer["{0} Extended Attribute '{1}' deleted."], typeof(TEntity).GetGenericTypeName(), extendedAttribute.Key));
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateExtendedAttributeCommand<TEntityId, TEntity> command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var extendedAttribute = await _context.ExtendedAttributes.Where(ea => ea.Id.Equals(command.Id)).FirstOrDefaultAsync(cancellationToken);
            if (extendedAttribute == null)
            {
                throw new EntityNotFoundException<Guid, TExtendedAttribute>(command.Id, _localizer);
            }

            if (!extendedAttribute.EntityId.Equals(command.EntityId))
            {
                throw new EntityNotFoundException<TEntityId, TEntity>(command.EntityId, _localizer);
            }

            if (extendedAttribute.IsSoftDeleted())
            {
                throw new EntityIsDeletedException<Guid, TExtendedAttribute>(_localizer);
            }

            bool isKeyUsed = await _context.ExtendedAttributes.AsNoTracking()
                .AnyAsync(ea => ea.Id != extendedAttribute.Id && ea.EntityId.Equals(command.EntityId) && ea.Key.Equals(command.Key), cancellationToken);
            if (isKeyUsed)
            {
                throw new EntityAlreadyExistsException<Guid, TExtendedAttribute>(nameof(ExtendedAttribute<TEntityId, TEntity>.Key), command.Key, _localizer);
            }

            extendedAttribute = _mapper.Map(command, extendedAttribute);
            extendedAttribute.AddDomainEvent(new ExtendedAttributeUpdatedEvent<TEntityId, TEntity>(extendedAttribute, string.Format(_localizer["'{0}' Extended Attribute '{1}' updated."]!, typeof(TEntity).GetGenericTypeName(), extendedAttribute.Key), extendedAttribute.GetType().GetGenericTypeName()));
            _context.ExtendedAttributes.Update(extendedAttribute);
            await _context.SaveChangesAsync(cancellationToken);
            await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, ExtendedAttribute<TEntityId, TEntity>>(command.Id), cancellationToken);
            return await Result<Guid>.SuccessAsync(extendedAttribute.Id, string.Format(_localizer["'{0}' Extended Attribute '{1}' updated."], typeof(TEntity).GetGenericTypeName(), extendedAttribute.Key));
        }
    }
}