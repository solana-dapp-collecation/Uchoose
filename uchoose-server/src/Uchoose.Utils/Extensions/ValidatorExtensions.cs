// ------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using FluentValidation;
using Microsoft.Extensions.Localization;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Searching;
using Uchoose.Utils.Filters;
using Uchoose.Utils.Mappings.Converters;

namespace Uchoose.Utils.Extensions
{
    /// <summary>
    /// Методы расширения для валидаторов данных.
    /// </summary>
    public static class ValidatorExtensions
    {
        #region MustContainCorrectOrderingsFor

        /// <summary>
        /// Правило, указывающее, что значение должно содержать корректную строку с сортировками.
        /// </summary>
        /// <typeparam name="T">Тип проверяемого класса.</typeparam>
        /// <param name="ruleBuilder">Строитель правил.</param>
        /// <param name="orderedType">Тип, для которого применяется сортировка.</param>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        /// <returns>Возвращает <see cref="IRuleBuilderOptions{T,TProperty}"/>.</returns>
        public static IRuleBuilderOptions<T, string?> MustContainCorrectOrderingsFor<T>(
            this IRuleBuilderInitial<T, string?> ruleBuilder,
            Type orderedType,
            IStringLocalizer localizer)
                where T : class
            => ruleBuilder
                .Cascade(CascadeMode.Stop)
                .Must((_, value, context) =>
                {
                    string[]? orderings = new OrderByConverter().Convert(value);
                    if (orderings == null)
                    {
                        return true;
                    }

                    bool result = true;
                    var orderedProperties = new List<string>();
                    var propertyNames = orderedType
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Select(p => p.Name.ToLowerInvariant())
                        .ToList();
                    foreach (string? ordering in orderings)
                    {
                        var orderingParts = ordering
                            .Trim()
                            .Split(" ")
                            .Where(x => x.IsPresent())
                            .Select(x => x.Trim().ToLowerInvariant())
                            .ToList();
                        if (orderingParts.Count != 2)
                        {
                            context.AddFailure(string.Format(localizer["Ordering '{0}' does not contains 2 parts."]!, ordering));
                            result = false;
                            continue;
                        }

                        string propertyName = orderingParts[0];
                        string sortDirection = orderingParts.Last();

                        switch (sortDirection)
                        {
                            case "asc":
                            case "desc":
                            case "ascending":
                            case "descending":
                                break;
                            default:
                                context.AddFailure(string.Format(localizer["Ordering '{0}' does not contains correct sort direction."]!, ordering));
                                result = false;
                                continue;
                        }

                        if (!propertyNames.Contains(propertyName))
                        {
                            context.AddFailure(string.Format(localizer["Ordering '{0}' contains wrong property name."]!, ordering));
                            result = false;
                        }

                        if (orderedProperties.Contains(propertyName))
                        {
                            context.AddFailure(string.Format(localizer["Ordering '{0}' contains already used property name."]!, ordering));
                            result = false;
                            continue;
                        }

                        orderedProperties.Add(propertyName);
                    }

                    return result;
                })
                .WithMessage(_ => localizer["The '{PropertyName}' property must contain correct comma separated orderings: '<property1> <direction>,<property2> <direction>'."]);

        #endregion MustContainCorrectOrderingsFor

        #region MustContainOnlyPropertyNamesOfExportableEntity

