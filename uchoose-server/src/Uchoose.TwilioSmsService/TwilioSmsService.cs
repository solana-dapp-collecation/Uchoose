// ------------------------------------------------------------------------------------------------------
// <copyright file="TwilioSmsService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Uchoose.SmsService.Interfaces;
using Uchoose.SmsService.Interfaces.Requests;
using Uchoose.SmsService.Interfaces.Settings;
using Uchoose.Utils.Contracts.Services;

namespace Uchoose.TwilioSmsService
{
    /// <inheritdoc cref="ISmsService"/>
    internal sealed class TwilioSmsService : ISmsService, ITransientService
    {
        private readonly SmsSettings _settings;

        /// <summary>
        /// Инициализирует экземпляр <see cref="TwilioSmsService"/>.
        /// </summary>
        /// <param name="settings"><see cref="SmsSettings"/>.</param>
        public TwilioSmsService(
            IOptionsSnapshot<SmsSettings> settings)
        {
            _settings = settings.Value;
        }

        /// <inheritdoc/>
        public async Task SendAsync(SmsRequest request)
        {
            string accountSid = _settings.SmsAccountIdentification;
            string authToken = _settings.SmsAccountPassword;

            TwilioClient.Init(accountSid, authToken);

            await MessageResource.CreateAsync(
                to: new(request.Number),
                from: new(_settings.SmsAccountFrom),
                body: request.Message);

            await Task.CompletedTask;
        }
    }
}