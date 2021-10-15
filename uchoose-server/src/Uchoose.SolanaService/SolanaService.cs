// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using Microsoft.Extensions.Options;
using Solnet.Rpc;
using Solnet.Wallet;
using Solnet.Wallet.Bip39;
using Uchoose.SolanaService.Interfaces;
using Uchoose.SolanaService.Interfaces.Requests;
using Uchoose.SolanaService.Interfaces.Settings;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.SolanaService
{
    // TODO - реализовать методы интерфейса

    /// <inheritdoc cref="ISolanaService"/>
    public class SolanaService :
        ISolanaService,
        ITransientService
    {
        private readonly SolanaSettings _solanaSettings;

        private readonly IRpcClient _client;
        private readonly Wallet _wallet;

        /// <summary>
        /// Инициализирует экземпляр <see cref="SolanaService"/>.
        /// </summary>
        /// <param name="solanaSettings"><see cref="SolanaSettings"/>.</param>
        public SolanaService(
            IOptionsSnapshot<SolanaSettings> solanaSettings)
        {
            _solanaSettings = solanaSettings.Value;
            _client = ClientFactory.GetClient((Cluster)_solanaSettings.CurrentCluster);
            _wallet = new(new Mnemonic(_solanaSettings.Accounts.MinterAccount.MnemonicWords));
        }

        /// <inheritdoc />
        public async Task<Result<string>> MintNftAsync(SolanaMintNftRequest request)
        {
            // TODO - Waiting for Solnet.Metaplex nuget-package to implement NFT minting logic!

            return await Result<string>.SuccessAsync("TODO");

            /*var recentHash = await _client.GetRecentBlockHashAsync();

            ulong minBalanceForExemptionMint =
                (await _client.GetMinimumBalanceForRentExemptionAsync(TokenProgram.MintAccountDataSize)).Result;

            byte[] tx = new TransactionBuilder()
                .SetRecentBlockHash(recentHash.Result.Value.Blockhash)
                .SetFeePayer(request.OwnerAccount)
                .AddInstruction(SystemProgram.CreateAccount(
                    request.OwnerAccount.PublicKey,
                    request.MintAccount.PublicKey,
                    minBalanceForExemptionMint,
                    TokenProgram.MintAccountDataSize,
                    TokenProgram.ProgramIdKey))
                .AddInstruction(TokenProgram.InitializeMint(
                    request.MintAccount.PublicKey,
                    0,
                    request.OwnerAccount.PublicKey,
                    request.OwnerAccount.PublicKey))
                .AddInstruction(SystemProgram.CreateAccount(
                    request.OwnerAccount,
                    initialAccount,
                    minBalanceForExemptionAcc,
                    TokenProgram.TokenAccountDataSize,
                    TokenProgram.ProgramIdKey))
                .AddInstruction(TokenProgram.InitializeAccount(
                    request.InitialAccount.PublicKey,
                    request.MintAccount.PublicKey,
                    request.OwnerAccount.PublicKey))
                .AddInstruction(TokenProgram.MintTo(
                    request.MintAccount.PublicKey,
                    request.InitialAccount.PublicKey,
                    1,
                    request.OwnerAccount.PublicKey))
                .SetFeePayer(_wallet.Account)

                .Build(_wallet.Account);

            var txHash = await _client.SendTransactionAsync(tx);

            if (txHash.WasSuccessful)
            {
                return await Result<string>.SuccessAsync(txHash.Result);
            }
            else
            {
                return await Result<string>.FailAsync(txHash.Reason);
            }*/
        }
    }
}