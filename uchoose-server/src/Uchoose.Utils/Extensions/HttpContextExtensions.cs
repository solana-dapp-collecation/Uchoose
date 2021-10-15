// ------------------------------------------------------------------------------------------------------
// <copyright file="HttpContextExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Uchoose.Utils.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="HttpContext"/>.
    /// </summary>
    public static class HttpContextExtensions
    {
        private const string XForwardedForHeader = "X-Forwarded-For";

        private const string CorrelationIdHeader = "correlationId";

        /// <summary>
        /// Получить идентификатор корреляции.
        /// </summary>
        /// <param name="httpContext"><see cref="HttpContext"/>.</param>
        /// <returns>Возвращает идентификатор корреляции из запроса в виде строки.</returns>
        public static string GetCorrelationId(this HttpContext httpContext)
        {
            if (httpContext?.Request.Headers.ContainsKey(CorrelationIdHeader) == true)
            {
                return httpContext.Request?.Headers[CorrelationIdHeader];
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Получить ip адрес пользователя, вызывающего метод.
        /// </summary>
        /// <param name="httpContext"><see cref="HttpContext"/>.</param>
        /// <returns>Возвращает ip адрес пользователя, вызывающего метод, в виде строки.</returns>
        public static string GetRequestUserAgent(this HttpContext httpContext)
        {
            if (httpContext?.Request.Headers.ContainsKey(HeaderNames.UserAgent) == true)
            {
                return httpContext.Request?.Headers[HeaderNames.UserAgent];
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Получить ip адрес пользователя, вызывающего метод.
        /// </summary>
        /// <param name="httpContext"><see cref="HttpContext"/>.</param>
        /// <returns>Возвращает ip адрес пользователя, вызывающего метод, в виде строки.</returns>
        public static string GetRequestIpAddress(this HttpContext httpContext)
        {
            if (httpContext?.Request.Headers.ContainsKey(XForwardedForHeader) == true)
            {
                return GetIpAddressFromProxy(httpContext.Request?.Headers[XForwardedForHeader]);
            }
            else
            {
                return httpContext?.Connection.RemoteIpAddress?.MapToIPv4().ToString();
            }
        }

        private static string GetIpAddressFromProxy(string proxifiedIpList)
        {
            string[] addresses = proxifiedIpList.Split(',');
            if (addresses.Length != 0)
            {
                // If IP contains port, it will be after the last : (IPv6 uses : as delimiter and could have more of them)
                return addresses[0].Contains(":")
                    ? addresses[0].Substring(0, addresses[0].LastIndexOf(":", StringComparison.Ordinal))
                    : addresses[0];
            }

            return string.Empty;
        }
    }
}