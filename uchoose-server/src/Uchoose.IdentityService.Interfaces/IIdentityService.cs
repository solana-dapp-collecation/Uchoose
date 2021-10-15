// ------------------------------------------------------------------------------------------------------
// <copyright file="IIdentityService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Uchoose.IdentityService.Interfaces.Requests;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.IdentityService.Interfaces
{
    /// <summary>
    /// Сервис для работы с Identity.
    /// </summary>
    public interface IIdentityService :
        IApplicationService
    {
        /// <summary>
        /// Зарегистрировать нового пользователя.
        /// </summary>
        /// <param name="request">Запрос на регистрацию нового пользователя.</param>
        /// <param name="origin">Заголовок запроса Origin.</param>
        /// <returns>Возвращает идентификатор зарегистрированного пользователя.</returns>
        Task<IResult<Guid>> RegisterAsync(RegisterRequest request, string origin);

        /// <summary>
        /// Подтвердить email пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="code">Код для подтверждения.</param>
        /// <returns>Возвращает идентификатор пользователя с подтверждённым email.</returns>
        Task<IResult<Guid>> ConfirmEmailAsync(Guid userId, string code);

        /// <summary>
        /// Подтвердить номер телефона пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="code">Код для подтверждения.</param>
        /// <returns>Возвращает идентификатор пользователя с подтверждённым номером телефона.</returns>
        Task<IResult<Guid>> ConfirmPhoneNumberAsync(Guid userId, string code);

        /// <summary>
        /// Отправить ссылку для сброса пароля пользователя по email.
        /// </summary>
        /// <remarks>
        /// Используется, когда пользователь забыл свой пароль.
        /// </remarks>
        /// <param name="request">Запрос для формирования email со ссылкой для сброса пароля пользователя.</param>
        /// <param name="origin">Заголовок запроса Origin.</param>
        /// <returns>Возвращает <see cref="IResult"/>.</returns>
        Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);

        /// <summary>
        /// Сбросить пароль пользователя.
        /// </summary>
        /// <param name="request">Запрос для сброса пароля.</param>
        /// <returns>Возвращает <see cref="IResult"/>.</returns>
        Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);
    }
}