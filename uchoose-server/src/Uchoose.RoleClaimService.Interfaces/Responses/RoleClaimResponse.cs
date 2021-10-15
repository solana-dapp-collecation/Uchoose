// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using AutoMapper;
using Uchoose.Domain.Identity.Entities;
using Uchoose.Utils.Attributes.Mappings;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.RoleClaimService.Interfaces.Responses
{
    /// <summary>
    /// Ответ с данными разрешения роли пользователя.
    /// </summary>
    [UseReverseMap(typeof(UchooseRoleClaim))]
    public class RoleClaimResponse :
        IMapFromTo<UchooseRoleClaim, RoleClaimResponse>,
        IHasDescription
    {
        /// <summary>
        /// Идентификатор разрешения роли.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор роли, которому принадлежит разрешение.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Тип.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Значение.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Группа.
        /// </summary>
        public string Group { get; set; }

        /// <inheritdoc/>
        void IMapFromTo<UchooseRoleClaim, RoleClaimResponse>.Mapping(Profile profile, bool useReverseMap)
        {
            profile.CreateMap<RoleClaimResponse, UchooseRoleClaim>()
                .ForMember(dest => dest.ClaimType, source => source.MapFrom(c => c.Type))
                .ForMember(dest => dest.ClaimValue, source => source.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}