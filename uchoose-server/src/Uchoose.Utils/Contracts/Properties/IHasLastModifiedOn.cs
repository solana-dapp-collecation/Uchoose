// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasLastModifiedOn.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uchoose.Utils.Contracts.Properties
{
    /// <inheritdoc cref="IHasLastModifiedOn{TPropertyType}"/>
    public interface IHasLastModifiedOn :
        IHasLastModifiedOn<DateTime?>
    {
    }

    /// <summary>
    /// Имеет свойство с именем LastModifiedOn.
    /// </summary>
    /// <typeparam name="TPropertyType">Тип свойства.</typeparam>
    public interface IHasLastModifiedOn<TPropertyType> :
        IHasProperty
    {
        /// <summary>
        /// Дата последнего изменения.
        /// </summary>
        TPropertyType LastModifiedOn { get; set; }

        /// <summary>
        /// Был изменён.
        /// </summary>
        [NotMapped]
        public bool IsModified => LastModifiedOn != null;
    }
}