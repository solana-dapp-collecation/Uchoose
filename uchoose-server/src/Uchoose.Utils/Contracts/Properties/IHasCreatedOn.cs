// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasCreatedOn.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.Utils.Contracts.Properties
{
    /// <inheritdoc cref="IHasCreatedOn{TPropertyType}"/>
    public interface IHasCreatedOn :
        IHasCreatedOn<DateTime>
    {
    }

    /// <summary>
    /// Имеет свойство с именем CreatedOn.
    /// </summary>
    /// <typeparam name="TPropertyType">Тип свойства.</typeparam>
    public interface IHasCreatedOn<TPropertyType> :
        IHasProperty
    {
        /// <summary>
        /// Дата создания.
        /// </summary>
        TPropertyType CreatedOn { get; set; }
    }
}