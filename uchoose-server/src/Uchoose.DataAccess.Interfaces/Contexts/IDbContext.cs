// ------------------------------------------------------------------------------------------------------
// <copyright file="IDbContext.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Data;

using Uchoose.DataAccess.Interfaces.Contracts;

namespace Uchoose.DataAccess.Interfaces.Contexts
{
    /// <summary>
    /// Контекст доступа к данным.
    /// </summary>
    public interface IDbContext :
        ISupportsSavingChanges
    {
        /// <summary>
        /// Соединение с базой данных для текущего контекста доступа к данным.
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// Есть ли изменения в контексте доступа к данным.
        /// </summary>
        bool HasChanges { get; }
    }
}