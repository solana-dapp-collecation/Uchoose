// ------------------------------------------------------------------------------------------------------
// <copyright file="Message.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Extensions;

namespace Uchoose.Utils.Abstractions.Common
{
    /// <summary>
    /// Сообщение.
    /// </summary>
    public abstract class Message :
        IMessage
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса, наследующего <see cref="Message"/>.
        /// </summary>
        /// <remarks>
        /// Свойство MessageType инициализируется именем типа класса, наследующего этот класс.
        /// </remarks>
        /// <param name="aggregateId">Идентификатор агрегата.</param>
        /// <param name="messageType">Тип сообщения.</param>
        protected Message(Guid aggregateId, string messageType = null)
        {
            MessageType = messageType ?? GetType().GetGenericTypeName();
            AggregateId = aggregateId == Guid.Empty ? Guid.NewGuid() : aggregateId;
        }

        /// <inheritdoc cref="IMessage.MessageType"/>
        [Display(Name = "Message type")]
        [JsonInclude]
        public string MessageType { get; private set; }

        /// <inheritdoc cref="IMessage.AggregateId"/>
        [Display(Name = "Aggregate identifier")]
        [JsonInclude]
        public Guid AggregateId { get; private set; }

        /// <summary>
        /// Установить тип сообщения.
        /// </summary>
        /// <param name="messageType">Тип сообщения.</param>
        protected virtual void SetMessageType(string messageType = null)
        {
            MessageType = messageType ?? GetType().GetGenericTypeName();
        }
    }
}