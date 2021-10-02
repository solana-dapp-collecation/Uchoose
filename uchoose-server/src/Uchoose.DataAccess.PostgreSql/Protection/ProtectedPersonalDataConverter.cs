// ------------------------------------------------------------------------------------------------------
// <copyright file="ProtectedPersonalDataConverter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Uchoose.DataAccess.PostgreSql.Protection
{
    /// <summary>
    /// Конвертер значений для защиты персональных данных с помощью <see cref="IPersonalDataProtector"/>.
    /// </summary>
    public class ProtectedPersonalDataConverter :
        ValueConverter<string, string>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="ProtectedPersonalDataConverter"/>.
        /// </summary>
        /// <param name="protector"><see cref="IPersonalDataProtector"/>.</param>
        public ProtectedPersonalDataConverter(IPersonalDataProtector protector)
            : base(
                s => protector.Protect(s),
                s => protector.Unprotect(s))
        {
        }
    }
}