// ------------------------------------------------------------------------------------------------------
// <copyright file="PersonalDataProtector.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Uchoose.DataAccess.Interfaces.Settings;

namespace Uchoose.DataAccess.PostgreSql.Protection
{
    /// <summary>
    /// Защитник персональных данных.
    /// </summary>
    public class PersonalDataProtector :
        IPersonalDataProtector
    {
        private readonly IDataProtector _protector;

        /// <summary>
        /// Инициализирует экземпляр <see cref="PersonalDataProtector"/>.
        /// </summary>
        /// <param name="protectionSettings"><see cref="ProtectionSettings"/>.</param>
        /// <param name="dataProtectionProvider"><see cref="IDataProtectionProvider"/>.</param>
        public PersonalDataProtector(
            IOptionsSnapshot<ProtectionSettings> protectionSettings,
            IDataProtectionProvider dataProtectionProvider)
        {
            _protector = dataProtectionProvider.CreateProtector(protectionSettings.Value.ApplicationDiscriminator);
        }

        /// <summary>
        /// Зашифровать персональные данные.
        /// </summary>
        /// <param name="data">Не защищённые персональные данные в виде строки.</param>
        /// <returns>Возвращает защищённые персональные данные в виде строки.</returns>
        public string Protect(string data)
            => _protector.Protect(data);

        /// <summary>
        /// Расшифровать персональные данные.
        /// </summary>
        /// <param name="data">Защищённые персональные данные в виде строки.</param>
        /// <returns>Возвращает не защищённые персональные данные в виде строки.</returns>
        public string Unprotect(string data)
            => _protector.Unprotect(data);
    }
}