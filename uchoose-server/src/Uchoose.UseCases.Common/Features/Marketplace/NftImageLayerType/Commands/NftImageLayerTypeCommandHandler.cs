// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerTypeCommandHandler.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Uchoose.Application.Constants.Role;
using Uchoose.CurrentUserService.Interfaces;
using Uchoose.DataAccess.Marketplace.Interfaces.Contexts;
using Uchoose.Domain.Exceptions;
using Uchoose.Domain.Marketplace.Events.NftImageLayerType;
using Uchoose.Domain.Marketplace.Exceptions;
using Uchoose.ExcelService.Interfaces;
using Uchoose.ExcelService.Interfaces.Requests;
using Uchoose.Utils.Constants.Caching;
using Uchoose.Utils.Contracts.Importing;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Commands
{
    /// <summary>
    /// Обработчик команд для типов слоёв изображений NFT.
    /// </summary>
    internal class NftImageLayerTypeCommandHandler :
        IRequestHandler<AddNftImageLayerTypeCommand, Result<Guid>>,
        IRequestHandler<UpdateNftImageLayerTypeCommand, Result<Guid>>,
        IRequestHandler<RemoveNftImageLayerTypeCommand, Result<Guid>>,
        IRequestHandler<ImportNftImageLayerTypesCommand, Result<int>>
    {
        private readonly IDistributedCache _cache;
        private readonly IExcelService _excelService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMarketplaceDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<NftImageLayerTypeCommandHandler> _localizer;
        private readonly IStringLocalizer<Domain.Marketplace.Entities.NftImageLayerType> _nftImageLayerTypeLocalizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerTypeCommandHandler"/>.
        /// </summary>
        /// <param name="context"><see cref="IMarketplaceDbContext"/>.</param>
        /// <param name="mapper"><see cref="IMapper"/>.</param>
        /// <param name="mediator"><see cref="IMediator"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="nftImageLayerTypeLocalizer"><see cref="IStringLocalizer{T}"/> для <see cref="NftImageLayerType"/>.</param>
        /// <param name="cache"><see cref="IDistributedCache"/>.</param>
        /// <param name="excelService"><see cref="IExcelService"/>.</param>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        public NftImageLayerTypeCommandHandler(
            IMarketplaceDbContext context,
            IMapper mapper,
            IMediator mediator,
            IStringLocalizer<NftImageLayerTypeCommandHandler> localizer,
            IStringLocalizer<Domain.Marketplace.Entities.NftImageLayerType> nftImageLayerTypeLocalizer,
            IDistributedCache cache,
            IExcelService excelService,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _localizer = localizer;
            _nftImageLayerTypeLocalizer = nftImageLayerTypeLocalizer;
            _cache = cache;
            _excelService = excelService;
            _currentUserService = currentUserService;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(AddNftImageLayerTypeCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            bool isNftImageLayerTypeNameUsed = await _context.NftImageLayerTypes
                .AsNoTracking()
                .AnyAsync(x => x.Name.Equals(command.Name), cancellationToken);
            if (isNftImageLayerTypeNameUsed)
            {
                throw new EntityAlreadyExistsException<Guid, Domain.Marketplace.Entities.NftImageLayerType>(nameof(Domain.Marketplace.Entities.NftImageLayerType.Name), command.Name, _localizer);
            }

            var nftImageLayerType = _mapper.Map<Domain.Marketplace.Entities.NftImageLayerType>(command);
            nftImageLayerType.AddDomainEvent(new NftImageLayerTypeAddedEvent(nftImageLayerType, string.Format(_localizer["NFT Image Layer Type '{0}' added."]!, nftImageLayerType.Name)));
            await _context.NftImageLayerTypes.AddAsync(nftImageLayerType, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(nftImageLayerType.Id, string.Format(_localizer["NFT Image Layer Type '{0}' added."], nftImageLayerType.Name));
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateNftImageLayerTypeCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var nftImageLayerType = await _context.NftImageLayerTypes.Where(x => x.Id.Equals(command.Id)).FirstOrDefaultAsync(cancellationToken);
            if (nftImageLayerType == null)
            {
                throw new EntityNotFoundException<Guid, Domain.Marketplace.Entities.NftImageLayerType>(command.Id, _localizer);
            }

            if (nftImageLayerType.IsReadOnly && !_currentUserService.IsInRole(RoleConstants.SuperAdmin))
            {
                throw new MarketplaceException(_localizer["Update Not Allowed Because This NFT Image Layer Type Is Read Only"], statusCode: HttpStatusCode.BadRequest);
            }

            if (nftImageLayerType.IsSoftDeleted())
            {
                throw new EntityIsDeletedException<Guid, Domain.Marketplace.Entities.NftImageLayerType>(_localizer);
            }

            bool isNftImageLayerTypeNameUsed = await _context.NftImageLayerTypes
                .AsNoTracking()
                .AnyAsync(x => x.Id != command.Id && x.Name.Equals(command.Name), cancellationToken);
            if (isNftImageLayerTypeNameUsed)
            {
                throw new EntityAlreadyExistsException<Guid, Domain.Marketplace.Entities.NftImageLayerType>(nameof(Domain.Marketplace.Entities.NftImageLayerType.Name), command.Name, _localizer);
            }

            nftImageLayerType = _mapper.Map(command, nftImageLayerType);
            nftImageLayerType.AddDomainEvent(new NftImageLayerTypeUpdatedEvent(nftImageLayerType, string.Format(_localizer["NFT Image Layer Type '{0}' updated."]!, nftImageLayerType.Name)));
            _context.NftImageLayerTypes.Update(nftImageLayerType);
            await _context.SaveChangesAsync(cancellationToken);
            await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Domain.Marketplace.Entities.NftImageLayerType>(command.Id), cancellationToken);
            return await Result<Guid>.SuccessAsync(nftImageLayerType.Id, string.Format(_localizer["NFT Image Layer Type '{0}' updated."], nftImageLayerType.Name));
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveNftImageLayerTypeCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var nftImageLayerType = await _context.NftImageLayerTypes.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
            if (nftImageLayerType == null)
            {
                throw new EntityNotFoundException<Guid, Domain.Marketplace.Entities.NftImageLayerType>(command.Id, _localizer);
            }

            if (nftImageLayerType.IsReadOnly && !_currentUserService.IsInRole(RoleConstants.SuperAdmin))
            {
                throw new MarketplaceException(_localizer["Deletion Not Allowed Because This NFT Image Layer Type Is Read Only"], statusCode: HttpStatusCode.BadRequest);
            }

            if (nftImageLayerType.IsSoftDeleted())
            {
                throw new EntityIsDeletedException<Guid, Domain.Marketplace.Entities.NftImageLayerType>(_localizer);
            }

            bool isNftImageLayerTypeUsed = await _context.NftImageLayers.Include(x => x.Type)
                .AsNoTracking()
                .AnyAsync(x => x.Type.Id == command.Id, cancellationToken);
            if (isNftImageLayerTypeUsed)
            {
                throw new MarketplaceException(_localizer["NFT Image Layer Type Used In NFT Image Layer"], statusCode: HttpStatusCode.BadRequest);
            }

            _context.NftImageLayerTypes.Remove(nftImageLayerType);
            nftImageLayerType.AddDomainEvent(new NftImageLayerTypeRemovedEvent(command.Id, nftImageLayerType.Version, string.Format(_localizer["NFT Image Layer Type '{0}' deleted."], nftImageLayerType.Name)));
            await _context.SaveChangesAsync(cancellationToken);
            await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Domain.Marketplace.Entities.NftImageLayerType>(command.Id), cancellationToken);
            return await Result<Guid>.SuccessAsync(nftImageLayerType.Id, string.Format(_localizer["NFT Image Layer Type '{0}' deleted."], nftImageLayerType.Name));
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<int>> Handle(ImportNftImageLayerTypesCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            await using var stream = new MemoryStream(command.Data, false);
            var mappers = IImportable<Guid, Domain.Marketplace.Entities.NftImageLayerType>.DefaultImportMappers(_nftImageLayerTypeLocalizer);
            if ((command.Properties?.Where(p => p.IsPresent()) ?? Array.Empty<string>()).Any())
            {
                mappers = IImportable<Guid, Domain.Marketplace.Entities.NftImageLayerType>.ImportMappers(command.Properties, _localizer);
            }

            var importRequest = _mapper.Map<ImportRequest<Guid, Domain.Marketplace.Entities.NftImageLayerType>>(command);
            importRequest.DataStream = stream;
            importRequest.Mappers = mappers;
            importRequest.SheetName = _localizer["NftImageLayerTypes"];

            var result = await _excelService.ImportAsync(importRequest);

            if (result.Succeeded)
            {
                var importedNftImageLayerTypes = result.Data;
                var errors = new List<string>();
                bool errorsOccurred = false;
                foreach (var nftImageLayerType in importedNftImageLayerTypes)
                {
                    if (errorsOccurred)
                    {
                        break;
                    }

                    var addResult = await _mediator.Send(_mapper.Map<AddNftImageLayerTypeCommand>(nftImageLayerType), cancellationToken);
                    if (!addResult.Succeeded)
                    {
                        errorsOccurred = true;
                        errors.AddRange(addResult.Messages.Select(e => $"{(nftImageLayerType.Name.IsPresent() ? $"{nftImageLayerType.Name} - " : string.Empty)}{e}"));
                    }
                }

                if (errorsOccurred)
                {
                    return await Result<int>.FailAsync(result.Data.Count(), errors);
                }

                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(result.Data.Count(), result.Messages[0]);
            }
            else
            {
                return await Result<int>.FailAsync(result.Data?.Count() ?? 0, result.Messages);
            }
        }
    }
}