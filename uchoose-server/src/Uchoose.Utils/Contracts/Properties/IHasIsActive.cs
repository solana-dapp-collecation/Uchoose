// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasIsActive.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Utils.Contracts.Properties
{
    /// <inheritdoc cref="IHasIsActive{TPropertyType}"/>
    public interface IHasIsActive :
        IHasIsActive<bool>
    {
    }

    /// <summary>
    /// Имеет свойство с именем IsActive.
    /// </summary>
    /// <typeparam name="TPropertyType">Тип свойства.</typeparam>
    public interface IHasIsActive<out TPropertyType> :
        IHasProperty
    {
        /// <summary>
        /// Является активной.
        /// </summary>
        TPropertyType IsActive { get; }
    }
}