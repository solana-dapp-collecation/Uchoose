// ------------------------------------------------------------------------------------------------------
// <copyright file="IMapFromTo.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;

using AutoMapper;
using Uchoose.Utils.Exceptions;
using Uchoose.Utils.Extensions;

namespace Uchoose.Utils.Contracts.Mappings
{
    /// <summary>
    /// Интерфейс для обозначения сопоставления (mapping) между классами.
    /// </summary>
    /// <remarks>
    /// <typeparamref name="TDestination"/> должен совпадать с типом класса, который реализует интерфейс <see cref="IMapFromTo{TSource,TDestination}"/>,
    /// и не должен содержать generic параметров.
    /// </remarks>
    /// <typeparam name="TSource">Тип класса для сопоставления.</typeparam>
    /// <typeparam name="TDestination">Тип класса, к которому происходит сопоставление.</typeparam>
    public interface IMapFromTo<TSource, TDestination>
        where TSource : class
        where TDestination : class, IMapFromTo<TSource, TDestination>, new()
    {
        /// <summary>
        /// Преобразовать к указанному типу.
        /// </summary>
        /// <param name="profile"><see cref="Profile"/>.</param>
        /// <param name="useReverseMap">Использовать <see cref="IMappingExpression.ReverseMap"/>? По умолчанию - false.</param>
        void Mapping(Profile profile, bool useReverseMap = false)
        {
            if (typeof(TDestination) == GetType() && !typeof(TDestination).ContainsGenericParameters)
            {
                if (useReverseMap)
                {
                    profile.CreateMap(typeof(TSource), GetType()).ReverseMap();
                }
                else
                {
                    profile.CreateMap(typeof(TSource), GetType());
                }
            }
            else
            {
                // применяем подход fail-fast: при несоответствии типа класса TDestination нужному типу,
                // возникнет это исключение в runtime во время регистрации всех сопоставлений по IMapFromTo
                throw new CustomException($"Destination type '{typeof(TDestination).GetGenericTypeName()}' in {nameof(IMapFromTo<TSource, TDestination>)} with source type '{typeof(TSource).GetGenericTypeName()}' is wrong!", statusCode: HttpStatusCode.InternalServerError);
            }
        }
    }
}