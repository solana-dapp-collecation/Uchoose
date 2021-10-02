// ------------------------------------------------------------------------------------------------------
// <copyright file="IdentitySettings.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Identity;
using Uchoose.Utils.Contracts.Common;

namespace Uchoose.DataAccess.PostgreSql.Identity.Settings
{
    /// <summary>
    /// Настройки Identity.
    /// </summary>
    public class IdentitySettings :
        ISettings
    {
        /// <summary>
        /// <see cref="IdentityOptions"/>.
        /// </summary>
        public IdentityOptions Options { get; set; }
    }
}