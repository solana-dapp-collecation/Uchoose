// ------------------------------------------------------------------------------------------------------
// <copyright file="NotLoggedAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.Utils.Attributes.Logging
{
    /// <summary>
    /// Атрибут, указывающий, что класс или свойство не должно логироваться.
    /// </summary>
    /// <remarks>
    /// Используется в посреднике для логирования запросов при вызове их обработчиков.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class NotLoggedAttribute : Attribute
    {
    }
}