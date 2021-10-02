// ------------------------------------------------------------------------------------------------------
// <copyright file="SmtpMailService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Uchoose.MailService.Interfaces;
using Uchoose.MailService.Interfaces.Requests;
using Uchoose.MailService.Interfaces.Settings;
using Uchoose.Utils.Contracts.Services;

using ContentType = MimeKit.ContentType;

namespace Uchoose.SmtpMailService
{
    /// <inheritdoc cref="IMailService"/>
    internal sealed class SmtpMailService :
        IMailService,
        ITransientService
    {
        private readonly MailSettings _settings;

        /// <summary>
        /// Инициализирует экземпляр <see cref="SmtpMailService"/>.
        /// </summary>
        /// <param name="settings"><see cref="MailSettings"/>.</param>
        public SmtpMailService(
            IOptionsSnapshot<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        // TODO - добавить контроллер с методами

        /// <inheritdoc/>
        public async Task SendAsync(MailRequest request) // TODO - заменить на другое возвращаемое значение (Result) - проверить, что Hangfire работает?
        {
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = request.Body
            };
            if (request.Attachments?.Any() == true)
            {
                foreach (var file in request.Attachments)
                {
                    if (file.Length > 0)
                    {
                        byte[] fileBytes;
                        await using (var ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);
                            fileBytes = ms.ToArray();
                        }

                        bodyBuilder.Attachments.Add(
                            file.FileName,
                            fileBytes,
                            ContentType.TryParse(file.ContentType, out var contentType)
                                ? contentType
                                : ContentType.Parse(MediaTypeNames.Application.Octet));
                    }
                }
            }

            var email = new MimeMessage
            {
                Sender = new(_settings.DisplayName, request.From ?? _settings.From),
                Subject = request.Subject,
                Body = bodyBuilder.ToMessageBody()
            };
            email.To.AddRange(request.To.Select(MailboxAddress.Parse));
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTlsWhenAvailable); // TODO - исправить ошибку отправки email
            if (_settings.EnableAuthentication)
            {
                await smtp.AuthenticateAsync(_settings.UserName, _settings.Password);
            }

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        // TODO - добавить CRUD методы для шаблонов

        /// <inheritdoc/>
        public async Task SendTemplateEmailAsync(MailTemplateRequest request) // TODO - заменить на другое возвращаемое значение (Result) - проверить, что Hangfire работает?
        {
            string templateFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Templates", "Mail", $"{request.TemplateName}.html");
            if (!File.Exists(templateFilePath))
            {
                return; // TODO - кидать исключение
            }

            using var str = new StreamReader(templateFilePath);
            string mailText = await str.ReadToEndAsync();
            if (request.Substitutions != null)
            {
                foreach (string substitution in request.Substitutions.Keys)
                {
                    mailText = mailText.Replace(substitution, request.Substitutions[substitution]);
                }
            }

            var mailRequest = new MailRequest
            {
                Body = mailText,
                From = request.From,
                Subject = request.Subject,
                To = request.To
            };

            await SendAsync(mailRequest);
        }
    }
}