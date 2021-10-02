// ------------------------------------------------------------------------------------------------------
// <copyright file="DomainEntity.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Uchoose.Domain.Contracts;
using Uchoose.Utils.Attributes.Exporting;
using Uchoose.Utils.Attributes.Importing;
using Uchoose.Utils.Attributes.Ordering;
using Uchoose.Utils.Attributes.Searching;
using Uchoose.Utils.Contracts.Common;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
// ReSharper disable VirtualMemberCallInConstructor
namespace Uchoose.Domain.Abstractions
{
    /// <inheritdoc cref="IDomainEntity"/>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    // https://github.com/vkhorikov/DddAndEFCore/blob/master/src/App/Entity.cs
    public abstract class DomainEntity<TEntityId> :
        IEntity<TEntityId>,
        IDomainEntity
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DomainEntity{TEntityId}"/>.
        /// </summary>
        /// <remarks>
        /// Свойство Id инициализируется новым значением GUID.
        /// </remarks>
        protected DomainEntity()
        {
            Id = GenerateNewId();
        }

        /// <inheritdoc cref="IEntity{TEntityId}.Id"/>
        [ExportDefaultOrder(-100)]
        [Display(Name = "Entity identifier")]
        public TEntityId Id { get; private set; }

        /// <summary>
        /// Метод для генерации нового идентификатора сущности.
        /// </summary>
        /// <returns>Возвращает новый идентификатор сущности.</returns>
        public abstract TEntityId GenerateNewId();

#pragma warning disable SA1201 // Elements should appear in the correct order
        private List<IDomainEvent> _domainEvents;
#pragma warning restore SA1201 // Elements should appear in the correct order

        /// <inheritdoc/>
        [JsonIgnore] // для System.Text.Json
        [NotExportable]
        [NotImportable]
        [NotSearchable]
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        /// <summary>
        /// Оператор равенства.
        /// </summary>
        /// <param name="obj1">Левый операнд.</param>
        /// <param name="obj2">Правый операнд.</param>
        /// <returns>Возвращает true, если операнды равны. Иначе - false.</returns>
        public static bool operator ==(DomainEntity<TEntityId> obj1, DomainEntity<TEntityId> obj2)
        {
            if (obj1 is null && obj2 is null)
            {
                return true;
            }

            if (obj1 is null || obj2 is null)
            {
                return false;
            }

            return obj1.Equals(obj2);
        }

        /// <summary>
        /// Оператор неравенства.
        /// </summary>
        /// <param name="obj1">Левый операнд.</param>
        /// <param name="obj2">Правый операнд.</param>
        /// <returns>Возвращает true, если операнды не равны. Иначе - false.</returns>
        public static bool operator !=(DomainEntity<TEntityId> obj1, DomainEntity<TEntityId> obj2) => !(obj1 == obj2);

        /// <inheritdoc/>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (this is IVersionableByEvent and IAggregate aggregate)
            {
                aggregate.IncrementVersion();
            }

            _domainEvents ??= new();
            _domainEvents.Add(domainEvent);
        }

        /// <inheritdoc/>
        public void RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents?.Remove(domainEvent);

        /// <inheritdoc/>
        public void ClearDomainEvents() => _domainEvents?.Clear();

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not DomainEntity<TEntityId> other)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetRealType() != other.GetRealType())
            {
                return false;
            }

            if (Id.Equals(default) || other.Id.Equals(default))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        // ReSharper disable once NonReadonlyMemberInGetHashCode

        /// <inheritdoc/>
        public override int GetHashCode() => (GetRealType().ToString() + Id).GetHashCode();

        /// <summary>
        /// Проверить бизнес-правило.
        /// </summary>
        /// <param name="rule">Бизнес-правило.</param>
        public virtual void CheckRule(IBusinessRule rule) => (this as IDomainEntity).CheckRule(rule);

        private Type GetRealType() => GetType();
    }
}