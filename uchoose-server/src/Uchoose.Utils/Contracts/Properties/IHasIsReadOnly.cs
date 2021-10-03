// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasIsReadOnly.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Utils.Contracts.Properties
{
    /// <inheritdoc cref="IHasIsReadOnly{TPropertyType}"/>
    public interface IHasIsReadOnly :
        IHasIsReadOnly<bool>
    {
    }

    /// <summary>
    /// Имеет свойство с именем IsReadOnly.
    /// </summary>
    /// <typeparam name="TPropertyType">Тип свойства.</typeparam>
    public interface IHasIsReadOnly<out TPropertyType> :
        IHasProperty
    {
        /// <summary>
        /// Только для чтения.
        /// </summary>
        TPropertyType IsReadOnly { get; }
    }
}