// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerCommandHandler.cs" company="Life Loop">
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
using Uchoose.Domain.Marketplace.Events.NftImageLayer;
using Uchoose.Domain.Marketplace.Exceptions;
using Uchoose.ExcelService.Interfaces;
using Uchoose.ExcelService.Interfaces.Requests;
using Uchoose.FileStorageService.Interfaces;
using Uchoose.Utils.Constants.Caching;
using Uchoose.Utils.Contracts.Importing;
using Uchoose.Utils.Enums;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Commands
{
    /// <summary>
    /// Обработчик команд для слоёв изображений NFT.
    /// </summary>
    internal class NftImageLayerCommandHandler :
        IRequestHandler<AddNftImageLayerCommand, Result<Guid>>,
        IRequestHandler<UpdateNftImageLayerCommand, Result<Guid>>,
        IRequestHandler<RemoveNftImageLayerCommand, Result<Guid>>,
        IRequestHandler<ImportNftImageLayersCommand, Result<int>>
    {
        private readonly IDistributedCache _cache;
        private readonly IExcelService _excelService;
        private readonly IFileStorageService _fileStorageService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMarketplaceDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<NftImageLayerCommandHandler> _localizer;
        private readonly IStringLocalizer<Domain.Marketplace.Entities.NftImageLayer> _nftImageLayerLocalizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="NftImageLayerCommandHandler"/>.
        /// </summary>
        /// <param name="context"><see cref="IMarketplaceDbContext"/>.</param>
        /// <param name="mapper"><see cref="IMapper"/>.</param>
        /// <param name="mediator"><see cref="IMediator"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="nftImageLayerLocalizer"><see cref="IStringLocalizer{T}"/> для <see cref="NftImageLayer"/>.</param>
        /// <param name="cache"><see cref="IDistributedCache"/>.</param>
        /// <param name="excelService"><see cref="IExcelService"/>.</param>
        /// <param name="fileStorageService"><see cref="IFileStorageService"/>.</param>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        public NftImageLayerCommandHandler(
            IMarketplaceDbContext context,
            IMapper mapper,
            IMediator mediator,
            IStringLocalizer<NftImageLayerCommandHandler> localizer,
            IStringLocalizer<Domain.Marketplace.Entities.NftImageLayer> nftImageLayerLocalizer,
            IDistributedCache cache,
            IExcelService excelService,
            IFileStorageService fileStorageService,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _localizer = localizer;
            _nftImageLayerLocalizer = nftImageLayerLocalizer;
            _cache = cache;
            _excelService = excelService;
            _fileStorageService = fileStorageService;
            _currentUserService = currentUserService;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(AddNftImageLayerCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            bool isNftImageLayerNameUsed = await _context.NftImageLayers
                .AsNoTracking()
                .AnyAsync(x => x.Name.Equals(command.Name) && x.ArtistDid.Equals(command.ArtistDid), cancellationToken);
            if (isNftImageLayerNameUsed)
            {
                throw new EntityAlreadyExistsException<Guid, Domain.Marketplace.Entities.NftImageLayer>(nameof(Domain.Marketplace.Entities.NftImageLayer.Name), command.Name, _localizer);
            }

            var nftImageLayer = _mapper.Map<Domain.Marketplace.Entities.NftImageLayer>(command);

            nftImageLayer.NftImageLayerUri = await _fileStorageService.UploadAsync<Domain.Marketplace.Entities.NftImageLayer>(command.NftImageLayer, FileType.Image, cancellationToken);

            nftImageLayer.AddDomainEvent(new NftImageLayerAddedEvent(nftImageLayer, string.Format(_localizer["NFT Image Layer '{0}' added."]!, nftImageLayer.Name)));
            await _context.NftImageLayers.AddAsync(nftImageLayer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(nftImageLayer.Id, string.Format(_localizer["NFT Image Layer '{0}' added."], nftImageLayer.Name));
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateNftImageLayerCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var nftImageLayer = await _context.NftImageLayers.Where(x => x.Id.Equals(command.Id)).FirstOrDefaultAsync(cancellationToken);
            if (nftImageLayer == null)
            {
                throw new EntityNotFoundException<Guid, Domain.Marketplace.Entities.NftImageLayer>(command.Id, _localizer);
            }

            if (nftImageLayer.IsReadOnly && !_currentUserService.IsInRole(RoleConstants.SuperAdmin))
            {
                throw new MarketplaceException(_localizer["Update Not Allowed Because This NFT Image Layer Is Read Only"], statusCode: HttpStatusCode.BadRequest);
            }

            if (nftImageLayer.IsSoftDeleted())
            {
                throw new EntityIsDeletedException<Guid, Domain.Marketplace.Entities.NftImageLayer>(_localizer);
            }

            bool isNftImageLayerNameUsed = await _context.NftImageLayers
                .AsNoTracking()
                .AnyAsync(x => x.Id != command.Id && x.Name.Equals(command.Name) && x.ArtistDid.Equals(command.ArtistDid), cancellationToken);
            if (isNftImageLayerNameUsed)
            {
                throw new EntityAlreadyExistsException<Guid, Domain.Marketplace.Entities.NftImageLayer>(nameof(Domain.Marketplace.Entities.NftImageLayer.Name), command.Name, _localizer);
            }

            nftImageLayer = _mapper.Map(command, nftImageLayer);

            if (command.NftImageLayer != null)
            {
                await _fileStorageService.DeleteAsync(nftImageLayer.NftImageLayerUri, cancellationToken);

                nftImageLayer.NftImageLayerUri = await _fileStorageService.UploadAsync<Domain.Marketplace.Entities.NftImageLayer>(command.NftImageLayer, FileType.Image, cancellationToken);
            }

            nftImageLayer.AddDomainEvent(new NftImageLayerUpdatedEvent(nftImageLayer, string.Format(_localizer["NFT Image Layer '{0}' updated."]!, nftImageLayer.Name)));
            _context.NftImageLayers.Update(nftImageLayer);
            await _context.SaveChangesAsync(cancellationToken);
            await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Domain.Marketplace.Entities.NftImageLayer>(command.Id), cancellationToken);
            return await Result<Guid>.SuccessAsync(nftImageLayer.Id, string.Format(_localizer["NFT Image Layer '{0}' updated."], nftImageLayer.Name));
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveNftImageLayerCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            // TODO - учитывать DID, чтобы можно было удалять только свои записи?
            var nftImageLayer = await _context.NftImageLayers.FirstOrDefaultAsync(x => x.Id.Equals(command.Id), cancellationToken);
            if (nftImageLayer == null)
            {
                throw new EntityNotFoundException<Guid, Domain.Marketplace.Entities.NftImageLayer>(command.Id, _localizer);
            }

            if (nftImageLayer.IsReadOnly && !_currentUserService.IsInRole(RoleConstants.SuperAdmin))
            {
                throw new MarketplaceException(_localizer["Deletion Not Allowed Because This NFT Image Layer Is Read Only"], statusCode: HttpStatusCode.BadRequest);
            }

            if (nftImageLayer.IsSoftDeleted())
            {
                throw new EntityIsDeletedException<Guid, Domain.Marketplace.Entities.NftImageLayer>(_localizer);
            }

            // TODO - добавить проверку, когда добавится сущность изображений NFT
            /*bool isNftImageLayerUsed = await _context.NftImageLayers.Include(x => x.Images)
                .AsNoTracking()
                .AnyAsync(x => x.Images.Id == command.Id, cancellationToken);
            if (isNftImageLayerUsed)
            {
                throw new MarketplaceException(_localizer["NFT Image Layer Used In NFT Image Layer"], statusCode: HttpStatusCode.BadRequest);
            }*/

            await _fileStorageService.DeleteAsync(nftImageLayer.NftImageLayerUri, cancellationToken);

            _context.NftImageLayers.Remove(nftImageLayer);

            nftImageLayer.AddDomainEvent(new NftImageLayerRemovedEvent(command.Id, nftImageLayer.Version, string.Format(_localizer["NFT Image Layer '{0}' deleted."], nftImageLayer.Name)));
            await _context.SaveChangesAsync(cancellationToken);
            await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Domain.Marketplace.Entities.NftImageLayer>(command.Id), cancellationToken);
            return await Result<Guid>.SuccessAsync(nftImageLayer.Id, string.Format(_localizer["NFT Image Layer '{0}' deleted."], nftImageLayer.Name));
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<int>> Handle(ImportNftImageLayersCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            await using var stream = new MemoryStream(command.Data, false);
            var mappers = IImportable<Guid, Domain.Marketplace.Entities.NftImageLayer>.DefaultImportMappers(_nftImageLayerLocalizer);
            if ((command.Properties?.Where(p => p.IsPresent()) ?? Array.Empty<string>()).Any())
            {
                mappers = IImportable<Guid, Domain.Marketplace.Entities.NftImageLayer>.ImportMappers(command.Properties, _localizer);
            }

            var importRequest = _mapper.Map<ImportRequest<Guid, Domain.Marketplace.Entities.NftImageLayer>>(command);
            importRequest.DataStream = stream;
            importRequest.Mappers = mappers;
            importRequest.SheetName = _localizer["NftImageLayers"];

            var result = await _excelService.ImportAsync(importRequest);

            if (result.Succeeded)
            {
                var importedNftImageLayers = result.Data;
                var errors = new List<string>();
                bool errorsOccurred = false;
                foreach (var nftImageLayer in importedNftImageLayers)
                {
                    if (errorsOccurred)
                    {
                        break;
                    }

                    var addResult = await _mediator.Send(_mapper.Map<AddNftImageLayerCommand>(nftImageLayer), cancellationToken);
                    if (!addResult.Succeeded)
                    {
                        errorsOccurred = true;
                        errors.AddRange(addResult.Messages.Select(e => $"{(nftImageLayer.Name.IsPresent() ? $"{nftImageLayer.Name} - " : string.Empty)}{e}"));
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