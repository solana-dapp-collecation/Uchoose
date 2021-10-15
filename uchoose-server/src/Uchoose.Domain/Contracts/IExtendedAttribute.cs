// ------------------------------------------------------------------------------------------------------
// <copyright file="IExtendedAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;

using Uchoose.Domain.Enums;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.Domain.Contracts
{
    /// <summary>
    /// Расширенный атрибут сущности.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности, которой принадлежит расширенный атрибут.</typeparam>
    public interface IExtendedAttribute<out TEntityId> :
        IEntity<Guid>,
        IHasIsActive
    {
        /// <summary>
        /// Идентификатор сущности, которой принадлежит расширенный атрибут.
        /// </summary>
        public TEntityId EntityId { get; }

        /// <summary>
        /// Тип хранимого в расширенном атрибуте значения.
        /// </summary>
        public ExtendedAttributeType Type { get; }

        /// <summary>
        /// Ключ.
        /// </summary>
        /// <remarks>
        /// Уникальный в рамках одной сущности.
        /// </remarks>
        public string Key { get; }

        /// <summary>
        /// Значение с типом Decimal.
        /// </summary>
        /// <remarks>
        /// Используется для хранения значений с плавающей точкой.
        /// </remarks>
        public decimal? Decimal { get; }

        /// <summary>
        /// Значение с типом Text.
        /// </summary>
        /// <remarks>
        /// Используется для хранения текстовых значений.
        /// </remarks>
        public string? Text { get; }

        /// <summary>
        /// Значение с типом DateTime.
        /// </summary>
        /// <remarks>
        /// Используется для хранения значений даты и времени.
        /// </remarks>
        public DateTime? DateTime { get; }

        /// <summary>
        /// Значение с типом Json.
        /// </summary>
        /// <remarks>
        /// Используется для хранения значений в виде json строки.
        /// </remarks>
        public string? Json { get; }

        /// <summary>
        /// Значение с типом Boolean.
        /// </summary>
        /// <remarks>
        /// Используется для хранения логических значений.
        /// </remarks>
        public bool? Boolean { get; }

        /// <summary>
        /// Значение с типом Integer.
        /// </summary>
        /// <remarks>
        /// Используется для хранения целочисленных значений.
        /// </remarks>
        public int? Integer { get; }

        /// <summary>
        /// Внешний идентификатор.
        /// </summary>
        public string? ExternalId { get; }

        /// <summary>
        /// Группа.
        /// </summary>
        /// <remarks>
        /// Используется для группировки различных расширенных атрибутов.
        /// </remarks>
        public string? Group { get; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string? Description { get; }
    }
}