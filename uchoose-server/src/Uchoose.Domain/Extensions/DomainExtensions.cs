// ------------------------------------------------------------------------------------------------------
// <copyright file="DomainExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Uchoose.Domain.Contracts;

namespace Uchoose.Domain.Extensions
{
    /// <summary>
    /// Методы расширения для доменных типов.
    /// </summary>
    public static class DomainExtensions
    {
        /// <summary>
        /// Список доменных типов.
        /// </summary>
        private static readonly List<Type> DomainTypes = new()
        {
            typeof(IAggregate),
            typeof(IAuditableEntity),
            typeof(IBusinessRule),
            typeof(IDomainEntity),
            typeof(IDomainEvent),
            typeof(IDomainService),
            typeof(IValueObject)
        };

        #region Domain checks

        /// <summary>
        /// Проверить, является ли тип доменным.
        /// </summary>
        /// <param name="type">Проверяемый тип.</param>
        /// <returns>Возвращает true, если проверяемый тип является доменным. Иначе - false.</returns>
        public static bool IsDomainObject(this Type type) => DomainTypes.Any(s => s.IsAssignableFrom(type));

        /// <summary>
        /// Проверить, является ли тип <see cref="IAggregate"/>.
        /// </summary>
        /// <param name="type">Проверяемый тип.</param>
        /// <returns>Возвращает true, если проверяемый тип является <see cref="IAggregate"/>. Иначе - false.</returns>
        public static bool IsAggregate(this Type type) => typeof(IAggregate).IsAssignableFrom(type);

        /// <summary>
        /// Проверить, является ли тип <see cref="IAuditableEntity"/>.
        /// </summary>
        /// <param name="type">Проверяемый тип.</param>
        /// <returns>Возвращает true, если проверяемый тип является <see cref="IAuditableEntity"/>. Иначе - false.</returns>
        public static bool IsAuditableEntity(this Type type) => typeof(IAuditableEntity).IsAssignableFrom(type);

        /// <summary>
        /// Проверить, является ли тип <see cref="IBusinessRule"/>.
        /// </summary>
        /// <param name="type">Проверяемый тип.</param>
        /// <returns>Возвращает true, если проверяемый тип является <see cref="IBusinessRule"/>. Иначе - false.</returns>
        public static bool IsBusinessRule(this Type type) => typeof(IBusinessRule).IsAssignableFrom(type);

        /// <summary>
        /// Проверить, является ли тип <see cref="IDomainEvent"/>.
        /// </summary>
        /// <param name="type">Проверяемый тип.</param>
        /// <returns>Возвращает true, если проверяемый тип является <see cref="IDomainEvent"/>. Иначе - false.</returns>
        public static bool IsDomainEvent(this Type type) => typeof(IDomainEvent).IsAssignableFrom(type);

        /// <summary>
        /// Проверить, является ли тип <see cref="IDomainService"/>.
        /// </summary>
        /// <param name="type">Проверяемый тип.</param>
        /// <returns>Возвращает true, если проверяемый тип является <see cref="IDomainService"/>. Иначе - false.</returns>
        public static bool IsDomainService(this Type type) => typeof(IDomainService).IsAssignableFrom(type);

        /// <summary>
        /// Проверить, является ли тип <see cref="IDomainEntity"/>.
        /// </summary>
        /// <param name="type">Проверяемый тип.</param>
        /// <returns>Возвращает true, если проверяемый тип является <see cref="IDomainEntity"/>. Иначе - false.</returns>
        public static bool IsDomainEntity(this Type type) => typeof(IDomainEntity).IsAssignableFrom(type);

        /// <summary>
        /// Проверить, является ли тип <see cref="IValueObject"/>.
        /// </summary>
        /// <param name="type">Проверяемый тип.</param>
        /// <returns>Возвращает true, если проверяемый тип является <see cref="IValueObject"/>. Иначе - false.</returns>
        public static bool IsValueObject(this Type type) => typeof(IValueObject).IsAssignableFrom(type);

        #endregion Domain checks
    }
}