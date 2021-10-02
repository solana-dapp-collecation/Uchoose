// ------------------------------------------------------------------------------------------------------
// <copyright file="ISmsService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Uchoose.SmsService.Interfaces.Requests;
using Uchoose.Utils.Contracts.Services;

namespace Uchoose.SmsService.Interfaces
{
    /// <summary>
    /// Сервис для работы с СМС.
    /// </summary>
    public interface ISmsService :
        IInfrastructureService
    {
        /// <summary>
        /// Отправить СМС сообщение.
        /// </summary>
        /// <param name="request">Запрос на отправку СМС сообщения.</param>
        /// <returns>Возвращает результат выполнения операции.</returns>
        Task SendAsync(SmsRequest request);
    }
}