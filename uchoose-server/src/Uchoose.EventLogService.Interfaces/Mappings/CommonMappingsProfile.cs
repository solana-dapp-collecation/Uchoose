// ------------------------------------------------------------------------------------------------------
// <copyright file="CommonMappingsProfile.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using AutoMapper;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Extensions;

namespace Uchoose.EventLogService.Interfaces.Mappings
{
    /// <summary>
    /// Общий профиль сопоставлений для классов текущей сборки.
    /// </summary>
    /// <remarks>
    /// Создаёт сопоставления для классов, реализующих <see cref="IMapFromTo{TSource,TDestination}"/>.
    /// </remarks>
    public class CommonMappingsProfile : Profile
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="CommonMappingsProfile"/>.
        /// </summary>
        public CommonMappingsProfile()
        {
            this.ApplyMappingsFromAssemblies(Assembly.GetExecutingAssembly());
        }
    }
}