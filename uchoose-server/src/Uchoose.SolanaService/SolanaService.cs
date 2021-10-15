// ------------------------------------------------------------------------------------------------------
// <copyright file="SolanaService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;
using Solnet.Extensions;
using Solnet.Metaplex;
using Solnet.Programs;
using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Wallet;
using Solnet.Wallet.Bip39;
using Uchoose.SolanaService.Interfaces;
using Uchoose.SolanaService.Interfaces.Requests;
using Uchoose.SolanaService.Interfaces.Responses;
using Uchoose.SolanaService.Interfaces.Settings;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Wrapper;

namespace Uchoose.SolanaService
{
    /// <inheritdoc cref="ISolanaService"/>
    public class SolanaService :
        ISolanaService,
        ITransientService
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
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
        public async Task<Result<SolanaMintNftResponse>> MintNftAsync(SolanaMintNftRequest request)
        {
            var blockHash = await _client.GetRecentBlockHashAsync();
            ulong minBalanceForExemptionAcc = (await _client.GetMinimumBalanceForRentExemptionAsync(TokenProgram.TokenAccountDataSize)).Result;
            ulong minBalanceForExemptionMint = (await _client.GetMinimumBalanceForRentExemptionAsync(TokenProgram.MintAccountDataSize)).Result;

            var mintAccount = _wallet.GetAccount(request.MintAccount.GetAccountIndex());
            var ownerAccount = _wallet.GetAccount(request.FromAccount.GetAccountIndex());
            var initialAccount = _wallet.GetAccount(request.ToAccount.GetAccountIndex());

            byte[] tx = new TransactionBuilder()
                .SetRecentBlockHash(blockHash.Result.Value.Blockhash)
                .SetFeePayer(ownerAccount)
                .AddInstruction(SystemProgram.CreateAccount(
                    ownerAccount,
                    mintAccount,
                    minBalanceForExemptionMint,
                    TokenProgram.MintAccountDataSize,
                    TokenProgram.ProgramIdKey))
                .AddInstruction(TokenProgram.InitializeMint(
                    mintAccount,
                    request.MintDecimals,
                    ownerAccount,
                    ownerAccount))
                .AddInstruction(SystemProgram.CreateAccount(
                    ownerAccount,
                    initialAccount,
                    minBalanceForExemptionAcc,
                    TokenProgram.TokenAccountDataSize,
                    TokenProgram.ProgramIdKey))
                .AddInstruction(TokenProgram.InitializeAccount(
                    initialAccount,
                    mintAccount,
                    ownerAccount))
                .AddInstruction(TokenProgram.MintTo(
                    mintAccount,
                    initialAccount,
                    request.Amount,
                    ownerAccount))
                .AddInstruction(MemoProgram.NewMemo(initialAccount, request.MemoText))
                .Build(new List<Account> { mintAccount, ownerAccount, initialAccount });

            var sendTransactionResult = await _client.SimulateTransactionAsync(tx);
            if (!sendTransactionResult.WasSuccessful)
            {
                return await Result<SolanaMintNftResponse>.FailAsync(sendTransactionResult.Reason);
            }

            return await Result<SolanaMintNftResponse>.SuccessAsync(new(sendTransactionResult.Result.Value.Error.InstructionError.BorshIoError), "Success");
        }

        /// <inheritdoc />
        public async Task<Result<SolanaGetNftMetadataResponse>> GetNftMetadataAsync(SolanaGetNftMetadataRequest request)
        {
            try
            {
                var account = await MetadataAccount.GetAccount(_client, new(request.AccountAddress));
                return await Result<SolanaGetNftMetadataResponse>.SuccessAsync(new(account), "Success");
            }
            catch (ArgumentNullException)
            {
                return await Result<SolanaGetNftMetadataResponse>.FailAsync("Account address is not correct or metadata not exists");
            }
            catch (NullReferenceException)
            {
                return await Result<SolanaGetNftMetadataResponse>.FailAsync("Account address is not correct or metadata not exists");
            }
            catch (Exception e)
            {
                return await Result<SolanaGetNftMetadataResponse>.FailAsync(e.Message);
            }
        }

