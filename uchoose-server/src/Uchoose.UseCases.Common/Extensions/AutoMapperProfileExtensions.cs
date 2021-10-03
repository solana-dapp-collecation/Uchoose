// ------------------------------------------------------------------------------------------------------
// <copyright file="AutoMapperProfileExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using AutoMapper;
using Uchoose.Domain.Abstractions;
using Uchoose.UseCases.Common.Features.Common.Filters;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Commands;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries;
using Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Queries.Responses;

namespace Uchoose.UseCases.Common.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="AutoMapper"/>.
    /// </summary>
    public static class AutoMapperProfileExtensions
    {
        #region CreateExtendedAttributesMappings

        /// <summary>
        /// Добавить сопоставления (mappings) для классов расширенных атрибутов сущности.
        /// </summary>
        /// <param name="profile"><see cref="Profile"/>.</param>
        /// <param name="assemblies">Сборки с классами расширенных атрибутов, которые используются для создания соответствий (mappings).</param>
        /// <returns>Возвращает <see cref="Profile"/>.</returns>
        public static Profile CreateExtendedAttributesMappings(this Profile profile, params Assembly[] assemblies)
        {
            var extendedAttributeTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                    .Select(t => new
                    {
                        BaseGenericType = t.BaseType,
                        CurrentType = t
                    })
                    .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(ExtendedAttribute<,>)))
                .ToList();

            foreach (var extendedAttributeType in extendedAttributeTypes)
            {
                var extendedAttributeTypeGenericArguments = extendedAttributeType.BaseGenericType.GetGenericArguments().ToList();

                #region AddExtendedAttributeCommand

                var sourceType = typeof(AddExtendedAttributeCommand<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                profile.CreateMap(sourceType, extendedAttributeType.CurrentType).ReverseMap();

                #endregion AddExtendedAttributeCommand

                #region UpdateExtendedAttributeCommand

                sourceType = typeof(UpdateExtendedAttributeCommand<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                profile.CreateMap(sourceType, extendedAttributeType.CurrentType).ReverseMap();

                #endregion UpdateExtendedAttributeCommand

                #region GetExtendedAttributeByIdQuery

                var baseExtendedAttributeType = typeof(ExtendedAttribute<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                sourceType = typeof(GetByIdCacheableFilter<,>).MakeGenericType(typeof(Guid), baseExtendedAttributeType);
                var destinationType = typeof(GetExtendedAttributeByIdQuery<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                profile.CreateMap(sourceType, destinationType);

                #endregion GetExtendedAttributeByIdQuery

                #region ExtendedAttributeResponse

                sourceType = typeof(ExtendedAttributeResponse<>).MakeGenericType(extendedAttributeTypeGenericArguments[0]);
                profile.CreateMap(sourceType, extendedAttributeType.CurrentType).ReverseMap();

                #endregion ExtendedAttributeResponse
            }

            return profile;
        }

        #endregion CreateExtendedAttributesMappings
    }
}