        /// <summary>
        /// Правило, указывающее, что проверяемое значение может содержать только имена свойств указанного типа.
        /// </summary>
        /// <typeparam name="TEntityId">Тип идентификатора сущности с проверяемыми свойствами.</typeparam>
        /// <typeparam name="TEntity">Тип экспортируемой сущности с проверяемыми свойствами.</typeparam>
        /// <typeparam name="TRequest">Тип проверяемого запроса.</typeparam>
        /// <param name="ruleBuilder">Строитель правил.</param>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        /// <returns>Возвращает <see cref="IRuleBuilderOptions{T,TRpoperty}"/>.</returns>
        public static IRuleBuilderOptions<TRequest, IEnumerable<string>> MustContainOnlyPropertyNamesOfExportableEntity<TEntityId, TEntity, TRequest>(
            this IRuleBuilderInitial<TRequest, IEnumerable<string>> ruleBuilder,
            IStringLocalizer localizer)
                where TEntity : IEntity<TEntityId>, IExportable<TEntityId, TEntity>, new()
                where TRequest : class
            => ruleBuilder
                .Cascade(CascadeMode.Stop)
                .Must((_, values, context) =>
                {
                    if (values == null)
                    {
                        return true;
                    }

                    bool result = true;
                    var typeProperties = IExportable<TEntityId, TEntity>.ExportableProperties();
                    foreach (string? value in values.Where(x => x.IsPresent()).Select(x => x.Trim()).Distinct())
                    {
                        if (!typeProperties.ContainsKey(value))
                        {
                            result = false;
                            context.AddFailure(string.Format(localizer["Property '{0}' does not exist in exported entity."]!, value));
                        }
                    }

                    if (!result)
                    {
                        context.MessageFormatter.AppendArgument("EntityName", typeof(TEntity).GetGenericTypeName());
                        context.MessageFormatter.AppendArgument("AllowedProperties", string.Join(",", typeProperties.Select(x => $"'{x.Key}'").OrderBy(x => x)));
                    }

                    return result;
                })
                .WithMessage("The '{PropertyName}' property must contain only property names of '{EntityName}' entity. Allowed properties: {AllowedProperties}");

        #endregion MustContainOnlyPropertyNamesOfExportableEntity

        #region MustContainOnlyPropertyNamesOfSearchableEntity

        /// <summary>
        /// Правило, указывающее, что проверяемое значение может содержать только имена свойств указанного типа.
        /// </summary>
        /// <typeparam name="TEntityId">Тип идентификатора сущности с проверяемыми свойствами.</typeparam>
        /// <typeparam name="TEntity">Тип сущности, по которой можно осуществлять поиск, с проверяемыми свойствами.</typeparam>
        /// <typeparam name="TFilter">Тип проверяемого фильтра.</typeparam>
        /// <param name="ruleBuilder">Строитель правил.</param>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        /// <returns>Возвращает <see cref="IRuleBuilderOptions{T,TRpoperty}"/>.</returns>
        public static IRuleBuilderOptions<TFilter, IEnumerable<string>> MustContainOnlyPropertyNamesOfSearchableEntity<TEntityId, TEntity, TFilter>(
            this IRuleBuilderInitial<TFilter, IEnumerable<string>> ruleBuilder,
            IStringLocalizer localizer)
                where TEntity : IEntity<TEntityId>, ISearchable<TEntityId, TEntity>
                where TFilter : PaginationFilter
            => ruleBuilder
                .Cascade(CascadeMode.Stop)
                .Must((_, values, context) =>
                {
                    if (values == null)
                    {
                        return true;
                    }

                    bool result = true;
                    var typeProperties = ISearchable<TEntityId, TEntity>.SearchableProperties();
                    foreach (string? value in values.Where(x => x.IsPresent()).Select(x => x.Trim()).Distinct())
                    {
                        if (!typeProperties.ContainsKey(value))
                        {
                            result = false;
                            context.AddFailure(string.Format(localizer["Property '{0}' does not exist in searchable entity."]!, value));
                        }
                    }

                    if (!result)
                    {
                        context.MessageFormatter.AppendArgument("EntityName", typeof(TEntity).GetGenericTypeName());
                        context.MessageFormatter.AppendArgument("AllowedProperties", string.Join(",", typeProperties.Select(x => $"'{x.Key}'").OrderBy(x => x)));
                    }

                    return result;
                })
                .WithMessage("The '{PropertyName}' property must contain only property names of '{EntityName}' entity. Allowed properties: {AllowedProperties}");

        #endregion MustContainOnlyPropertyNamesOfSearchableEntity
    }
}