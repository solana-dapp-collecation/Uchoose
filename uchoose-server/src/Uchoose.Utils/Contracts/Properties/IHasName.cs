// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasName.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Utils.Contracts.Properties
{
    /// <summary>
    /// Имеет свойство с именем Name.
    /// </summary>
    public interface IHasName :
        IHasProperty
    {
        /// <summary>
        /// Наименование (имя).
        /// </summary>
        string Name { get; }
    }
}