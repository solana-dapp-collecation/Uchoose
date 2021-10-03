// ------------------------------------------------------------------------------------------------------
// <copyright file="UpdateExtendedAttributeCommand.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;

using MediatR;
using Uchoose.Domain.Contracts;
using Uchoose.Domain.Enums;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Wrapper;

namespace Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Commands
{
    /// <summary>
    /// Команда для обновления расширенного атрибута сущности.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    // ReSharper disable once UnusedTypeParameter
    public class UpdateExtendedAttributeCommand<TEntityId, TEntity> :
        IRequest<Result<Guid>>,
        IHasDescription<string?>,
        IHasIsActive
            where TEntity : class, IEntity<TEntityId>
    {
        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public Guid Id { get; set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.EntityId"/>
#pragma warning disable 8618
        public TEntityId EntityId { get; set; }
#pragma warning restore 8618

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Type"/>
        public ExtendedAttributeType Type { get; set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Key"/>
        /// <example>Example</example>
#pragma warning disable 8618
        public string Key { get; set; }
#pragma warning restore 8618

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Decimal"/>
        /// <example>null</example>
        public decimal? Decimal { get; set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Text"/>
        /// <example>Example</example>
        public string? Text { get; set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.DateTime"/>
        /// <example>null</example>
        public DateTime? DateTime { get; set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Json"/>
        /// <example>null</example>
        public string? Json { get; set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Boolean"/>
        /// <example>null</example>
        public bool? Boolean { get; set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Integer"/>
        /// <example>null</example>
        public int? Integer { get; set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.ExternalId"/>
        /// <example>Example</example>
        public string? ExternalId { get; set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Group"/>
        /// <example>Example</example>
        public string? Group { get; set; }

        /// <inheritdoc cref="IExtendedAttribute{TEntityId}.Description"/>
        /// <example>Example</example>
        public string? Description { get; set; }

        /// <inheritdoc cref="IHasIsActive{TProperty}.IsActive"/>
        /// <example>true</example>
        public bool IsActive { get; set; } = true;
    }
}