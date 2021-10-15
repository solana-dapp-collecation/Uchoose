// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasDateRange.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.Utils.Contracts.Properties
{
    /// <inheritdoc cref="IHasDateRange{TPropertyType}"/>
    public interface IHasDateRange :
        IHasDateRange<DateTime?>
    {
    }

    /// <summary>
    /// Имеет свойства с именами StartDateRange и EndDateRange.
    /// </summary>
    /// <typeparam name="TPropertiesType">Тип свойств.</typeparam>
    public interface IHasDateRange<out TPropertiesType> :
        IHasProperty
    {
        /// <summary>
        /// Начало диапазона даты записи данных аудита.
        /// </summary>
        public TPropertiesType StartDateRange { get; }

        /// <summary>
        /// Конец диапазона даты записи данных аудита.
        /// </summary>
        public TPropertiesType EndDateRange { get; }
    }
}