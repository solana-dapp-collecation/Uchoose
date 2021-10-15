// ------------------------------------------------------------------------------------------------------
// <copyright file="IEvent.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using MediatR;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Enums;

namespace Uchoose.Utils.Contracts.Events
{
    /// <summary>
    /// Событие.
    /// </summary>
    public interface IEvent :
        IMessage,
        INotification
    {
        /// <summary>
        /// Описание события.
        /// </summary>
        public string EventDescription { get; }

        /// <summary>
        /// Тип события.
        /// </summary>
        public EventType EventType { get; }
    }
}