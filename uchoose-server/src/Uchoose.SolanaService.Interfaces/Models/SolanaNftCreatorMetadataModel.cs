// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaNftCreatorMetadataModel.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Solnet.Metaplex;

namespace Uchoose.SolanaService.Interfaces.Models
{
    /// <summary>
    /// Модель данных с метаданными создателя NFT.
    /// </summary>
    public sealed class SolanaNftCreatorMetadataModel
    {
        /// <summary>
        /// Публичный ключ.
        /// </summary>
        public string PublicKey { get; }

        /// <summary>
        /// Верифицирован.
        /// </summary>
        public bool Verified { get; }

        /// <summary>
        /// TODO.
        /// </summary>
        public byte Share { get; }

        /// <summary>
        /// Инициализирует экземпляр <see cref="SolanaNftCreatorMetadataModel"/>.
        /// </summary>
        /// <param name="creator">Создатель NFT.</param>
        public SolanaNftCreatorMetadataModel(Creator creator)
        {
            if (creator == null)
            {
                return;
            }

            PublicKey = creator.key.Key;
            Verified = creator.verified;
            Share = creator.share;
        }
    }
}