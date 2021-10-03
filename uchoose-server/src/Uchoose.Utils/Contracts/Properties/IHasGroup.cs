// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasGroup.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Utils.Contracts.Properties
{
    /// <summary>
    /// Имеет свойство с именем Group.
    /// </summary>
    public interface IHasGroup :
        IHasProperty
    {
        /// <summary>
        /// Группа.
        /// </summary>
        string Group { get; set; }
    }
}