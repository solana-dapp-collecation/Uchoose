// ------------------------------------------------------------------------------------------------------
// <copyright file="RemoveNftImageLayerTypeCommand.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using MediatR;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Logging;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.Commands
{
    /// <summary>
    /// Команда для удаления типа слоя изображения NFT.
    /// </summary>
    public class RemoveNftImageLayerTypeCommand :
        IRequest<Result<Guid>>,
        ILoggable,
        IHasId<Guid>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="RemoveNftImageLayerTypeCommand"/>.
        /// </summary>
        /// <param name="id">Идентификатор типа слоя изображения NFT.</param>
        public RemoveNftImageLayerTypeCommand(Guid id)
        {
            Id = id;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        public Guid Id { get; }
    }
}