// ------------------------------------------------------------------------------------------------------
// <copyright file="ImportNftImageLayersCommand.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using AutoMapper;
using MediatR;
using Uchoose.ExcelService.Interfaces.Requests;
using Uchoose.UseCases.Common.Features.Common.Commands;
using Uchoose.Utils.Contracts.Logging;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Commands
{
    /// <summary>
    /// Команда для импорта слоёв изображений NFT из файла.
    /// </summary>
    // [UseReverseMap(typeof(ImportRequest<Guid, Domain.Marketplace.Entities.NftImageLayer>))]
    public sealed class ImportNftImageLayersCommand :
        ImportEntitiesCommand,
        IRequest<Result<int>>,
        IMapFromTo<ImportRequest<Guid, Domain.Marketplace.Entities.NftImageLayer>, ImportNftImageLayersCommand>,
        ILoggable
    {
        /// <inheritdoc/>
        void IMapFromTo<ImportRequest<Guid, Domain.Marketplace.Entities.NftImageLayer>, ImportNftImageLayersCommand>.Mapping(Profile profile, bool useReverseMap)
        {
            profile.CreateMap<ImportNftImageLayersCommand, ImportRequest<Guid, Domain.Marketplace.Entities.NftImageLayer>>()
                .ForMember(dest => dest.CheckProperties, source => source.MapFrom(c => c.Properties != null && c.Properties.Count > 0));
        }
    }
}