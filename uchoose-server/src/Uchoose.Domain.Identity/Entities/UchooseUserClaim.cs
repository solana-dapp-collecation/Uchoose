// ------------------------------------------------------------------------------------------------------
// <copyright file="UchooseUserClaim.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;
using Uchoose.Domain.Contracts;
using Uchoose.Utils.Attributes.Exporting;
using Uchoose.Utils.Attributes.Importing;
using Uchoose.Utils.Attributes.Searching;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.Domain.Identity.Entities
{
    /// <summary>
    /// Разрешение пользователя.
    /// </summary>
    public class UchooseUserClaim :
        IdentityUserClaim<Guid>,
        IAuditableEntity<int>,
        IDomainEntity,
        IHasDescription
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UchooseUserClaim"/>.
        /// </summary>
        /// <remarks>
        /// Свойство Id инициализируется новым значением GUID.
        /// </remarks>
        public UchooseUserClaim()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UchooseUserClaim"/>.
        /// </summary>
        /// <remarks>
        /// Свойство Id инициализируется новым значением GUID.
        /// </remarks>
        /// <param name="roleClaimDescription">Описание.</param>
        /// <param name="roleClaimGroup">Группа.</param>
        public UchooseUserClaim(string roleClaimDescription = null, string roleClaimGroup = null)
        {
            Description = roleClaimDescription;
            Group = roleClaimGroup;
        }

        /// <inheritdoc/>
        public Guid CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public Guid LastModifiedBy { get; set; }

        /// <inheritdoc/>
        public DateTime? LastModifiedOn { get; set; }

        /// <summary>
        /// Описание разрешения.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Группа разрешений.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Пользователь, которому принадлежит разрешение.
        /// </summary>
        public virtual UchooseUser User { get; set; }

#pragma warning disable SA1201 // Elements should appear in the correct order
        private List<IDomainEvent> _domainEvents;
#pragma warning restore SA1201 // Elements should appear in the correct order

        /// <inheritdoc/>
        [NotExportable]
        [NotImportable]
        [NotSearchable]
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        /// <inheritdoc/>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new();
            _domainEvents.Add(domainEvent);
        }

        /// <inheritdoc/>
        public void RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents?.Remove(domainEvent);

        /// <inheritdoc/>
        public void ClearDomainEvents() => _domainEvents?.Clear();
    }
}