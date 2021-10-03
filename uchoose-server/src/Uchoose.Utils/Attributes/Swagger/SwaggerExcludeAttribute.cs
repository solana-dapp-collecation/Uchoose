// ------------------------------------------------------------------------------------------------------
// <copyright file="SwaggerExcludeAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.Utils.Attributes.Swagger
{
    /// <summary>
    /// Атрибут, указывающий, что свойство или параметр должно игнорироваться в swagger документации.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public sealed class SwaggerExcludeAttribute : Attribute
    {
    }
}