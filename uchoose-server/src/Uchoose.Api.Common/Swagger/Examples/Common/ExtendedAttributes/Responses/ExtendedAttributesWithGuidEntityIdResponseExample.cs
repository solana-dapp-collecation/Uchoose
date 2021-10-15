// ------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributesWithGuidEntityIdResponseExample.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Extensions.Localization;
using Uchoose.Api.Common.Swagger.Examples.Common.ExtendedAttributes.Responses.Abstractions;

namespace Uchoose.Api.Common.Swagger.Examples.Common.ExtendedAttributes.Responses
{
    /// <inheritdoc/>
    internal class ExtendedAttributesWithGuidEntityIdResponseExample : ExtendedAttributesResponseExample<Guid>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="ExtendedAttributesWithGuidEntityIdResponseExample"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        public ExtendedAttributesWithGuidEntityIdResponseExample(IStringLocalizer<ExtendedAttributesResponseExample> localizer)
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