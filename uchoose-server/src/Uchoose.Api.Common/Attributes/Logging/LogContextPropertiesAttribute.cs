// ------------------------------------------------------------------------------------------------------
// <copyright file="LogContextPropertiesAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog.Context;
using Uchoose.CurrentUserService.Interfaces;
using Uchoose.Utils.Extensions;

// ReSharper disable NonConstantEqualityExpressionHasConstantResult

namespace Uchoose.Api.Common.Attributes.Logging
{
    /// <summary>
    /// Логируемые свойства.
    /// </summary>
    [Flags]
    public enum LogContextProperties
    {
        /// <summary>
        /// Убрать добавленные ранее свойства.
        /// </summary>
        None = 0,

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        UserId = 1,

        /// <summary>
        /// Email пользователя.
        /// </summary>
        UserEmail = 2,

        /// <summary>
        /// Ip адрес пользователя.
        /// </summary>
        UserIp = 4
    }

    /// <summary>
    /// Атрибут для добавления свойств в <see cref="LogContext"/>.
    /// </summary>
    /// <remarks>
    /// Используется, когда необходимо добавить в структурный лог нужную информацию.
    /// </remarks>
    public class LogContextPropertiesAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="LogContextPropertiesAttribute"/>.
        /// </summary>
        /// <param name="properties">Логируемые свойства <see cref="LogContextProperties"/>.</param>
        public LogContextPropertiesAttribute(LogContextProperties properties)
            : base(typeof(LogContextFilter))
        {
            Arguments = new object[] { properties };
        }

        /// <summary>
        /// Фильтр для добавления свойств в <see cref="LogContext"/>.
        /// </summary>
        internal class LogContextFilter : ActionFilterAttribute
        {
            /// <summary>
            /// Инициализирует экземпляр <see cref="LogContextFilter"/>.
            /// </summary>
            /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
            /// <param name="properties"><see cref="LogContextProperties"/>.</param>
            public LogContextFilter(
                ICurrentUserService currentUserService,
                LogContextProperties properties)
            {
                if (properties == LogContextProperties.None)
                {
                    LogContext.Reset();
                }

                if ((properties & LogContextProperties.UserId) != 0)
                {
                    LogContext.PushProperty(nameof(LogContextProperties.UserId), currentUserService.GetUserId());
                }

                if ((properties & LogContextProperties.UserEmail) != 0)
                {
                    LogContext.PushProperty(nameof(LogContextProperties.UserEmail), currentUserService.GetUserEmail());
                }

                if ((properties & LogContextProperties.UserIp) != 0)
                {
                    LogContext.PushProperty(nameof(LogContextProperties.UserIp), currentUserService.GetHttpContext()?.GetRequestIpAddress());
                }
            }
        }
    }
}