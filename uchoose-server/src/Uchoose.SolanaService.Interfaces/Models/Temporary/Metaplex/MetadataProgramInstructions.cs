// ------------------------------------------------------------------------------------------------------
// <copyright file="MetadataProgramInstructions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

// ReSharper disable UnusedMember.Global
#pragma warning disable 1591

// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace
namespace Solnet.Metaplex // TODO - replace with Solnet.Metaplex nuget when published
{
    internal static class MetadataProgramInstructions
    {
        /// <summary>
        /// Represents the user-friendly names for the instruction types for the <see cref="MetadataProgram"/>.
        /// </summary>
        internal static readonly Dictionary<Values, string> Names = new()
        {
            { Values.CreateMetadataAccount, "Create MetadataAccount" },
            { Values.UpdateMetadataAccount, "Update MetadataAccount" },
            { Values.DeprecatedCreateMasterEdition, "Create MasterEdition (deprecated) " },
            { Values.DeprecatedMintNewEditionFromMasterEditionViaPrintingToken, "Mint new Edition from MasterEdition via PrintingToken (deprecated)" },
            { Values.UpdatePrimarySaleHappenedViaToken, "Update PrimarySaleHappened" },
            { Values.DeprecatedSetReservationList, "Set ReservationList (deprecated)" },
            { Values.DeprecatedCreateReservationList, "Create Reservation List (deprecated)" },
            { Values.SignMetadata, "Sign Metadata" },
            { Values.DeprecatedMintPrintingTokensViaToken, "Mint PrintingTokens via token (deprecated)" },
            { Values.DeprecatedMintPrintingTokens, "Mint PrintingTokens (deprecated)" },
            { Values.CreateMasterEdition, "Create MasterEdition" },
            { Values.MintNewEditionFromMasterEditionViaToken, "Mint new Edition from MasterEdition via token" },
            { Values.ConvertMasterEditionV1ToV2, "Convert Master Edition from V1 to V2" },
            { Values.MintNewEditionFromMasterEditionViaVaultProxy, "Mint new Edition from MasterEdition via VaultProxy" },
            { Values.PuffMetadata, "Puff metadata" }
        };

        /// <summary>
        /// Represents the instruction types for the <see cref="MetadataProgram"/>.
        /// </summary>
        internal enum Values : byte
        {
            /// <summary>
            /// .
            /// </summary>
            CreateMetadataAccount = 0,

            /// <summary>
            /// .
            /// </summary>
            UpdateMetadataAccount = 1,

            /// <summary>
            /// .
            /// </summary>
            DeprecatedCreateMasterEdition = 2,

            /// <summary>
            /// .
            /// </summary>
            DeprecatedMintNewEditionFromMasterEditionViaPrintingToken = 3,

            /// <summary>
            /// .
            /// </summary>
            UpdatePrimarySaleHappenedViaToken = 4,

            /// <summary>
            /// .
            /// </summary>
            DeprecatedSetReservationList = 5,

            /// <summary>
            /// .
            /// </summary>
            DeprecatedCreateReservationList = 6,

            /// <summary>
            /// .
            /// </summary>
            SignMetadata = 7,

            /// <summary>
            /// .
            /// </summary>
            DeprecatedMintPrintingTokensViaToken = 8,

            /// <summary>
            /// .
            /// </summary>
            DeprecatedMintPrintingTokens = 9,

            /// <summary>
            /// .
            /// </summary>
            CreateMasterEdition = 10,

            /// <summary>
            /// .
            /// </summary>
            MintNewEditionFromMasterEditionViaToken = 11,

            /// <summary>
            /// .
            /// </summary>
            ConvertMasterEditionV1ToV2 = 12,

            /// <summary>
            /// .
            /// </summary>
            MintNewEditionFromMasterEditionViaVaultProxy = 13,

            /// <summary>
            /// .
            /// </summary>
            PuffMetadata = 14
        }
    }
}