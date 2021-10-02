﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="IHasVersion.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Uchoose.Utils.Contracts.Properties
{
    /// <summary>
    /// Имеет версию.
    /// </summary>
    public interface IHasVersion
    {
        /// <summary>
        /// Версия.
        /// </summary>
        int Version { get; }
    }
}