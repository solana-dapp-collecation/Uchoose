// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasDescription.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Utils.Contracts.Properties
{
    /// <inheritdoc cref="IHasDescription{TPropertyType}"/>
    public interface IHasDescription :
        IHasDescription<string>
    {
    }

    /// <summary>
    /// Имеет свойство с именем Description.
    /// </summary>
    /// <typeparam name="TPropertyType">Тип свойства.</typeparam>
    public interface IHasDescription<TPropertyType> :
        IHasProperty
    {
        /// <summary>
        /// Описание.
        /// </summary>
        TPropertyType Description { get; set; }
    }
}