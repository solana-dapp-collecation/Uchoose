// ------------------------------------------------------------------------------------------------------
// <copyright file="NftImageLayer.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

using Microsoft.Extensions.Localization;
using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Contracts;
using Uchoose.Domain.Marketplace.Events.NftImageLayer;
using Uchoose.Utils.Attributes.Exporting;
using Uchoose.Utils.Attributes.Importing;
using Uchoose.Utils.Attributes.Ordering;
using Uchoose.Utils.Attributes.Searching;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Importing;
using Uchoose.Utils.Contracts.Properties;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Extensions;

namespace Uchoose.Domain.Marketplace.Entities
{
    /// <summary>
    /// Слой изображения NFT.
    /// </summary>
    public class NftImageLayer :
        AuditableAggregate,
        IImportable<Guid, NftImageLayer>,
        IExportable<Guid, NftImageLayer>,
        ISearchable<Guid, NftImageLayer>,
        IVersionableByEvent<Guid, NftImageLayer, NftImageLayerAddedEvent>,
        IVersionableByEvent<Guid, NftImageLayer, NftImageLayerUpdatedEvent>,
        IVersionableByEvent<Guid, NftImageLayer, NftImageLayerRemovedEvent>,
        IHasName,
        IHasIsActive,
        IHasIsReadOnly
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="NftImageLayer"/>.
        /// </summary>
        /// <remarks>
        /// Свойство Id инициализируется новым значением GUID.
        /// </remarks>
#pragma warning disable 8618
        public NftImageLayer()
#pragma warning restore 8618
        {
            // NftImages = new HashSet<NftImage>();
        }

        /// <summary>
        /// Наименование зоны доступа.
        /// </summary>
        [ExportDefaultOrder(1)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор типа слоя изображения.
        /// </summary>
        [ExportDefaultOrder(2)]
        [Display(Name = "Type")]
        public Guid TypeId { get; set; }

        /// <summary>
        /// Тип слоя изображения.
        /// </summary>
        [NotExportable]
        [NotImportable]
        [NotSearchable]
        public virtual NftImageLayerType Type { get; set; }

        /// <summary>
        /// Путь к слою изображения NFT.
        /// </summary>
        /// <remarks>
        /// Может принимать идентификатор в файловом хранилище.
        /// </remarks>
        [ExportDefaultOrder(3)]
        [Display(Name = "Uri")]
        public string NftImageLayerUri { get; set; }

        /// <summary>
        /// DID художника, который создал слой изображения NFT.
        /// </summary>
        [ExportDefaultOrder(4)]
        [Display(Name = "Did")]
        public string ArtistDid { get; set; }

        /// <inheritdoc/>
        [ExportDefaultOrder(5)]
        [Display(Name = "Is read only")]
        public bool IsReadOnly { get; set; }

        /// <inheritdoc/>
        [ExportDefaultOrder(6)]
        [Display(Name = "Is active")]
        public bool IsActive { get; set; }

        /*/// <summary>
        /// Коллекция изображений NFT, содержащих текущий слой.
        /// </summary>
        [NotExportable]
        [NotImportable]
        [NotSearchable]
        public virtual ICollection<NftImage> NftImages { get; set; }*/

        #region IImportable

        /// <inheritdoc/>
        public Dictionary<string, Func<DataRow, NftImageLayer, (object? Object, int Order)>> GetDefaultImportMappers(IStringLocalizer localizer) => new()
        {
            { localizer["Name"]!, (row, item) => (item.Name = row[localizer["Name"]!].ToString() ?? string.Empty, item.GetImportExportOrderAttributeValue(nameof(Name))) },
            { localizer["TypeId"]!, (row, item) => (item.TypeId = Guid.TryParse(row[localizer["TypeId"]!].ToString(), out var typeid) ? typeid : Guid.Empty, item.GetImportExportOrderAttributeValue(nameof(TypeId))) },
            { localizer["NftImageLayerUri"]!, (row, item) => (item.NftImageLayerUri = row[localizer["NftImageLayerUri"]!].ToString() ?? string.Empty, item.GetImportExportOrderAttributeValue(nameof(NftImageLayerUri))) },
            { localizer["ArtistDid"]!, (row, item) => (item.ArtistDid = row[localizer["ArtistDid"]!].ToString() ?? string.Empty, item.GetImportExportOrderAttributeValue(nameof(ArtistDid))) },
            { localizer["IsReadOnly"]!, (row, item) => (item.IsReadOnly = bool.TryParse(row[localizer["IsReadOnly"]!].ToString(), out bool isReadOnly) && isReadOnly, item.GetImportExportOrderAttributeValue(nameof(IsReadOnly))) },
            { localizer["IsActive"]!, (row, item) => (item.IsActive = bool.TryParse(row[localizer["IsActive"]!].ToString(), out bool isActive) && isActive, item.GetImportExportOrderAttributeValue(nameof(IsActive))) }
        };

        #endregion IImportable

        #region IExportable

        /// <inheritdoc/>
        public Dictionary<string, Func<NftImageLayer, (object? Object, int Order)>> GetDefaultExportMappers(IStringLocalizer localizer)
        {
            return new()
            {
                { localizer["Id"]!, item => (item.Id, item.GetImportExportOrderAttributeValue(nameof(Id))) },
                { localizer["Name"]!, item => (item.Name, item.GetImportExportOrderAttributeValue(nameof(Name))) },
                { localizer["TypeId"]!, item => (item.TypeId, item.GetImportExportOrderAttributeValue(nameof(TypeId))) },
                { localizer["NftImageLayerUri"]!, item => (item.NftImageLayerUri, item.GetImportExportOrderAttributeValue(nameof(NftImageLayerUri))) },
                { localizer["ArtistDid"]!, item => (item.ArtistDid, item.GetImportExportOrderAttributeValue(nameof(ArtistDid))) },
                { localizer["IsReadOnly"]!, item => (item.IsReadOnly, item.GetImportExportOrderAttributeValue(nameof(IsReadOnly))) },
                { localizer["IsActive"]!, item => (item.IsActive, item.GetImportExportOrderAttributeValue(nameof(IsActive))) }
            };
        }

        #endregion IExportable

        #region IVersionableByEvent

        /// <inheritdoc/>
        public void When(NftImageLayerAddedEvent @event)
        {
            Name = @event.Name;
            TypeId = @event.TypeId;
            NftImageLayerUri = @event.NftImageLayerUri;
            ArtistDid = @event.ArtistDid;
            IsReadOnly = @event.IsReadOnly;
            IsActive = @event.IsActive;
            IncrementVersion();
        }

        /// <inheritdoc/>
        public void When(NftImageLayerUpdatedEvent @event)
        {
            Name = @event.Name;
            TypeId = @event.TypeId;
            NftImageLayerUri = @event.NftImageLayerUri;
            ArtistDid = @event.ArtistDid;
            IsReadOnly = @event.IsReadOnly;
            IsActive = @event.IsActive;
            IncrementVersion();
        }

        /// <inheritdoc/>
        public void When(NftImageLayerRemovedEvent @event)
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