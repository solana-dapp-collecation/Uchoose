// ------------------------------------------------------------------------------------------------------
// <copyright file="UchooseUser.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
    /// Пользователь.
    /// </summary>
    public class UchooseUser :
        IdentityUser<Guid>,
        IAuditableEntity<Guid>,
        IDomainEntity,
        ISoftDelete,
        IHasIsActive,
        ISearchable<Guid, UchooseUser>
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UchooseUser"/>.
        /// </summary>
        /// <remarks>
        /// Свойство Id инициализируется новым значением GUID.
        /// </remarks>
        public UchooseUser()
        {
            UserClaims = new HashSet<UchooseUserClaim>();
            ExtendedAttributes = new HashSet<UchooseUserExtendedAttribute>();
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UchooseUser"/>.
        /// </summary>
        /// <remarks>
        /// Свойство Id инициализируется новым значением GUID.
        /// </remarks>
        /// <param name="userName">UserName пользователя.</param>
        /// <param name="externalId">Внешний идентификатор пользователя (идентификатор контакта в CRM).</param>
        public UchooseUser(string userName, string externalId = null)
            : base(userName)
        {
            ExternalId = externalId;
            UserClaims = new HashSet<UchooseUserClaim>();
            ExtendedAttributes = new HashSet<UchooseUserExtendedAttribute>();
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
        /// Имя.
        /// </summary>
        [PersonalData]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [PersonalData]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        [PersonalData]
        public string MiddleName { get; set; }

        /// <summary>
        /// Url изображения пользователя.
        /// </summary>
        [Column(TypeName = "text")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Пользователь активен.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Значение refresh токена.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Срок действия refresh токена.
        /// </summary>
        public DateTime RefreshTokenExpiryTime { get; set; }

        /// <summary>
        /// Внешний идентификатор пользователя.
        /// </summary>
        /// <remarks>
        /// Идентификатор пользователя в CRM.
        /// </remarks>
        public string ExternalId { get; set; }

        /// <summary>
        /// Коллекция разрешений для пользователя.
        /// </summary>
        public virtual ICollection<UchooseUserClaim> UserClaims { get; set; }

        /// <summary>
        /// Коллекция расширенных атрибутов пользователя.
        /// </summary>
        public virtual ICollection<UchooseUserExtendedAttribute> ExtendedAttributes { get; set; }

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