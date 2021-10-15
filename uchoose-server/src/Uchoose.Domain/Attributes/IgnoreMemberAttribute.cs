// ------------------------------------------------------------------------------------------------------
// <copyright file="IgnoreMemberAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Domain.Contracts;

namespace Uchoose.Domain.Attributes
{
    /// <summary>
    /// Атрибут, указывающий, что помеченное им свойство или поле у <see cref="IValueObject"/> не должно учитываться при сравнении.
    /// </summary>
    /// <remarks>
    /// Используется при получении списка свойств или полей для <see cref="IValueObject"/>.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }
}