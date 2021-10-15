// ------------------------------------------------------------------------------------------------------
// <copyright file="UserClaimModel.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using AutoMapper;
using Uchoose.Domain.Identity.Entities;
using Uchoose.UserClaimService.Interfaces.Responses;
using Uchoose.Utils.Attributes.Mappings;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.UserClaimService.Interfaces.Models
{
    /// <summary>
    /// Ответ с данными разрешения пользователя.
    /// </summary>
    [UseReverseMap(typeof(UchooseUserClaim), typeof(UserClaimResponse))]
    public class UserClaimModel :
        IHasValue,
        IHasType,
        IHasGroup,
        IHasDescription,
        IMapFromTo<UchooseUserClaim, UserClaimModel>,
        IMapFromTo<UserClaimResponse, UserClaimModel>
    {
        /// <summary>
        /// Идентификатор разрешения пользователя.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит разрешение.
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public Guid UserId { get; set; }

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
        /// Разрешение пользователя добавлено к пользователю.
        /// </summary>
        /// <example>true</example>
        public bool Selected { get; set; }

        /// <inheritdoc/>
        void IMapFromTo<UchooseUserClaim, UserClaimModel>.Mapping(Profile profile, bool useReverseMap)
        {
            profile.CreateMap<UserClaimModel, UchooseUserClaim>()
                .ForMember(dest => dest.ClaimType, source => source.MapFrom(c => c.Type))
                .ForMember(dest => dest.ClaimValue, source => source.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}