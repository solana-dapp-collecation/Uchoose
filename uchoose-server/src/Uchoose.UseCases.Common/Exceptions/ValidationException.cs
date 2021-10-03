// ------------------------------------------------------------------------------------------------------
// <copyright file="ValidationException.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;

using Microsoft.Extensions.Localization;
using Uchoose.Utils.Exceptions;

namespace Uchoose.UseCases.Common.Exceptions
{
    /// <summary>
    /// Исключение валидации данных.
    /// </summary>
    public class ValidationException : CustomException
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="ValidationException"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        /// <param name="errors">Список ошибок.</param>
        public ValidationException(IStringLocalizer localizer, List<string> errors)
            : base(localizer["Validation Failures Occurred."], errors, HttpStatusCode.BadRequest)
        {
        }
    }
}