// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using AutoMapper;
using Uchoose.Domain.Identity.Entities;
using Uchoose.RoleClaimService.Interfaces.Models;
using Uchoose.Utils.Attributes.Mappings;
using Uchoose.Utils.Contracts.Mappings;

namespace Uchoose.RoleClaimService.Interfaces.Requests
{
    /// <summary>
    /// Запрос с данными разрешения роли пользователя.
    /// </summary>
    [UseReverseMap(typeof(UchooseRoleClaim))]
    public class RoleClaimRequest :
        IMapFromTo<UchooseRoleClaim, RoleClaimRequest>,
        IMapFromTo<RoleClaimModel, RoleClaimRequest>
    {
        /// <summary>
        /// Идентификатор разрешения роли.
        /// </summary>
        /// <example>0</example>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор роли, которому принадлежит разрешение.
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Тип.
        /// </summary>
        /// <example>Example</example>
        public string Type { get; set; }

        /// <summary>
        /// Значение.
        /// </summary>
        /// <example>Example</example>
        public string Value { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        /// <example>Example</example>
        public string Description { get; set; }

        /// <summary>
        /// Группа.
        /// </summary>
        /// <example>Example</example>
        public string Group { get; set; }

        /// <inheritdoc/>
        void IMapFromTo<UchooseRoleClaim, RoleClaimRequest>.Mapping(Profile profile, bool useReverseMap)
        {
            profile.CreateMap<RoleClaimRequest, UchooseRoleClaim>()
                .ForMember(dest => dest.ClaimType, source => source.MapFrom(c => c.Type))
                .ForMember(dest => dest.ClaimValue, source => source.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}