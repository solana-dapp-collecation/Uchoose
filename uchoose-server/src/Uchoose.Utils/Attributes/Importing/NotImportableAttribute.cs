// ------------------------------------------------------------------------------------------------------
// <copyright file="NotImportableAttribute.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;

namespace Uchoose.Utils.Attributes.Importing
{
    /// <summary>
    /// Атрибут, указывающий, что значение свойства класса (или класс/структура) не может быть импортировано из файла.
    /// </summary>
    /// <remarks>
    /// Используется при импорте данных из файла в сущность.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class NotImportableAttribute : Attribute
    {
    }
}