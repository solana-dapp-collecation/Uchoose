// ------------------------------------------------------------------------------------------------------
// <copyright file="UchooseUserExtendedAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Domain.Abstractions;

namespace Uchoose.Domain.Identity.Entities.ExtendedAttributes
{
    /// <summary>
    /// Расширенный атрибут пользователя.
    /// </summary>
    public class UchooseUserExtendedAttribute :
        ExtendedAttribute<Guid, UchooseUser>
    {
        /// <inheritdoc/>
        public override Guid GenerateNewId()
        {
            return Guid.NewGuid();
        }
    }
}