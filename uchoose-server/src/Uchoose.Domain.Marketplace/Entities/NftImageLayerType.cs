// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayerType.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Collections.Generic;
using System.Data;

using Microsoft.Extensions.Localization;
using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Contracts;
using Uchoose.Domain.Marketplace.Events.NftImageLayerType;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Importing;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Extensions;

namespace Uchoose.Domain.Marketplace.Entities
{
    /// <summary>
    /// Тип слоя изображения NFT.
    /// </summary>
    public class NftImageLayerType :
        AuditableAggregate,
        IImportable<Guid, NftImageLayerType>,
        IExportable<Guid, NftImageLayerType>,
        ISearchable<Guid, NftImageLayerType>,
        IVersionableByEvent<Guid, NftImageLayerType, NftImageLayerTypeAddedEvent>,
        IVersionableByEvent<Guid, NftImageLayerType, NftImageLayerTypeUpdatedEvent>,
        IVersionableByEvent<Guid, NftImageLayerType, NftImageLayerTypeRemovedEvent>,
        IHasName,
        IHasDescription<string?>,
        IHasIsActive,
        IHasIsReadOnly
    {
        /// <summary>
        /// Наименование типа слоя изображения NFT.
        /// </summary>
#pragma warning disable 8618
        public string Name { get; set; }
#pragma warning restore 8618

        /// <summary>
        /// Описание типа слоя изображения NFT.
        /// </summary>
        public string? Description { get; set; }

        /// <inheritdoc/>
        public bool IsReadOnly { get; set; }

        /// <inheritdoc/>
        public bool IsActive { get; set; }

        #region IImportable

        /// <inheritdoc/>
        public Dictionary<string, Func<DataRow, NftImageLayerType, (object? Object, int Order)>> GetDefaultImportMappers(IStringLocalizer localizer) => new()
        {
            { localizer["Name"]!, (row, item) => (item.Name = row[localizer["Name"]!].ToString() ?? string.Empty, item.GetImportExportOrderAttributeValue(nameof(Name))) },
            { localizer["Description"]!, (row, item) => (item.Description = row[localizer["Description"]!].ToString() ?? string.Empty, item.GetImportExportOrderAttributeValue(nameof(Description))) },
            { localizer["IsReadOnly"]!, (row, item) => (item.IsReadOnly = bool.TryParse(row[localizer["IsReadOnly"]!].ToString(), out bool isReadOnly) && isReadOnly, item.GetImportExportOrderAttributeValue(nameof(IsReadOnly))) },
            { localizer["IsActive"]!, (row, item) => (item.IsActive = bool.TryParse(row[localizer["IsActive"]!].ToString(), out bool isActive) && isActive, item.GetImportExportOrderAttributeValue(nameof(IsActive))) }
        };

        #endregion IImportable

        #region IExportable

        /// <inheritdoc/>
        public Dictionary<string, Func<NftImageLayerType, (object? Object, int Order)>> GetDefaultExportMappers(IStringLocalizer localizer)
        {
            return new()
            {
                { localizer["Id"]!, item => (item.Id, item.GetImportExportOrderAttributeValue(nameof(Id))) },
                { localizer["Name"]!, item => (item.Name, item.GetImportExportOrderAttributeValue(nameof(Name))) },
                { localizer["Description"]!, item => (item.Description, item.GetImportExportOrderAttributeValue(nameof(Description))) },
                { localizer["IsReadOnly"]!, item => (item.IsReadOnly, item.GetImportExportOrderAttributeValue(nameof(IsReadOnly))) },
                { localizer["IsActive"]!, item => (item.IsActive, item.GetImportExportOrderAttributeValue(nameof(IsActive))) }
            };
        }

        #endregion IExportable

        #region IVersionableByEvent

        /// <inheritdoc/>
        public void When(NftImageLayerTypeAddedEvent @event)
        {
            Name = @event.Name;
            Description = @event.Description;
            IsReadOnly = @event.IsReadOnly;
            IsActive = @event.IsActive;
            IncrementVersion();
        }

        /// <inheritdoc/>
        public void When(NftImageLayerTypeUpdatedEvent @event)
        {
            Name = @event.Name;
            Description = @event.Description;
            IsReadOnly = @event.IsReadOnly;
            IsActive = @event.IsActive;
            IncrementVersion();
        }

        /// <inheritdoc/>
        public void When(NftImageLayerTypeRemovedEvent @event)
        {
            IncrementVersion();
        }

        #endregion IVersionableByEvent

        /// <inheritdoc/>
        protected override void Apply(IDomainEvent @event)
        {
            When((dynamic)@event);
        }
    }
}