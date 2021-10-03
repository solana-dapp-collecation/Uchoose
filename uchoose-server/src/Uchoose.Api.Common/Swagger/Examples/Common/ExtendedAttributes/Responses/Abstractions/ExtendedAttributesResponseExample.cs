// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributesResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Domain.Enums;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Common.ExtendedAttributes.Responses.Abstractions
{
    /// <summary>
    /// Пример ответа для получения списка расширенных атрибутов сущности.
    /// </summary>
    internal class ExtendedAttributesResponseExample
    {
        // для локализации
    }

    /// <summary>
    /// Пример ответа для получения списка расширенных атрибутов сущности с идентификатором указанного типа.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    internal abstract class ExtendedAttributesResponseExample<TEntityId> : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<ExtendedAttributesResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ExtendedAttributesResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        protected ExtendedAttributesResponseExample(IStringLocalizer<ExtendedAttributesResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <summary>
        /// Получить значение по умолчанию для идентификатора сущности.
        /// </summary>
        /// <returns>Возвращает значение по умолчанию для идентификатора сущности.</returns>
        public abstract TEntityId GetDefaultEntityId();

        /// <inheritdoc/>
        public object GetExamples()
        {
            return PaginatedResult<ExtendedAttributeResponse<TEntityId>>.Success(
                new()
                {
                    new(
                        Guid.Empty,
                        GetDefaultEntityId(),
                        ExtendedAttributeType.Decimal,
                        _localizer["<Key>"],
                        (decimal?)10.52,
                        null,
                        null,
                        null,
                        null,
                        null,
                        _localizer["<External Id>"],
                        _localizer["<Group>"],
                        _localizer["<Description>"],
                        true),
                    new(
                        Guid.Empty,
                        GetDefaultEntityId(),
                        ExtendedAttributeType.Text,
                        _localizer["<Key>"],
                        null,
                        _localizer["<Text>"],
                        null,
                        null,
                        null,
                        null,
                        _localizer["<External Id>"],
                        _localizer["<Group>"],
                        _localizer["<Description>"],
                        true),
                    new(
                        Guid.Empty,
                        GetDefaultEntityId(),
                        ExtendedAttributeType.DateTime,
                        _localizer["<Key>"],
                        null,
                        null,
                        DateTime.MinValue,
                        null,
                        null,
                        null,
                        _localizer["<External Id>"],
                        _localizer["<Group>"],
                        _localizer["<Description>"],
                        true),
                    new(
                        Guid.Empty,
                        GetDefaultEntityId(),
                        ExtendedAttributeType.Json,
                        _localizer["<Key>"],
                        null,
                        null,
                        null,
                        "{ id: 1 }",
                        null,
                        null,
                        _localizer["<External Id>"],
                        _localizer["<Group>"],
                        _localizer["<Description>"],
                        true),
                    new(
                        Guid.Empty,
                        GetDefaultEntityId(),
                        ExtendedAttributeType.Boolean,
                        _localizer["<Key>"],
                        null,
                        null,
                        null,
                        null,
                        true,
                        null,
                        _localizer["<External Id>"],
                        _localizer["<Group>"],
                        _localizer["<Description>"],
                        true),
                    new(
                        Guid.Empty,
                        GetDefaultEntityId(),
                        ExtendedAttributeType.Integer,
                        _localizer["<Key>"],
                        null,
                        null,
                        null,
                        null,
                        null,
                        5,
                        _localizer["<External Id>"],
                        _localizer["<Group>"],
                        _localizer["<Description>"],
                        true)
                },
                6,
                1,
                10);
        }
    }
}