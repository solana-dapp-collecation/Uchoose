// ------------------------------------------------------------------------------------------------------
// <copyright file="IdentityService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Uchoose.Application.Constants.Role;
using Uchoose.Application.Constants.User;
using Uchoose.DateTimeService.Interfaces;
using Uchoose.Domain.Exceptions;
using Uchoose.Domain.Identity.Entities;
using Uchoose.Domain.Identity.Events.Users;
using Uchoose.Domain.Identity.Exceptions;
using Uchoose.IdentityService.Interfaces;
using Uchoose.IdentityService.Interfaces.Requests;
using Uchoose.MailService.Interfaces;
using Uchoose.MailService.Interfaces.Requests;
using Uchoose.MailService.Interfaces.Settings;
using Uchoose.SmsService.Interfaces;
using Uchoose.SmsService.Interfaces.Requests;
using Uchoose.SmsService.Interfaces.Settings;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Extensions;
using Uchoose.Utils.Wrapper;

namespace Uchoose.IdentityService
{
    /// <inheritdoc cref="IIdentityService"/>.
    internal sealed class IdentityService : IIdentityService, ITransientService
    {
        private readonly UserManager<UchooseUser> _userManager;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMailService _mailService;
        private readonly MailSettings _mailSettings;
        private readonly SmsSettings _smsSettings;
        private readonly ISmsService _smsService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IStringLocalizer<IdentityService> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="IdentityService"/>.
        /// </summary>
        /// <param name="userManager"><see cref="UserManager{T}"/>.</param>
        /// <param name="backgroundJobClient"><see cref="IBackgroundJobClient"/>.</param>
        /// <param name="mailService"><see cref="IMailService"/>.</param>
        /// <param name="mailSettings"><see cref="MailSettings"/>.</param>
        /// <param name="smsSettings"><see cref="SmsSettings"/>.</param>
        /// <param name="smsService"><see cref="ISmsService"/>.</param>
        /// <param name="dateTimeService"><see cref="IDateTimeService"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public IdentityService(
            UserManager<UchooseUser> userManager,
            IBackgroundJobClient backgroundJobClient,
            IMailService mailService,
            IOptionsSnapshot<MailSettings> mailSettings,
            IOptionsSnapshot<SmsSettings> smsSettings,
            ISmsService smsService,
            IDateTimeService dateTimeService,
            IStringLocalizer<IdentityService> localizer)
        {
            _userManager = userManager;
            _backgroundJobClient = backgroundJobClient;
            _mailService = mailService;
            _mailSettings = mailSettings.Value;
            _smsSettings = smsSettings.Value;
            _smsService = smsService;
            _dateTimeService = dateTimeService;
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public async Task<IResult<Guid>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                if (userWithSameUserName.IsSoftDeleted())
                {
                    throw new EntityIsDeletedException<Guid, UchooseUser>(_localizer);

                    // TODO - добавить своё исключение или зарегистрировать повторно
                }

                throw new IdentityException(string.Format(_localizer["Username '{0}' is already taken."], request.UserName));
            }

            var currentDateTime = _dateTimeService.NowUtc;
            var systemUserId = new Guid(UserConstants.SystemUserId);
            var user = new UchooseUser(request.UserName)
            {
                Email = request.Email,
                EmailConfirmed = request.EmailConfirmed,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                PhoneNumber = request.PhoneNumber,
                PhoneNumberConfirmed = request.PhoneNumberConfirmed,
                IsActive = request.ActivateUser,
                CreatedBy = systemUserId,
                CreatedOn = currentDateTime,
                LastModifiedBy = systemUserId,
                LastModifiedOn = currentDateTime
            };
            if (request.PhoneNumber.IsPresent())
            {
                var userWithSamePhoneNumber = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);
                if (userWithSamePhoneNumber != null)
                {
                    if (userWithSamePhoneNumber.IsSoftDeleted())
                    {
                        throw new EntityIsDeletedException<Guid, UchooseUser>(_localizer);

                        // TODO - добавить своё исключение или зарегистрировать повторно
                    }

                    throw new IdentityException(string.Format(_localizer["Phone number '{0}' is already registered."], request.PhoneNumber));
                }
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                user.AddDomainEvent(new UserRegisteredEvent(user, string.Format(_localizer["User '{0}' registered."], user.UserName)));
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    // TODO - нужен ли тут try-catch
                    try
                    {
                        await _userManager.AddToRoleAsync(user, RoleConstants.Partner);
                    }
                    catch
                    {
                        // ignore
                    }

                    if (!_mailSettings.EnableVerification && !_smsSettings.EnableVerification)
                    {
                        return await Result<Guid>.SuccessAsync(user.Id, message: string.Format(_localizer["User '{0}' registered."], user.UserName));
                    }

                    var messages = new List<string> { string.Format(_localizer["User '{0}' Registered."], user.UserName) };
                    if (_mailSettings.EnableVerification)
                    {
                        // send verification email
                        string emailVerificationUri = await GetEmailVerificationUriAsync(user, origin);
                        var mailRequest = new MailRequest
                        {
                            From = "Uchooseapplication@Uchoose.com",
                            To = new[] { user.Email },
                            Body = string.Format(_localizer["Please confirm your Uchoose account by <a href='{0}'>clicking here</a>."], emailVerificationUri),
                            Subject = _localizer["Confirm Registration"]
                        };
                        _backgroundJobClient.Enqueue(() => _mailService.SendAsync(mailRequest));

                        messages.Add(string.Format(_localizer["Please check '{0}' to verify your account!"], user.Email));
                    }

