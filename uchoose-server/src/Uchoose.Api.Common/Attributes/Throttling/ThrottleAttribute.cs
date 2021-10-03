// ------------------------------------------------------------------------------------------------------
// <copyright file="ThrottleAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Net;

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Net.Http.Headers;
using Uchoose.CurrentUserService.Interfaces;
using Uchoose.Utils.Enums;
using Uchoose.Utils.Exceptions;
using Uchoose.Utils.Extensions;

namespace Uchoose.Api.Common.Attributes.Throttling
{
    /// <summary>
    /// Условие применения ограничения количества запросов.
    /// </summary>
    public enum ThrottleLimitBy : byte
    {
        /// <summary>
        /// Не использовать.
        /// </summary>
        None = 0,

        /// <summary>
        /// По Ip адресу пользователя, вызывающего метод.
        /// </summary>
        Ip = 1,

        /// <summary>
        /// По идентификатору авторизованного пользователя, вызывающего метод.
        /// </summary>
        Id = 2,

        /// <summary>
        /// По Ip адресу и идентификатору авторизованного пользователя, вызывающего метод.
        /// </summary>
        IpAndId = 3
    }

    /// <summary>
    /// Атрибут, указывающий, какое количество запросов к методу можно сделать в единицу времени (с учётом множителя).
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ThrottleAttribute :
        ActionFilterAttribute
    {
        /// <summary>
        /// Шаблон сообщения по умолчанию, возвращаемого в случае достижения ограничения.
        /// </summary>
        public const string DefaultMessageTemplate = "Вам разрешено делать только {AttemptsLimit} запрос(ов) в {TimeUnitMultiplier} {TimeUnit}.";

        /// <summary>
        /// Заголовок HTTP ответа, который показывает, как долго пользователь
        /// должен подождать перед последующим запросом.
        /// </summary>
        public static string RetryAfterHeader => HeaderNames.RetryAfter;

        /// <summary>
        /// Уникальное имя.
        /// </summary>
        /// <remarks>
        /// По умолчанию - ThrottleAttribute.
        /// </remarks>
        public string Name { get; set; } = nameof(ThrottleAttribute);

        /// <summary>
        /// Единица времени.
        /// </summary>
        public TimeUnit TimeUnit { get; set; } = TimeUnit.Second;

        /// <summary>
        /// Множитель для единицы времени.
        /// </summary>
        /// <remarks>
        /// По умолчанию - 1.
        /// </remarks>
        public double TimeUnitMultiplier { get; set; } = 1;

        /// <summary>
        /// Ограничение по количеству вызовов метода в единицу времени.
        /// </summary>
        /// <remarks>
        /// По умолчанию - 1000.
        /// </remarks>
        public uint RequestsLimit { get; set; } = 1000;

        /// <summary>
        /// Шаблон сообщения, возвращаемого в случае достижения ограничения.
        /// </summary>
        /// <remarks>
        /// В шаблоне можно использовать следующие элементы, которые будут заменены соответствующими значениями:
        /// <para>{AttemptsLimit} - ограничение по количеству вызовов метода в единицу времени.</para>
        /// <para>{TimeUnit} - единица времени.</para>
        /// <para>{TimeUnitMultiplier} - множитель для единицы времени.</para>
        /// </remarks>
        public string MessageTemplate { get; set; } = DefaultMessageTemplate;

        /// <summary>
        /// Условие применения ограничения количества запросов.
        /// </summary>
        /// <remarks>
        /// По умолчанию - None.
        /// </remarks>
        public ThrottleLimitBy LimitBy { get; set; }

        /// <inheritdoc/>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            double seconds = Convert.ToInt32(TimeUnit) * TimeUnitMultiplier;
            var controller = filterContext.ActionDescriptor as ControllerActionDescriptor;

            string key = string.Join(
                "-",
                Name,
                seconds,
                filterContext.HttpContext.Request.Method,
                controller?.ControllerName,
                controller?.ActionName);

            var currentUserService = filterContext.HttpContext.RequestServices.GetService(typeof(ICurrentUserService)) as ICurrentUserService;
            switch (LimitBy)
            {
                case ThrottleLimitBy.None:
                    break;
                case ThrottleLimitBy.Ip:
                    key = string.Join(
                        "-",
                        key,
                        filterContext.HttpContext.GetRequestIpAddress());
                    break;
                case ThrottleLimitBy.Id:
                    key = string.Join(
                        "-",
                        key,
                        currentUserService?.GetUserId());
                    break;
                case ThrottleLimitBy.IpAndId:
                    key = string.Join(
                        "-",
                        key,
                        filterContext.HttpContext.GetRequestIpAddress(),
                        currentUserService?.GetUserId());
                    break;
            }

            uint count;
            var cache = filterContext.HttpContext.RequestServices.GetService(typeof(IMemoryCache)) as IMemoryCache;
            object cacheValue = cache.Get(key);
            var now = DateTimeOffset.UtcNow;
            if (cacheValue == null)
            {
                count = 1;
                cache?.Set($"{key}-time", now.AddSeconds(seconds), new MemoryCacheEntryOptions { AbsoluteExpiration = now.AddSeconds(seconds), Priority = CacheItemPriority.Low });
                cache?.Set(key, count, new MemoryCacheEntryOptions { AbsoluteExpiration = now.AddSeconds(seconds), Priority = CacheItemPriority.Low });
            }
            else
            {
                count = (uint)cacheValue + 1;
                object cacheTimeValue = cache.Get($"{key}-time");
                if (cacheTimeValue != null)
                {
                    cache?.Set(key, count, new MemoryCacheEntryOptions { AbsoluteExpiration = (DateTimeOffset)cacheTimeValue, Priority = CacheItemPriority.Low });
                }
                else
                {
                    cache?.Set($"{key}-time", now.AddSeconds(seconds), new MemoryCacheEntryOptions { AbsoluteExpiration = now.AddSeconds(seconds), Priority = CacheItemPriority.Low });
                    cache?.Set(key, count, new MemoryCacheEntryOptions { AbsoluteExpiration = now.AddSeconds(seconds), Priority = CacheItemPriority.Low });
                }
            }

            if (count > RequestsLimit)
            {
                var localizer = filterContext.HttpContext.RequestServices.GetService(typeof(IStringLocalizer<ThrottleAttribute>)) as IStringLocalizer<ThrottleAttribute>;
                string message = !string.IsNullOrWhiteSpace(MessageTemplate)
                    ? (localizer != null ? localizer[MessageTemplate].ToString() : MessageTemplate)
                        .Replace("{AttemptsLimit}", RequestsLimit.ToString())
                        .Replace("{TimeUnit}", TimeUnit.ToDescriptionString())
                        .Replace("{TimeUnitMultiplier}", TimeUnitMultiplier.ToString(CultureInfo.InvariantCulture))
                    : (localizer != null ? localizer![DefaultMessageTemplate].ToString() : DefaultMessageTemplate)
                        .Replace("{AttemptsLimit}", RequestsLimit.ToString())
                        .Replace("{TimeUnit}", TimeUnit.ToDescriptionString())
                        .Replace("{TimeUnitMultiplier}", TimeUnitMultiplier.ToString(CultureInfo.InvariantCulture));

                object cacheTimeValue = cache.Get($"{key}-time");
                if (cacheTimeValue != null)
                {
                    uint retryAfter = (uint)((DateTimeOffset)cacheTimeValue - DateTimeOffset.UtcNow).TotalSeconds;
                    filterContext.HttpContext.Response.Headers.Add(RetryAfterHeader, retryAfter.ToString(CultureInfo.CurrentCulture));
                }

                throw new CustomException(message, statusCode: HttpStatusCode.TooManyRequests);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}