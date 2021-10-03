// ------------------------------------------------------------------------------------------------------
// <copyright file="UserResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Uchoose.Domain.Identity.Entities;
using Uchoose.Utils.Attributes.Mappings;
using Uchoose.Utils.Contracts.Mappings;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.UserService.Interfaces.Responses
{
    /// <summary>
    /// Ответ с данными о пользователе.
    /// </summary>
    [UseReverseMap(typeof(UchooseUser))]
    public class UserResponse :
        IMapFromTo<UchooseUser, UserResponse>,
        IHasIsActive
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// UserName.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Email подтверждён.
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Пользователь активный.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Номер телефона подтверждён.
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Url к изображения профиля пользователя.
        /// </summary>
        public string ProfilePictureDataUrl { get; set; }

        /// <summary>
        /// Внешний идентификатор пользователя.
        /// </summary>
        /// <remarks>
        /// Идентификатор контакта в CRM.
        /// </remarks>
        public string ExternalId { get; set; }
    }
}