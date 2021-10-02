// ------------------------------------------------------------------------------------------------------
// <copyright file="IAudit.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.Domain.Contracts
{
    /// <inheritdoc cref="IAudit"/>
    /// <typeparam name="TEntityId">Тип идентификатора записи в БД.</typeparam>
    public interface IAudit<TEntityId> : IAudit
    {
        /// <summary>
        /// Идентификатор записи в БД.
        /// </summary>
        public TEntityId Id { get; set; }
    }

    /// <summary>
    /// Данные аудита сущности.
    /// </summary>
    public interface IAudit
    {
        /// <summary>
        /// Идентификатор пользователя, вызвавшего изменения.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Тип изменений.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Имя сущности, в которой произошли изменения.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Дата возникновения изменений.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Старые значения.
        /// </summary>
        public string OldValues { get; set; }

        /// <summary>
        /// Новые значения.
        /// </summary>
        public string NewValues { get; set; }

        /// <summary>
        /// Затронутые столбцы в таблице БД.
        /// </summary>
        public string AffectedColumns { get; set; }

        /// <summary>
        /// Первичный ключ таблицы БД.
        /// </summary>
        public string PrimaryKey { get; set; }
    }
}