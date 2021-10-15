// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerTypeEventHandler.cs" company="Life Loop">
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
using Uchoose.Domain.Marketplace.Events.NftImageLayerType;

namespace Uchoose.UseCases.Common.Features.Marketplace.NftImageLayerType.EventHandlers
{
    /// <summary>
    /// Обработчик событий для типов слоёв изображений NFT.
    /// </summary>
    public class NftImageLayerTypeEventHandler :
        INotificationHandler<NftImageLayerTypeAddedEvent>,
        INotificationHandler<NftImageLayerTypeUpdatedEvent>,
        INotificationHandler<NftImageLayerTypeRemovedEvent>
    {
        private readonly ILogger<NftImageLayerTypeEventHandler> _logger;
        private readonly IStringLocalizer<NftImageLayerTypeEventHandler> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <seealso cref="NftImageLayerTypeEventHandler"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public NftImageLayerTypeEventHandler(
            ILogger<NftImageLayerTypeEventHandler> logger,
            IStringLocalizer<NftImageLayerTypeEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(NftImageLayerTypeAddedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(NftImageLayerTypeAddedEvent)} Raised."]);

            // TODO: добавить логику
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(NftImageLayerTypeUpdatedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(NftImageLayerTypeUpdatedEvent)} Raised."]);

            // TODO: добавить логику
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(NftImageLayerTypeRemovedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(NftImageLayerTypeRemovedEvent)} Raised. {notification.Id} Removed."]);

            // TODO: добавить логику
            return Task.CompletedTask;
        }
    }
}