// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasDeletedOn.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uchoose.Utils.Contracts.Properties
{
    /// <inheritdoc cref="IHasDeletedOn{TPropertyType}"/>
    public interface IHasDeletedOn :
        IHasDeletedOn<DateTime?>
    {
    }

    /// <summary>
    /// Имеет свойство с именем DeletedOn.
    /// </summary>
    /// <typeparam name="TPropertyType">Тип свойства.</typeparam>
    public interface IHasDeletedOn<TPropertyType> :
        IHasProperty
    {
        /// <summary>
        /// Дата удаления.
        /// </summary>
        public TPropertyType DeletedOn { get; set; }

        /// <summary>
        /// Был удалён.
        /// </summary>
        [NotMapped]
        public bool IsDeleted => DeletedOn != null;
    }
}