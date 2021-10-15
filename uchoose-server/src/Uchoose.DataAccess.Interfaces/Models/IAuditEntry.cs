// ------------------------------------------------------------------------------------------------------
// <copyright file="IAuditEntry.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Uchoose.DataAccess.Interfaces.Enums;
using Uchoose.Domain.Entities;

namespace Uchoose.DataAccess.Interfaces.Models
{
    /// <summary>
    /// Данные аудита.
    /// </summary>
    public interface IAuditEntry
    {
        /// <summary>
        /// <see cref="EntityEntry"/>.
        /// </summary>
        public EntityEntry Entry { get; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя сущности, в которой произошли изменения.
        /// </summary>
        public string EntityName { get; }

        /// <summary>
        /// Словарь со значениями первичных ключей.
        /// </summary>
        public Dictionary<string, object> KeyValues { get; }

        /// <summary>
        /// Словарь со старыми значениями.
        /// </summary>
        public Dictionary<string, object> OldValues { get; }

        /// <summary>
        /// Словарь с новыми значениями.
        /// </summary>
        public Dictionary<string, object> NewValues { get; }

        /// <summary>
        /// Временные свойства.
        /// </summary>
        public List<PropertyEntry> TemporaryProperties { get; }

        /// <summary>
        /// Тип аудита.
        /// </summary>
        public AuditType AuditType { get; set; }

        /// <summary>
        /// Изменённые столбцы в таблице БД.
        /// </summary>
        public List<string> ChangedColumns { get; }

        /// <summary>
        /// Имеются временные свойства.
        /// </summary>
        public bool HasTemporaryProperties => TemporaryProperties.Count > 0;

        /// <summary>
        /// Преобразовать в <see cref="Audit"/>.
        /// </summary>
        /// <returns>Возвращает <see cref="Audit"/>.</returns>
        public Audit ToAudit();
    }
}