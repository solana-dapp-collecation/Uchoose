// ------------------------------------------------------------------------------------------------------
// <copyright file="IBusinessRule.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Domain.Contracts
{
    /// <summary>
    /// Бизнес-правило.
    /// </summary>
    public interface IBusinessRule
    {
        /// <summary>
        /// Сообщение.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Не выполняется.
        /// </summary>
        /// <returns>Возвращает true, если бизнес-правило не выполняется. Иначе - false.</returns>
        bool IsBroken();
    }
}