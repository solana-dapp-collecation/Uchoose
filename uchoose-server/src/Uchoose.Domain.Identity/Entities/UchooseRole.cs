// ------------------------------------------------------------------------------------------------------
// <copyright file="UchooseRole.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;
using Uchoose.Domain.Contracts;
using Uchoose.Domain.Identity.Entities.ExtendedAttributes;
using Uchoose.Utils.Attributes.Exporting;
using Uchoose.Utils.Attributes.Importing;
using Uchoose.Utils.Attributes.Searching;
using Uchoose.Utils.Contracts.Deleting;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Contracts.Searching;

// ReSharper disable VirtualMemberCallInConstructor
namespace Uchoose.Domain.Identity.Entities
{
    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public class UchooseRole :
        IdentityRole<Guid>,
        IAuditableEntity<Guid>,
        IDomainEntity,
        ISoftDelete,
        ISearchable<Guid, UchooseRole>,
        IHasName,
        IHasDescription
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UchooseRole"/>.
        /// </summary>
        /// <remarks>
        /// Свойство Id инициализируется новым значением GUID.
        /// </remarks>
        public UchooseRole()
        {
            RoleClaims = new HashSet<UchooseRoleClaim>();
            ExtendedAttributes = new HashSet<UchooseRoleExtendedAttribute>();
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UchooseRole"/>.
        /// </summary>
        /// <remarks>
        /// Свойство Id инициализируется новым значением GUID.
        /// </remarks>
        /// <param name="roleName">Имя.</param>
        /// <param name="roleDescription">Описание.</param>
        public UchooseRole(string roleName, string roleDescription = null)
            : base(roleName)
        {
            Description = roleDescription;
            RoleClaims = new HashSet<UchooseRoleClaim>();
            ExtendedAttributes = new HashSet<UchooseRoleExtendedAttribute>();
        }

        /// <inheritdoc/>
        public Guid CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public Guid LastModifiedBy { get; set; }

        /// <inheritdoc/>
        public DateTime? LastModifiedOn { get; set; }

        /// <inheritdoc/>
        public DateTime? DeletedOn { get; set; }

        /// <inheritdoc/>
        public Guid? DeletedBy { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Коллекция разрешений для роли.
        /// </summary>
        public virtual ICollection<UchooseRoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// Коллекция расширенных атрибутов роли пользователя.
        /// </summary>
        public virtual ICollection<UchooseRoleExtendedAttribute> ExtendedAttributes { get; set; }

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