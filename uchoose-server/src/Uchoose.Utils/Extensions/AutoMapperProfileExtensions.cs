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
using Uchoose.Utils.Attributes.Mappings;
using Uchoose.Utils.Contracts.Mappings;

namespace Uchoose.Utils.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="AutoMapper"/>.
    /// </summary>
    public static class AutoMapperProfileExtensions
    {
        #region ApplyMappingsFromAssemblies

        /// <summary>
        /// Добавить сопоставления (mappings) для классов, наследующих <see cref="IMapFromTo{TSource,TDestination}"/>.
        /// </summary>
        /// <remarks>
        /// Не применимо для типов record.
        /// </remarks>
        /// <param name="profile"><see cref="Profile"/>.</param>
        /// <param name="assemblies">Сборки с классами, которые используются для создания соответствий (mappings).</param>
        /// <returns>Возвращает <see cref="Profile"/>.</returns>
        public static Profile ApplyMappingsFromAssemblies(this Profile profile, params Assembly[] assemblies)
        {
            var mappedTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes()
                    .Where(t => t.GetInterfaces().Any(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFromTo<,>))))
                    .Select(t => new
                    {
                        Interfaces = t.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFromTo<,>)),
                        CurrentType = t
                    })
                .ToList();

            foreach (var mappedType in mappedTypes)
            {
                bool hasDefaultConstructor = mappedType.CurrentType.GetConstructors().Any(t => t.GetParameters().Length == 0);
                if (!hasDefaultConstructor)
                {
                    continue;
                }

                var useReverseMapAttribute = mappedType.CurrentType.GetCustomAttribute(typeof(UseReverseMapAttribute)) as UseReverseMapAttribute;
                foreach (var assignedInterface in mappedType.Interfaces)
                {
                    bool useReverseMap = useReverseMapAttribute != null;
                    var genericArguments = assignedInterface.GetGenericArguments().ToList();
                    var methodInfo = assignedInterface.GetMethod(nameof(IMapFromTo<object, MappedObject>.Mapping));
                    if (!mappedType.CurrentType.ContainsGenericParameters)
                    {
                        object instance = Activator.CreateInstance(mappedType.CurrentType);
                        if (useReverseMapAttribute?.ReversedTypes?.Any() == true)
                        {
                            useReverseMap = useReverseMapAttribute.ReversedTypes.Contains(genericArguments[0]);
                        }

                        methodInfo?.Invoke(instance, new object[] { profile, useReverseMap });
                    }
                }
            }

            return profile;
        }

        /// <summary>
        /// Класс-болванка для <see cref="ApplyMappingsFromAssemblies"/>.
        /// </summary>
        private class MappedObject :
            IMapFromTo<object, MappedObject>
        {
        }

        #endregion ApplyMappingsFromAssemblies
    }
}