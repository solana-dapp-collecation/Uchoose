// ------------------------------------------------------------------------------------------------------
// <copyright file="AuditTrailsResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.AuditService.Interfaces.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Audit.Responses
{
    /// <summary>
    /// Пример ответа для получения данных аудита.
    /// </summary>
    public class AuditTrailsResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<AuditTrailsResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="AuditTrailsResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public AuditTrailsResponseExample(
            IStringLocalizer<AuditTrailsResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return PaginatedResult<AuditResponse>.Success(
                new()
                {
                    new()
                    {
                        Id = Guid.Empty,
                        Type = "Create",
                        UserId = Guid.Empty,
                        EntityName = _localizer["<Entity name>"],
                        DateTime = DateTime.MinValue,
                        PrimaryKey = _localizer["<Primary key>"],
                        AffectedColumns = _localizer["<Affected columns>"],
                        OldValues = _localizer["<Serialized old values>"],
                        NewValues = _localizer["<Serialized new values>"]
                    },
                    new()
                    {
                        Id = Guid.Empty,
                        Type = "Update",
                        UserId = Guid.Empty,
                        EntityName = _localizer["<Entity name>"],
                        DateTime = DateTime.MinValue,
                        PrimaryKey = _localizer["<Primary key>"],
                        AffectedColumns = _localizer["<Affected columns>"],
                        OldValues = _localizer["<Serialized old values>"],
                        NewValues = _localizer["<Serialized new values>"]
                    }
                },
                2,
                1,
                10);
        }
    }
}