// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Domain.Enums;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Common.ExtendedAttributes.Responses.Abstractions
{
    /// <summary>
    /// Пример ответа для получения расширенного атрибута сущности.
    /// </summary>
    internal class ExtendedAttributeResponseExample
    {
        // для локализации
    }

    /// <summary>
    /// Пример ответа для получения расширенного атрибута сущности с идентификатором указанного типа.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    internal abstract class ExtendedAttributeResponseExample<TEntityId> : IMultipleExamplesProvider<object>
    {
        private readonly IStringLocalizer<ExtendedAttributeResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ExtendedAttributeResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        protected ExtendedAttributeResponseExample(IStringLocalizer<ExtendedAttributeResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <summary>
        /// Получить значение по умолчанию для идентификатора сущности.
        /// </summary>
        /// <returns>Возвращает значение по умолчанию для идентификатора сущности.</returns>
        public abstract TEntityId GetDefaultEntityId();

        /// <inheritdoc/>
        public IEnumerable<SwaggerExample<object>> GetExamples()
        {
            yield return SwaggerExample.Create(
                _localizer["Decimal example"],
                _localizer["Decimal example"],
                Result<ExtendedAttributeResponse<TEntityId>>.Success(
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
                    _localizer["<Message>"]) as object);

            yield return SwaggerExample.Create(
                _localizer["Text example"],
                _localizer["Text example"],
                Result<ExtendedAttributeResponse<TEntityId>>.Success(
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
                    _localizer["<Message>"]) as object);

            yield return SwaggerExample.Create(
                _localizer["DateTime example"],
                _localizer["DateTime example"],
                Result<ExtendedAttributeResponse<TEntityId>>.Success(
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
                    _localizer["<Message>"]) as object);

            yield return SwaggerExample.Create(
                _localizer["Json example"],
                _localizer["Json example"],
                Result<ExtendedAttributeResponse<TEntityId>>.Success(
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
                    _localizer["<Message>"]) as object);

            yield return SwaggerExample.Create(
                _localizer["Boolean example"],
                _localizer["Boolean example"],
                Result<ExtendedAttributeResponse<TEntityId>>.Success(
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
                    _localizer["<Message>"]) as object);

            yield return SwaggerExample.Create(
                _localizer["Integer example"],
                _localizer["Integer example"],
                Result<ExtendedAttributeResponse<TEntityId>>.Success(
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
                        true),
                    _localizer["<Message>"]) as object);
        }
    }
}