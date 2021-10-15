// ------------------------------------------------------------------------------------------------------
// <copyright file="ProfilePictureUrlResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Filters;
using Uchoose.Utils.Wrapper;

namespace Uchoose.Api.Common.Swagger.Examples.Identity.Accounts.Responses
{
    /// <summary>
    /// Пример ответа, возвращающего расположение изображения профиля пользователя или его идентификатор в файловом хранилище.
    /// </summary>
    public class ProfilePictureUrlResponseExample : IExamplesProvider<object>
    {
        private readonly IStringLocalizer<ProfilePictureUrlResponseExample> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="ProfilePictureUrlResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public ProfilePictureUrlResponseExample(IStringLocalizer<ProfilePictureUrlResponseExample> localizer)
        {
            _localizer = localizer;
        }

        /// <inheritdoc/>
        public object GetExamples()
        {
            return Result<string>.Success(_localizer["<Profile picture URL>"], _localizer["<Message>"]);
        }
    }
}