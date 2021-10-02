// ------------------------------------------------------------------------------------------------------
// <copyright file="ProtectionSettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Uchoose.Utils.Contracts.Common;

namespace Uchoose.DataAccess.Interfaces.Settings
{
    /// <summary>
    /// Настройки защиты данных.
    /// </summary>
    public class ProtectionSettings :
        ISettings
    {
        /// <summary>
        /// Защитить персональные данные.
        /// </summary>
        public bool ProtectPersonalData { get; set; }

        /// <summary>
        /// Защитить ключи шифрования с помощью сертификата <seealso cref="CertificateSubjectName"/>.
        /// </summary>
        public bool ProtectKeysWithCertificate { get; set; }

        /// <summary>
        /// Шаблон SubjectName для поиска сертификата безопасности.
        /// </summary>
        public string CertificateSubjectName { get; set; }

        /// <summary>
        /// Идентификатор, который уникальным образом разграничит
        /// это приложение от всех других приложений на компьютере.
        /// </summary>
        public string ApplicationDiscriminator { get; set; }

        /// <summary>
        /// Срок действия ключа шифрования по умолчанию в днях.
        /// </summary>
        public double DefaultKeyLifetimeInDays { get; set; }

        /// <summary>
        /// Исключённые из обработки свойства сущностей.
        /// </summary>
        /// <remarks>
        /// Key - имя типа сущности, value - список исключённых имён свойств.
        /// </remarks>
        public Dictionary<string, List<string>> ExcludedEntityProperties { get; set; } = new();
    }
}