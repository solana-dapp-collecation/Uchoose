// ------------------------------------------------------------------------------------------------------
// <copyright file="RemoveNftImageLayerCommand.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using MediatR;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Logging;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.Commands
{
    /// <summary>
    /// Команда для удаления слоя изображения NFT.
    /// </summary>
    public class RemoveNftImageLayerCommand :
        IRequest<Result<Guid>>,
        ILoggable,
        IHasId<Guid>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="RemoveNftImageLayerCommand"/>.
        /// </summary>
        /// <param name="id">Идентификатор слоя изображения NFT.</param>
        public RemoveNftImageLayerCommand(Guid id)
        {
            Id = id;
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        public Guid Id { get; }
    }
}