        /// <inheritdoc />
        public async Task<Result<SolanaGetNftWalletResponse>> GetNftWalletAsync(SolanaGetNftWalletRequest request)
        {
            try
            {
                var ownerAccount = _wallet.GetAccount(request.OwnerAccount.GetAccountIndex());

                var tokens = new TokenMintResolver();
                tokens.Add(new(request.MintToken, request.MintName, request.MintSymbol, request.MintDecimal));

                var tokenWallet = await TokenWallet.LoadAsync(_client, tokens, ownerAccount);
                var balances = tokenWallet.Balances();
                var sublist = tokenWallet.TokenAccounts().WithSymbol(request.MintSymbol).WithMint(request.MintToken);
                if (!string.IsNullOrEmpty(request.MintSymbol))
                {
                    sublist = sublist.WithSymbol(request.MintSymbol);
                }

                if (!string.IsNullOrEmpty(request.MintToken))
                {
                    sublist = sublist.WithMint(request.MintToken);
                }

                return await Result<SolanaGetNftWalletResponse>.SuccessAsync(new(sublist, balances), "Success");
            }
            catch (Exception e)
            {
                return await Result<SolanaGetNftWalletResponse>.FailAsync(e.Message);
            }
        }

        /// <inheritdoc />
        public async Task<Result<SolanaExchangeTokenResponse>> ExchangeTokensAsync(SolanaExchangeTokenRequest request)
        {
            var blockHash = await _client.GetRecentBlockHashAsync();
            var mintAccount = _wallet.GetAccount(request.MintAccount.GetAccountIndex());
            var fromAccount = _wallet.GetAccount(request.FromAccount.GetAccountIndex());
            var toAccount = _wallet.GetAccount(request.ToAccount.GetAccountIndex());

            byte[] tx = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash)
                .SetFeePayer(fromAccount)
                .AddInstruction(TokenProgram.InitializeAccount(
                    toAccount,
                    mintAccount,
                    fromAccount))
                .AddInstruction(TokenProgram.Transfer(
                    fromAccount,
                    toAccount,
                    request.Amount,
                    fromAccount))
                .AddInstruction(MemoProgram.NewMemo(fromAccount, request.MemoText))
                .Build(new List<Account> { fromAccount });

            var sendTransactionResult = await _client.SendTransactionAsync(tx);
            if (!sendTransactionResult.WasSuccessful)
            {
                return await Result<SolanaExchangeTokenResponse>.FailAsync(sendTransactionResult.Reason);
            }

            return await Result<SolanaExchangeTokenResponse>.SuccessAsync(new(sendTransactionResult.Result), "Success");
        }

        /// <inheritdoc />
        public async Task<Result<SolanaSendTransactionResponse>> SendTransactionAsync(SolanaSendTransactionRequest request)
        {
            var fromAccount = _wallet.GetAccount(request.FromAccount.GetAccountIndex());
            var toAccount = _wallet.GetAccount(request.ToAccount.GetAccountIndex());
            var blockHash = await _client.GetRecentBlockHashAsync();

            byte[] tx = new TransactionBuilder()
                .SetRecentBlockHash(blockHash.Result.Value.Blockhash)
                .SetFeePayer(fromAccount)
                .AddInstruction(MemoProgram.NewMemo(fromAccount, request.MemoText))
                .AddInstruction(SystemProgram.Transfer(fromAccount, toAccount, request.Lampposts))
                .Build(new List<Account> { fromAccount, toAccount });

            var sendTransactionResult = await _client.SendTransactionAsync(tx);
            if (!sendTransactionResult.WasSuccessful)
            {
                return await Result<SolanaSendTransactionResponse>.FailAsync(sendTransactionResult.Reason);
            }

            return await Result<SolanaSendTransactionResponse>.SuccessAsync(new(sendTransactionResult.Result), "Success");
        }
    }
}