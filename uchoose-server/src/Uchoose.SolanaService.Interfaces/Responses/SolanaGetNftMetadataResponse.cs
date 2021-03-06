// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaGetNftMetadataResponse.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Solnet.Metaplex;
using Uchoose.SolanaService.Interfaces.Models;

namespace Uchoose.SolanaService.Interfaces.Responses
{
    /// <summary>
    /// Ответ с метаданными NFT.
    /// </summary>
    public class SolanaGetNftMetadataResponse
    {
        /// <summary>
        /// Владелец NFT.
        /// </summary>
        /// <example>559u...ZD9z</example>
        public string Owner { get; }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <example>621s...fN12</example>
        public string UpdateAuthority { get; }

        /// <summary>
        /// TODO.
        /// </summary>
        public string Mint { get; }

        /// <summary>
        /// Наименование NFT.
        /// </summary>
        /// <example>NFTs #1</example>
        public string Name { get; }

        /// <summary>
        /// Символьное имя NFT.
        /// </summary>
        /// <example>NFT1</example>
        public string Symbol { get; }

        /// <summary>
        /// TODO.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// TODO.
        /// </summary>
        public uint SellerFeeBasisPoints { get; }

        /// <summary>
        /// Список создателей NFT.
        /// </summary>
        public IEnumerable<SolanaNftCreatorMetadataModel> Creators { get; }

        /// <summary>
        /// Инициализирует экземпляр <see cref="SolanaGetNftMetadataResponse"/>.
        /// </summary>
        /// <param name="metadataAccount">Аккаунт с метаданными.</param>
        public SolanaGetNftMetadataResponse(MetadataAccount metadataAccount) // TODO - replace with Solnet.Metaplex nuget when published
        {
            if (metadataAccount == null)
            {
                throw new ArgumentNullException(nameof(metadataAccount));
            }

            Owner = metadataAccount.owner.Key;
            UpdateAuthority = metadataAccount.updateAuthority.Key;
            Mint = metadataAccount.mint;
            Name = metadataAccount.data.name;
            Symbol = metadataAccount.data.symbol;
            Url = metadataAccount.data.uri;
            SellerFeeBasisPoints = metadataAccount.data.sellerFeeBasisPoints;
            Creators = metadataAccount.data.creators.Select(x => new SolanaNftCreatorMetadataModel(x));
        }
    }
}