                    if (_smsSettings.EnableVerification)
                    {
                        // send verification sms
                        string mobilePhoneVerificationCode = await GetMobilePhoneVerificationCodeAsync(user);
                        var smsRequest = new SmsRequest
                        {
                            Number = user.PhoneNumber,
                            Message = string.Format(_localizer["Please confirm your Uchoose account by this code: {0}"], mobilePhoneVerificationCode)
                        };
                        _backgroundJobClient.Enqueue(() => _smsService.SendAsync(smsRequest));

                        messages.Add(string.Format(_localizer["Please check '{0}' for code in SMS to verify your account!"], user.PhoneNumber));
                    }

                    return await Result<Guid>.SuccessAsync(user.Id, messages: messages);
                }
                else
                {
                    throw new IdentityException(_localizer["Validation Errors Occurred."], result.Errors.Select(a => _localizer[a.Description].ToString()).ToList());
                }
            }
            else
            {
                if (userWithSameEmail.IsSoftDeleted())
                {
                    throw new EntityIsDeletedException<Guid, UchooseUser>(_localizer);

                    // TODO - добавить своё исключение или зарегистрировать повторно
                }

                throw new IdentityException(string.Format(_localizer["Email '{0}' is already registered."], request.Email));
            }
        }

        /// <inheritdoc/>
        public async Task<IResult<Guid>> ConfirmEmailAsync(Guid userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new IdentityException(_localizer["An error occurred while confirming E-Mail."]);
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                if (user.PhoneNumberConfirmed || !_smsSettings.EnableVerification)
                {
                    return await Result<Guid>.SuccessAsync(user.Id, string.Format(_localizer["Account Confirmed for E-Mail '{0}'. You can now use the /api/identity/token endpoint to generate JWT."], user.Email));
                }
                else
                {
                    return await Result<Guid>.SuccessAsync(user.Id, string.Format(_localizer["Account Confirmed for E-Mail '{0}'. You should confirm your Phone Number before using the /api/identity/token endpoint to generate JWT."], user.Email));
                }
            }
            else
            {
                throw new IdentityException(string.Format(_localizer["An error occurred while confirming '{0}'"], user.Email));
            }
        }

        /// <inheritdoc/>
        public async Task<IResult<Guid>> ConfirmPhoneNumberAsync(Guid userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new IdentityException(_localizer["An error occurred while confirming Mobile Phone."]);
            }

            var result = await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, code);
            if (result.Succeeded)
            {
                if (user.EmailConfirmed)
                {
                    return await Result<Guid>.SuccessAsync(user.Id, string.Format(_localizer["Account Confirmed for Phone Number '{0}'. You can now use the /api/identity/token endpoint to generate JWT."], user.PhoneNumber));
                }
                else
                {
                    return await Result<Guid>.SuccessAsync(user.Id, string.Format(_localizer["Account Confirmed for Phone Number '{0}'. You should confirm your E-mail before using the /api/identity/token endpoint to generate JWT."], user.PhoneNumber));
                }
            }
            else
            {
                throw new IdentityException(string.Format(_localizer["An error occurred while confirming '{0}'"], user.PhoneNumber));
            }
        }

        /// <inheritdoc/>
        public async Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                throw new IdentityException(_localizer["An Error has occurred!"]);
            }

            // For more information on how to enable account confirmation and password reset please
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            const string route = "account/password/reset"; // TODO: вынести в константы (учитывать версию API)
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            string passwordResetUrl = QueryHelpers.AddQueryString(endpointUri.ToString(), "Token", code);
            var mailRequest = new MailRequest
            {
                Body = string.Format(_localizer["Please reset your password by <a href='{0}'>clicking here</a>."], HtmlEncoder.Default.Encode(passwordResetUrl)),
                Subject = _localizer["Reset Password"],
                To = new[] { request.Email }
            };
            _backgroundJobClient.Enqueue(() => _mailService.SendAsync(mailRequest));
            return await Result.SuccessAsync(_localizer["Password Reset Mail has been sent to your authorized Email."]);
        }

        /// <inheritdoc/>
        public async Task<IResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                throw new IdentityException(_localizer["An Error has occurred!"]);
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (result.Succeeded)
            {
                return await Result.SuccessAsync(_localizer["Password Reset Successful!"]);
            }
            else
            {
                throw new IdentityException(_localizer["An Error has occurred!"]);
            }
        }

        /// <summary>
        /// Получить Uri для подтверждения email.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="origin">Заголовок запроса Origin.</param>
        /// <returns>Возвращает Uri для подтверждения email.</returns>
        private async Task<string> GetEmailVerificationUriAsync(UchooseUser user, string origin)
        {
            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            const string route = "api/v1/identity/email/confirm"; // TODO: вынести в константы (учитывать версию API)
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            string verificationUri = QueryHelpers.AddQueryString(endpointUri.ToString(), "userId", user.Id.ToString());
            return QueryHelpers.AddQueryString(verificationUri, "code", code);
        }

        /// <summary>
        /// Получить Uri для подтверждения номера телефона.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>Возвращает Uri для подтверждения номера телефона.</returns>
        private async Task<string> GetMobilePhoneVerificationCodeAsync(UchooseUser user)
        {
            return await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
        }
    }
}