// ------------------------------------------------------------------------------------------------------
// <copyright file="IMessage.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.Utils.Contracts.Common
{
    /// <summary>
    /// Сообщение.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Тип сообщения.
        /// </summary>
        string MessageType { get; }

        /// <summary>
        /// Идентификатор агрегата.
        /// </summary>
        Guid AggregateId { get; }
    }
}