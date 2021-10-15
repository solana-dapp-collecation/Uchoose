// ------------------------------------------------------------------------------------------------------
// <copyright file="UserClaimResponse.cs" company="Life Loop">
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

namespace Uchoose.UserClaimService.Interfaces.Responses
{
    /// <summary>
    /// Ответ с данными разрешения пользователя.
    /// </summary>
    [UseReverseMap(typeof(UchooseUserClaim))]
    public class UserClaimResponse :
        IMapFromTo<UchooseUserClaim, UserClaimResponse>,
        IHasDescription
    {
        /// <summary>
        /// Идентификатор разрешения пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит разрешение.
        /// </summary>
        public Guid UserId { get; set; }

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
        void IMapFromTo<UchooseUserClaim, UserClaimResponse>.Mapping(Profile profile, bool useReverseMap)
        {
            profile.CreateMap<UserClaimResponse, UchooseUserClaim>()
                .ForMember(dest => dest.ClaimType, source => source.MapFrom(c => c.Type))
                .ForMember(dest => dest.ClaimValue, source => source.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}