// ------------------------------------------------------------------------------------------------------
// <copyright file="AuditType.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.DataAccess.Interfaces.Enums
{
    /// <summary>
    /// Тип аудита.
    /// </summary>
    public enum AuditType : byte
    {
        /// <summary>
        /// Нет.
        /// </summary>
        None = 0,

        /// <summary>
        /// Создание.
        /// </summary>
        Create = 1,

        /// <summary>
        /// Обновление.
        /// </summary>
        Update = 2,

        /// <summary>
        /// Удаление.
        /// </summary>
        Delete = 3
    }
}