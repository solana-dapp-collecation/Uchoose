// ------------------------------------------------------------------------------------------------------
// <copyright file="LanguageCode.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Api.Common.Models
{
    /// <summary>
    /// Код языка.
    /// </summary>
    public class LanguageCode
    {
        /// <summary>
        /// Отображаемое имя.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Код.
        /// </summary>
        public string Code { get; set; }
    }
}