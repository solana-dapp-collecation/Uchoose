// ------------------------------------------------------------------------------------------------------
// <copyright file="RaribleService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Microsoft.Extensions.Options;
using Uchoose.RaribleService.Interfaces;
using Uchoose.RaribleService.Interfaces.Requests;
using Uchoose.RaribleService.Interfaces.Settings;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.RaribleService
{
    // TODO - реализовать методы интерфейса

    /// <inheritdoc cref="IRaribleService"/>
    public class RaribleService :
        IRaribleService,
        ITransientService
    {
        private readonly RaribleSettings _raribleSettings;

        /// <summary>
        /// Инициализирует экземпляр <see cref="RaribleService"/>.
        /// </summary>
        /// <param name="raribleSettings"><see cref="RaribleSettings"/>.</param>
        public RaribleService(
            IOptionsSnapshot<RaribleSettings> raribleSettings)
        {
            _raribleSettings = raribleSettings.Value;
        }

        /// <inheritdoc />
        public async Task<Result<string>> MintNftAsync(RaribleMintNftRequest request)
        {
            // TODO - добавить реализацию

            return await Result<string>.SuccessAsync("TODO");
        }
    }
}