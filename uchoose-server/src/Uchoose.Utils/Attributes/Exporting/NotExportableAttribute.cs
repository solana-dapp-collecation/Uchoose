// ------------------------------------------------------------------------------------------------------
// <copyright file="NotExportableAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.Utils.Attributes.Exporting
{
    /// <summary>
    /// Атрибут, указывающий, что значение свойства класса (или класс/структура) не может быть экспортировано в файл.
    /// </summary>
    /// <remarks>
    /// Используется при экспорте данных из сущности в файл.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class NotExportableAttribute : Attribute
    {
    }
}