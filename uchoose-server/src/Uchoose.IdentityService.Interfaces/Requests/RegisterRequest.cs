// ------------------------------------------------------------------------------------------------------
// <copyright file="RegisterRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Uchoose.IdentityService.Interfaces.Requests
{
    /// <summary>
    /// Запрос на регистрацию нового пользователя.
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// Имя.
        /// </summary>
        /// <example>FirstName</example>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        /// <example>LastName</example>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        /// <example>MiddleName</example>
        public string MiddleName { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        /// <example>example@example.com</example>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Email подтверждён.
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// UserName.
        /// </summary>
        /// <example>UserName</example>
        [Required]
        [MinLength(6)]
        public string UserName { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        /// <example>********</example>
        [Required]
        [MinLength(6)]
        [PasswordPropertyText]
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля.
        /// </summary>
        /// <example>********</example>
        [Required]
        [Compare(nameof(Password))]
        [PasswordPropertyText]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Номер телефона подтверждён.
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Активировать пользователя.
        /// </summary>
        /// <example>false</example>
        public bool ActivateUser { get; set; } = false;
    }
}