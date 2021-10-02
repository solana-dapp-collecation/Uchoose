// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Text.Json.Serialization;

using Uchoose.Domain.Contracts;
using Uchoose.Domain.Enums;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.Domain.Abstractions
{
    /// <inheritdoc cref="IExtendedAttribute{TEntityId}"/>
    /// <typeparam name="TEntityId">Тип идентификатора сущности, которой принадлежит расширенный атрибут.</typeparam>
    /// <typeparam name="TEntity">Тип сущности, которой принадлежит расширенный атрибут.</typeparam>
    public abstract class ExtendedAttribute<TEntityId, TEntity> :
        AuditableDomainEntity,
        IExtendedAttribute<TEntityId>,
        IHasDescription<string?>
        where TEntity : class, IEntity<TEntityId>
    {
        /// <inheritdoc/>
#pragma warning disable 8618
        public TEntityId EntityId { get; set; }
#pragma warning restore 8618

        /// <summary>
        /// Сущность, которой принадлежит расширенный атрибут.
        /// </summary>
        [JsonIgnore] // для System.Text.Json
#pragma warning disable 8618
        public virtual TEntity Entity { get; set; }
#pragma warning restore 8618

        /// <inheritdoc/>
        public ExtendedAttributeType Type { get; set; }

        /// <inheritdoc/>
#pragma warning disable 8618
        public string Key { get; set; }
#pragma warning restore 8618

        /// <inheritdoc/>
        public decimal? Decimal { get; set; }

        /// <inheritdoc/>
        public string? Text { get; set; }

        /// <inheritdoc/>
        public DateTime? DateTime { get; set; }

        /// <inheritdoc/>
        public string? Json { get; set; }

        /// <inheritdoc/>
        public bool? Boolean { get; set; }

        /// <inheritdoc/>
        public int? Integer { get; set; }

        /// <inheritdoc/>
        public string? ExternalId { get; set; }

        /// <inheritdoc/>
        public string? Group { get; set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Description"/>
        public string? Description { get; set; }

        /// <inheritdoc/>
        public bool IsActive { get; set; }
    }
}