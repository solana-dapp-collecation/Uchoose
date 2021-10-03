// ------------------------------------------------------------------------------------------------------
// <copyright file="MarketplaceException.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;

using Uchoose.Domain.Exceptions;

namespace Uchoose.Domain.Marketplace.Exceptions
{
    /// <summary>
    /// Исключение, связанное с аккредитациями.
    /// </summary>
    public class MarketplaceException :
        DomainException
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="MarketplaceException"/>.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="errors">Список ошибок.</param>
        /// <param name="statusCode">Код состояния http.</param>
        public MarketplaceException(string message, List<string> errors = default, HttpStatusCode statusCode = default)
            : base(message, errors, statusCode)
        {
        }
    }
}