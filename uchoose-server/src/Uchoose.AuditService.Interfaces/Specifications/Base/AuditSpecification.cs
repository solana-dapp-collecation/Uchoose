// ------------------------------------------------------------------------------------------------------
// <copyright file="AuditSpecification.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Domain.Entities;
using Uchoose.Utils.Specifications;

namespace Uchoose.AuditService.Interfaces.Specifications.Base
{
    /// <summary>
    /// Спецификация для <see cref="Audit"/>.
    /// </summary>
    public abstract class AuditSpecification :
        UchooseSpecification<Audit>
    {
    }
}