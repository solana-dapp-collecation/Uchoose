// ------------------------------------------------------------------------------------------------------
// <copyright file="MetadataAccountLayout.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#pragma warning disable SA1303 // Const field names should begin with upper-case letter

// ReSharper disable once CheckNamespace
namespace Solnet.Metaplex
{
    internal class MetadataAccountLayout
    {
        internal const int MethodOffset = 0;

        internal const int nameOffset = 65; // 69

        internal const int symbolOffset = 101; // 105

        internal const int uriOffset = 115;

        internal const int feeBasisOffset = 319;

        internal const int creatorsOffset = 322;
    }
}