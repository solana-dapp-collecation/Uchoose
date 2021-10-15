// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributesProfile.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using AutoMapper;
using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Identity.Entities;
using Uchoose.UseCases.Common.Extensions;

namespace Uchoose.UseCases.Common.Mappings
{
    /// <summary>
    /// Профиль сопоставления для <see cref="ExtendedAttribute{TEntityId,TEntity}"/> текущей сборки.
    /// </summary>
    public class ExtendedAttributesProfile : Profile
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="ExtendedAttributesProfile"/>.
        /// </summary>
        public ExtendedAttributesProfile()
        {
            this.CreateExtendedAttributesMappings(Assembly.GetAssembly(typeof(UchooseUser)));

            // this.CreateExtendedAttributesMappings(Assembly.GetAssembly(typeof(NFTRequest))); // TODO
        }
    }
}