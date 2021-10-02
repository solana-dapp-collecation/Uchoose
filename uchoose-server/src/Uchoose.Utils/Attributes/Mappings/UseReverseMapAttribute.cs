// ------------------------------------------------------------------------------------------------------
// <copyright file="UseReverseMapAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using AutoMapper;
using Uchoose.Utils.Contracts.Mappings;

namespace Uchoose.Utils.Attributes.Mappings
{
    /// <summary>
    /// Атрибут, указывающий, что необходимо применить <see cref="IMappingExpression.ReverseMap"/>
    /// для сопоставления класса, реализующего <see cref="IMapFromTo{TSource,TDestination}"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class UseReverseMapAttribute : Attribute
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="UseReverseMapAttribute"/>.
        /// </summary>
        /// <param name="reversedTypes">Массив типов, к которым применяется <see cref="IMappingExpression.ReverseMap"/>.</param>
        public UseReverseMapAttribute(params Type[] reversedTypes)
        {
            ReversedTypes = reversedTypes;
        }

        /// <summary>
        /// Массив типов, к которым применяется <see cref="IMappingExpression.ReverseMap"/>.
        /// </summary>
        /// <remarks>
        /// Если типа нет в массиве, то для него не будет применён <see cref="IMappingExpression.ReverseMap"/>.
        /// Если массив пуст, то для всех типов будет применён <see cref="IMappingExpression.ReverseMap"/>.
        /// </remarks>
        public Type[] ReversedTypes { get; }
    }
}