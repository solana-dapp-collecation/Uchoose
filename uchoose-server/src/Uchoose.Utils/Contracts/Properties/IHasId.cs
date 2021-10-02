// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasId.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.Utils.Contracts.Properties
{
    /// <inheritdoc cref="IHasId{TId}"/>
    public interface IHasId :
        IHasId<Guid>
    {
    }

    /// <summary>
    /// Имеет идентификатор.
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора.</typeparam>
    public interface IHasId<out TId> :
        IHasProperty
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        TId Id { get; }
    }
}