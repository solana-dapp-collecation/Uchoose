// ------------------------------------------------------------------------------------------------------
// <copyright file="IAggregateRoot.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Domain.Contracts
{
    /// <inheritdoc cref="IAggregateRoot"/>
    /// <typeparam name="TAggregateRootId">Тип идентификатора корня агрегации.</typeparam>
    public interface IAggregateRoot<out TAggregateRootId> :
        IAggregate<TAggregateRootId>
    {
    }

    /// <summary>
    /// Корень агрегации.
    /// </summary>
    public interface IAggregateRoot
        : IAggregate
    {
    }
}