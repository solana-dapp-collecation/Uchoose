// ------------------------------------------------------------------------------------------------------
// <copyright file="AuditController.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Api.Common.Controllers.Abstractions;
using Uchoose.Api.Common.Controllers.Identity.Abstractions;
using Uchoose.Api.Common.Swagger.Examples.Common.Responses;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Controllers.Audit
{
    /// <summary>
    /// Контроллер для объединения всех действий, связанных с данными аудита.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Журналы аудита.")]
    internal sealed class AuditController :
        BaseController
    {
        private readonly IStringLocalizer<AuditController> _localizer;
        private readonly IStringLocalizer<Domain.Entities.Audit> _auditLocalizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="AuditController"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        /// <param name="auditLocalizer"><see cref="IStringLocalizer{T}"/> для <see cref="Domain.Entities.Audit"/>.</param>
        public AuditController(
            IStringLocalizer<AuditController> localizer,
            IStringLocalizer<Domain.Entities.Audit> auditLocalizer)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            _auditLocalizer = auditLocalizer ?? throw new ArgumentNullException(nameof(auditLocalizer));
        }

        /// <summary>
        /// Получить словарь допустимых названий свойств c их описанием для экспорта данных аудита.
        /// </summary>
        /// <returns>Возвращает словарь допустимых названий свойств c их описанием для экспорта данных аудита.</returns>
        /// <response code="200">Возвращает словарь допустимых названий свойств c их описанием для экспорта данных аудита.</response>
        [MapToApiVersion("1")]
        [HttpGet("audit-trails/export/exportable-properties")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Audit.Export)]
        [SwaggerOperation(
            OperationId = "GetAuditExportableProperties",
            Tags = new[] { AuditTag, IdentityBaseController.IdentityTag })]
        [ProducesResponseType(typeof(Result<Dictionary<string, string>>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EntitySearchableExportableImportablePropertiesResponseExample))]
        public async Task<IActionResult> ExportablePropertiesAsync()
        {
            var properties = IExportable<Guid, Domain.Entities.Audit>.ExportableProperties(_auditLocalizer);
            return Ok(await Result<Dictionary<string, string>>.SuccessAsync(properties, _localizer["Exportable properties are got."]));
        }

        /// <summary>
        /// Получить словарь допустимых названий свойств c их описанием для данных аудита, по которым можно осуществлять поиск.
        /// </summary>
        /// <returns>Возвращает словарь допустимых названий свойств c их описанием для данных аудита, по которым можно осуществлять поиск.</returns>
        /// <response code="200">Возвращает словарь допустимых названий свойств c их описанием для данных аудита, по которым можно осуществлять поиск.</response>
        [MapToApiVersion("1")]
        [HttpGet("audit-trails/search/searchable-properties", Name = "GetAuditSearchableProperties")]
        [Authorize(Policy = Application.Constants.Permission.Permissions.Audit.Search)]
        [SwaggerOperation(
            OperationId = "GetAuditSearchableProperties",
            Tags = new[] { AuditTag, IdentityBaseController.IdentityTag })]
        [ProducesResponseType(typeof(Result<Dictionary<string, string>>), 200)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EntitySearchableExportableImportablePropertiesResponseExample))]
        public async Task<IActionResult> SearchablePropertiesAsync()
        {
            var properties = ISearchable<Guid, Domain.Entities.Audit>.SearchableProperties(_auditLocalizer);
            return Ok(await Result<Dictionary<string, string>>.SuccessAsync(properties, _localizer["Searchable properties are got."]));
        }
    }
}