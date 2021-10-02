// ------------------------------------------------------------------------------------------------------
// <copyright file="AuditableAggregate.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

using Uchoose.Domain.Contracts;
using Uchoose.Utils.Attributes.Importing;
using Uchoose.Utils.Attributes.Ordering;
using Uchoose.Utils.Contracts.Deleting;

namespace Uchoose.Domain.Abstractions
{
    /// <summary>
    /// Отслеживаемый агрегат.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора отслеживаемой сущности.</typeparam>
    public abstract class AuditableAggregate<TEntityId> :
        Aggregate<TEntityId>,
        IAuditableEntity<TEntityId>,
        ISoftDelete
    {
        /// <inheritdoc/>
        [ExportDefaultOrder(100)]
        [Display(Name = "The identifier of the user who created the entity")]
        [NotImportable]
        public Guid CreatedBy { get; set; }

        /// <inheritdoc/>
        [ExportDefaultOrder(101)]
        [Display(Name = "The entity creation date")]
        [NotImportable]
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        [ExportDefaultOrder(102)]
        [Display(Name = "The identifier of the user who last modified the entity")]
        [NotImportable]
        public Guid LastModifiedBy { get; set; }

        /// <inheritdoc/>
        [ExportDefaultOrder(103)]
        [Display(Name = "Date of the last modification of the entity")]
        [NotImportable]
        public DateTime? LastModifiedOn { get; set; }

        /// <inheritdoc/>
        [ExportDefaultOrder(104)]
        [Display(Name = "Date of the deletion of the entity")]
        [NotImportable]
        public DateTime? DeletedOn { get; set; }

        /// <inheritdoc/>
        [ExportDefaultOrder(105)]
        [Display(Name = "The identifier of the user who deleted the entity")]
        [NotImportable]
        public Guid? DeletedBy { get; set; }
    }

    /// <inheritdoc/>
    public abstract class AuditableAggregate : AuditableAggregate<Guid>
    {
        /// <inheritdoc/>
        public override Guid GenerateNewId()
        {
            return Guid.NewGuid();
        }
    }
}