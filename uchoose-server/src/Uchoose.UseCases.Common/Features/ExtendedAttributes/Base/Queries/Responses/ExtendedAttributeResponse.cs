// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;

using Uchoose.Domain.Enums;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries.Responses
{
    /// <summary>
    /// Ответ на запрос на получение расширенного атрибута сущности по его идентификатору.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <param name="Id">Идентификатор расширенного атрибута сущности.</param>
    /// <param name="EntityId">Идентификатор сущности, которой принадлежит расширенный атрибут.</param>
    /// <param name="Type">Тип хранимого в расширенном атрибуте значения.</param>
    /// <param name="Key">Ключ. Уникальный в рамках одной сущности.</param>
    /// <param name="Decimal">Значение с типом Decimal.</param>
    /// <param name="Text">Значение с типом Text.</param>
    /// <param name="DateTime">Значение с типом DateTime.</param>
    /// <param name="Json">Значение с типом Json. Используется для хранения значений в виде json строки.</param>
    /// <param name="Boolean">Значение с типом Boolean.</param>
    /// <param name="Integer">Значение с типом Integer.</param>
    /// <param name="ExternalId">Внешний идентификатор.</param>
    /// <param name="Group">Группа. Используется для группировки различных расширенных атрибутов.</param>
    /// <param name="Description">Описание.</param>
    /// <param name="IsActive">Является активным.</param>
    public record ExtendedAttributeResponse<TEntityId>(
        Guid Id,
        TEntityId EntityId,
        ExtendedAttributeType Type,
        string Key,
        decimal? Decimal,
        string? Text,
        DateTime? DateTime,
        string? Json,
        bool? Boolean,
        int? Integer,
        string? ExternalId,
        string? Group,
        string? Description,
        bool IsActive)
            : IHasIsActive;
}