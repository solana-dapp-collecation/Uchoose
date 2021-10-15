// ------------------------------------------------------------------------------------------------------
// <copyright file="IValueObject.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.Domain.Contracts
{
    /// <summary>
    /// Объект-значение.
    /// </summary>
    // https://habr.com/ru/post/275599/
    public interface IValueObject
    {
    }

    /// <inheritdoc cref="IValueObject"/>
    /// <typeparam name="TValueObject">Тип объекта-значения.</typeparam>
    public interface IValueObject<TValueObject> :
        IValueObject,
        IEquatable<TValueObject>
    {
    }

    /// <inheritdoc cref="IValueObject{TValueObject}"/>
    /// <typeparam name="TValueObject">Тип объекта-значения.</typeparam>
    /// <typeparam name="TValue">Тип значения объекта-значения.</typeparam>
    public interface IValueObject<TValueObject, out TValue> :
        IValueObject<TValueObject>
    {
        /// <summary>
        /// Значение объекта-значения.
        /// </summary>
        TValue Value { get; }
    }
}