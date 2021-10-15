// ------------------------------------------------------------------------------------------------------
// <copyright file="Audit.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Microsoft.Extensions.Localization;
using Uchoose.Domain.Contracts;
using Uchoose.Utils.Attributes.Importing;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Searching;

namespace Uchoose.Domain.Entities
{
    /// <inheritdoc cref="IAudit"/>
    [NotImportable]
    public class Audit :
        IAudit<Guid>,
        IEntity<Guid>,
        IExportable<Guid, Audit>,
        ISearchable<Guid, Audit>
    {
        /// <inheritdoc cref="IAudit{TEntityId}.Id" />
        [Display(Name = "Database entry identifier")]
        public Guid Id { get; set; }

        /// <inheritdoc/>
        [Display(Name = "The identifier of the user who caused the change")]
        public Guid UserId { get; set; }

        /// <inheritdoc/>
        [Display(Name = "Type of changes")]
        public string Type { get; set; }

        /// <inheritdoc/>
        [Display(Name = "The name of the entity in which the changes occurred")]
        public string EntityName { get; set; }

        /// <inheritdoc/>
        [Display(Name = "Date of changes")]
        public DateTime DateTime { get; set; }

        /// <inheritdoc/>
        [Display(Name = "Old Values")]
        public string OldValues { get; set; }

        /// <inheritdoc/>
        [Display(Name = "New Values")]
        public string NewValues { get; set; }

        /// <inheritdoc/>
        [Display(Name = "Affected Columns")]
        public string AffectedColumns { get; set; }

        /// <inheritdoc/>
        [Display(Name = "Primary Key")]
        public string PrimaryKey { get; set; }

        /// <inheritdoc/>
        public Dictionary<string, Func<Audit, (object Object, int Order)>> GetDefaultExportMappers(IStringLocalizer localizer)
        {
            return new()
            {
                { localizer["Id"], item => (item.Id, 0) },
                { localizer["Entity Name"], item => (item.EntityName, 1) },
                { localizer["Type"], item => (item.Type, 2) },
                { localizer["Date Time (Local)"], item => (DateTime.SpecifyKind(item.DateTime, DateTimeKind.Utc).ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture), 3) },
                { localizer["Date Time (UTC)"], item => (item.DateTime.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture), 4) },
                { localizer["Primary Key"], item => (item.PrimaryKey, 5) },
                { localizer["Old Values"], item => (item.OldValues, 6) },
                { localizer["New Values"], item => (item.NewValues, 7) },
                { localizer["Affected Columns"], item => (item.AffectedColumns, 8) }
            };
        }
    }
}