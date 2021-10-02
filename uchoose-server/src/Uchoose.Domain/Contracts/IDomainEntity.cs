// ------------------------------------------------------------------------------------------------------
// <copyright file="IDomainEntity.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.Domain.Contracts
{
    /// <summary>
    /// Доменная сущность.
    /// </summary>
    public interface IDomainEntity :
        IEntity,
        IGeneratesDomainEvents,
        ISupportsCheckingRules
    {
    }
}