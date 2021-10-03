// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeSpecification.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Domain.Abstractions;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Specifications;

namespace Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries.Specifications.Base
{
    /// <summary>
    /// Спецификация для <see cref="ExtendedAttribute{TEntityId, TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    /// <typeparam name="TExtendedAttribute">Тип расширенного атрибута сущности.</typeparam>
    internal abstract class ExtendedAttributeSpecification<TEntityId, TEntity, TExtendedAttribute> :
        UchooseSpecification<TExtendedAttribute>
            where TEntity : class, IEntity<TEntityId>
            where TExtendedAttribute : ExtendedAttribute<TEntityId, TEntity>
    {
    }
}