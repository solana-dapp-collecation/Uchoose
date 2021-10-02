// ------------------------------------------------------------------------------------------------------
// <copyright file="ISupportsSavingChanges.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

namespace Uchoose.DataAccess.Interfaces.Contracts
{
    /// <summary>
    /// Поддерживает сохранение изменений.
    /// </summary>
    public interface ISupportsSavingChanges
    {
        /// <summary>
        /// Сохранить изменения асинхронно.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Возвращает количество затронутых записей в базе данных.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Сохранить изменения синхронно.
        /// </summary>
        /// <returns>Возвращает количество затронутых записей в базе данных.</returns>
        int SaveChanges();
    }
}