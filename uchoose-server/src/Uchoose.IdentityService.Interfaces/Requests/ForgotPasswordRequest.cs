// ------------------------------------------------------------------------------------------------------
// <copyright file="ForgotPasswordRequest.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Uchoose.IdentityService.Interfaces.Requests
{
    /// <summary>
    /// Запрос для формирования email со ссылкой для сброса пароля пользователя.
    /// </summary>
    public class ForgotPasswordRequest
    {
        /// <summary>
        /// Email пользователя, на который отправляется ссылка для сброса пароля.
        /// </summary>
        /// <example>example@example.com</example>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}