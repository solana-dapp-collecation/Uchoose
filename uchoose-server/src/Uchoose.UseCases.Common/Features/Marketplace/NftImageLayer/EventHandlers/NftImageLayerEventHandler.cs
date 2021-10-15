// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerEventHandler.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Uchoose.Domain.Marketplace.Events.NftImageLayer;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayer.EventHandlers
{
    /// <summary>
    /// Обработчик событий для слоёв изображений NFT.
    /// </summary>
    public class NftImageLayerEventHandler :
        INotificationHandler<NftImageLayerAddedEvent>,
        INotificationHandler<NftImageLayerUpdatedEvent>,
        INotificationHandler<NftImageLayerRemovedEvent>
    {
        private readonly ILogger<NftImageLayerEventHandler> _logger;
        private readonly IStringLocalizer<NftImageLayerEventHandler> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <seealso cref="NftImageLayerEventHandler"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public NftImageLayerEventHandler(
            ILogger<NftImageLayerEventHandler> logger,
            IStringLocalizer<NftImageLayerEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(NftImageLayerAddedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(NftImageLayerAddedEvent)} Raised."]);

            // TODO: добавить логику
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(NftImageLayerUpdatedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(NftImageLayerUpdatedEvent)} Raised."]);

            // TODO: добавить логику
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(NftImageLayerRemovedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(NftImageLayerRemovedEvent)} Raised. {notification.Id} Removed."]);

            // TODO: добавить логику
            return Task.CompletedTask;
        }
    }
}