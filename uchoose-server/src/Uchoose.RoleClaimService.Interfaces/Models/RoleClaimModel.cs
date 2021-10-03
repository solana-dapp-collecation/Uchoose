// ------------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimModel.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using AutoMapper;
using Uchoose.Domain.Identity.Entities;
using Uchoose.RoleClaimService.Interfaces.Responses;
using Uchoose.Utils.Attributes.Mappings;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.RoleClaimService.Interfaces.Models
{
    /// <summary>
    /// Ответ с данными разрешения роли пользователя.
    /// </summary>
    [UseReverseMap(typeof(UchooseRoleClaim), typeof(RoleClaimResponse))]
    public class RoleClaimModel :
        IHasValue,
        IHasType,
        IHasGroup,
        IHasDescription,
        IMapFromTo<UchooseRoleClaim, RoleClaimModel>,
        IMapFromTo<RoleClaimResponse, RoleClaimModel>
    {
        /// <summary>
        /// Идентификатор разрешения роли.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор роли, которому принадлежит разрешение.
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Тип.
        /// </summary>
        /// <example>Permission</example>
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

        /// <summary>
        /// Разрешение роли добавлено к роли.
        /// </summary>
        /// <example>true</example>
        public bool Selected { get; set; }

        /// <inheritdoc/>
        void IMapFromTo<UchooseRoleClaim, RoleClaimModel>.Mapping(Profile profile, bool useReverseMap)
        {
            profile.CreateMap<RoleClaimModel, UchooseRoleClaim>()
                .ForMember(dest => dest.ClaimType, source => source.MapFrom(c => c.Type))
                .ForMember(dest => dest.ClaimValue, source => source.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}