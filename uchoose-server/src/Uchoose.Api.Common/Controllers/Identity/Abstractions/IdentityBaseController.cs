// ------------------------------------------------------------------------------------------------------
// <copyright file="IdentityBaseController.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Uchoose.Api.Common.Controllers.Abstractions;

namespace Uchoose.Api.Common.Controllers.Identity.Abstractions
{
    /// <summary>
    /// Базовый контроллер Identity.
    /// </summary>
    [ApiController]
    [Route(BasePath + "/[controller]")]
    public abstract class IdentityBaseController :
        BaseController
    {
        /// <summary>
        /// Базовый путь для роутинга.
        /// </summary>
        protected internal new const string BasePath = "api/v{version:apiVersion}/identity";

        /// <summary>
        /// Общий тэг для операций, связанных с Identity.
        /// </summary>
        protected internal const string IdentityTag = "Identity";

        /// <summary>
        /// Общий тэг для операций, связанных с пользователями.
        /// </summary>
        protected internal const string UsersTag = "Users";

        /// <summary>
        /// Общий тэг для операций, связанных с ролями пользователей.
        /// </summary>
        protected internal const string RolesTag = "Roles";

        /// <summary>
        /// Общий тэг для операций, связанных с авторизационными токенами пользователей.
        /// </summary>
        protected internal const string TokensTag = "Tokens";
    }
}