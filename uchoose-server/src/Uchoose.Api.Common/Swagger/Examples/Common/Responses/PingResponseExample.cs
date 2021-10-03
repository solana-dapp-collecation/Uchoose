// ------------------------------------------------------------------------------------------------------
// <copyright file="PingResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Swashbuckle.AspNetCore.Filters;

namespace Uchoose.Api.Common.Swagger.Examples.Common.Responses
{
    /// <summary>
    /// Пример ответа для методов проверки Ping.
    /// </summary>
    public class PingResponseExample : IExamplesProvider<object>
    {
        /// <inheritdoc/>
        public object GetExamples()
        {
            return "Ok";
        }
    }
}