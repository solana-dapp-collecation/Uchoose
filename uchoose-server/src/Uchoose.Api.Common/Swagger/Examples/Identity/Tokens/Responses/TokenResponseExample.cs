// ------------------------------------------------------------------------------------------------------
// <copyright file="TokenResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.TokenService.Interfaces.Responses;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Identity.Tokens.Responses
{
    /// <summary>
    /// Пример ответа для получения/обновления токена авторизации.
    /// </summary>
    public class TokenResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<TokenResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="TokenResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public TokenResponseExample(IStringLocalizer<TokenResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<TokenResponse>.Success(
                new(_localizer["<Token value>"], _localizer["<Refresh token value>"], DateTime.UtcNow.AddDays(7)),
                _localizer["<Message>"]);
        }
    }
}