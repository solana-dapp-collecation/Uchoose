// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeWithGuidEntityIdResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Uchoose.Api.Common.Swagger.Examples.Common.ExtendedAttributes.Responses.Abstractions;

namespace Uchoose.Api.Common.Swagger.Examples.Common.ExtendedAttributes.Responses
{
    /// <inheritdoc/>
    internal class ExtendedAttributeWithGuidEntityIdResponseExample : ExtendedAttributeResponseExample<Guid>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="ExtendedAttributeWithGuidEntityIdResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        public ExtendedAttributeWithGuidEntityIdResponseExample(IStringLocalizer<ExtendedAttributeResponseExample> localizer)
            : base(localizer)
        {
        }

        /// <inheritdoc/>
        public override Guid GetDefaultEntityId()
        {
            return Guid.Empty;
        }
    }
}