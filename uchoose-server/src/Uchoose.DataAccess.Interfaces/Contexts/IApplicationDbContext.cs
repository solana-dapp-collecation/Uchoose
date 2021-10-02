// ------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationDbContext.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.DataAccess.Interfaces.Contexts
{
    /// <summary>
    /// Контекст доступа к данным приложения.
    /// </summary>
    public interface IApplicationDbContext :
        ILoggableDbContext
    {
    }
}