// ------------------------------------------------------------------------------------------------------
// <copyright file="IMailService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Uchoose.MailService.Interfaces.Requests;
using Uchoose.Utils.Contracts.Services;

namespace Uchoose.MailService.Interfaces
{
    /// <summary>
    /// Сервис для работы с email.
    /// </summary>
    public interface IMailService :
        IInfrastructureService
    {
        /// <summary>
        /// Отправить email сообщение.
        /// </summary>
        /// <param name="request">Запрос на отправку email сообщения.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        Task SendAsync(MailRequest request);

        /// <summary>
        /// Отправить email сообщение из шаблона.
        /// </summary>
        /// <param name="request">Запрос на отправку email сообщения из шаблона.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        Task SendTemplateEmailAsync(MailTemplateRequest request);
    }
}