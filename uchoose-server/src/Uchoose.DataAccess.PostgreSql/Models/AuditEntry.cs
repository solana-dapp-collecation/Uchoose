// ------------------------------------------------------------------------------------------------------
// <copyright file="AuditEntry.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Uchoose.DataAccess.Interfaces.Enums;
using Uchoose.DataAccess.Interfaces.Models;
using Uchoose.Domain.Entities;
using Uchoose.SerializationService.Interfaces;
using Uchoose.Utils.Extensions;

namespace Uchoose.DataAccess.PostgreSql.Models
{
    /// <inheritdoc cref="IAuditEntry"/>
    internal class AuditEntry :
        IAuditEntry
    {
        private readonly IJsonSerializer _jsonSerializer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="AuditEntry"/>.
        /// </summary>
        /// <param name="entry"><see cref="EntityEntry"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        public AuditEntry(EntityEntry entry, IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
            Entry = entry;
            EntityName = entry.Entity.GetType().GetGenericTypeName();
        }

        /// <inheritdoc/>
        public EntityEntry Entry { get; }

        /// <inheritdoc/>
        public Guid UserId { get; set; }

        /// <inheritdoc/>
        public string EntityName { get; }

        /// <inheritdoc/>
        public Dictionary<string, object> KeyValues { get; } = new();

        /// <inheritdoc/>
        public Dictionary<string, object> OldValues { get; } = new();

        /// <inheritdoc/>
        public Dictionary<string, object> NewValues { get; } = new();

        /// <inheritdoc/>
        public List<PropertyEntry> TemporaryProperties { get; } = new();

        /// <inheritdoc/>
        public AuditType AuditType { get; set; }

        /// <inheritdoc/>
        public List<string> ChangedColumns { get; } = new();

        /// <inheritdoc/>
        public bool HasTemporaryProperties => TemporaryProperties.Count > 0;

        /// <inheritdoc/>
        public Audit ToAudit()
        {
            return new()
            {
                UserId = UserId,
                Type = AuditType.ToString(),
                EntityName = Entry.Entity.GetType().GetGenericTypeName(),
                DateTime = DateTime.UtcNow,
                PrimaryKey = _jsonSerializer.Serialize(KeyValues),
                OldValues = OldValues.Count == 0 ? null : _jsonSerializer.Serialize(OldValues),
                NewValues = NewValues.Count == 0 ? null : _jsonSerializer.Serialize(NewValues),
                AffectedColumns = ChangedColumns.Count == 0 ? null : _jsonSerializer.Serialize(ChangedColumns)
            };
        }
    }
}