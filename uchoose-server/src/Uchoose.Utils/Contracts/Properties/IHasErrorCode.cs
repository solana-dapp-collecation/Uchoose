// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasErrorCode.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Utils.Contracts.Properties
{
    /// <inheritdoc cref="IHasErrorCode{TPropertyType}"/>
    public interface IHasErrorCode :
        IHasErrorCode<int>
    {
    }

    /// <summary>
    /// Имеет свойство с именем ErrorCode.
    /// </summary>
    /// <typeparam name="TPropertyType">Тип свойства.</typeparam>
    public interface IHasErrorCode<TPropertyType> :
        IHasProperty
    {
        /// <summary>
        /// Код ошибки.
        /// </summary>
        TPropertyType ErrorCode { get; set; }
    }
}