// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasValue.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Utils.Contracts.Properties
{
    /// <inheritdoc cref="IHasValue{TPropertyType}"/>
    public interface IHasValue :
        IHasValue<string>
    {
    }

    /// <summary>
    /// Имеет свойство с именем Value.
    /// </summary>
    /// <typeparam name="TPropertyType">Тип свойства.</typeparam>
    public interface IHasValue<TPropertyType> :
        IHasProperty
    {
        /// <summary>
        /// Значение.
        /// </summary>
        TPropertyType Value { get; set; }
    }
}