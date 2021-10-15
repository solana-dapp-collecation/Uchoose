// ------------------------------------------------------------------------------------------------------
// <copyright file="MetadataAccount.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Solnet.Programs.Utilities;
using Solnet.Rpc;
using Solnet.Rpc.Models;
using Solnet.Rpc.Utilities;
using Solnet.Wallet;

// ReSharper disable InconsistentNaming
#pragma warning disable 1591

#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter

// ReSharper disable once CheckNamespace
namespace Solnet.Metaplex
{
    public enum MetadataKey
    {
        Uninitialized = 0,

        MetadataV1 = 4,

        EditionV1 = 1,

        MasterEditionV1 = 2,

        MasterEditionV2 = 6,

        EditionMarker = 7
    }

    public class MetadataCategory
    {
        public string Audio = "audio";

        public string Video = "video";

        public string Image = "image";

        public string VR = "vr";

        public string HTML = "html";

        public string Document = "document";
    }

    public struct MetadataFile
    {
        public string uri;

        public string type;
    }

    public class Data
    {
        public string name;

        public string symbol;

        public string uri;

        public uint sellerFeeBasisPoints;

        public IList<Creator> creators;

        public Data(string name, string symbol, string uri, uint sellerFee, IList<Creator> creators)
        {
            this.name = name;
            this.symbol = symbol;
            this.uri = uri;
            this.sellerFeeBasisPoints = sellerFee;
            this.creators = creators;
        }
    }

    public class MetadataAccount
    {
        public PublicKey metadataKey;

        public PublicKey updateAuthority;

        public string mint;

        public Data data;

        public bool isMutable;

        public uint editionNonce;

        public AccountInfo accInfo;

        public PublicKey owner;

        public MetadataAccount(AccountInfo accInfo)
        {
            try
            {
                this.owner = new(accInfo.Owner);
                this.data = ParseData(accInfo.Data);

                byte[] data = Convert.FromBase64String(accInfo.Data[0]);
                this.updateAuthority = new PublicKey(data[1..33]);
                this.mint = new PublicKey(data[33..65]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static Data ParseData(List<string> data)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(data[0]);
                ReadOnlySpan<byte> binData = new(bytes);

                (string name, _) = binData.DecodeRustString(MetadataAccountLayout.nameOffset);
                (string symbol, _) = binData.DecodeRustString(MetadataAccountLayout.symbolOffset);
                (string uri, _) = binData.DecodeRustString(MetadataAccountLayout.uriOffset);
                uint sellerFee = binData.GetU32(MetadataAccountLayout.feeBasisOffset);

                ushort numOfCreators = binData.GetU16(MetadataAccountLayout.creatorsOffset);
                var creators = MetadataProgramData.DecodeCreators(binData.GetSpan(
                    MetadataAccountLayout.creatorsOffset + 4,
                    numOfCreators * (32 + 1 + 1)));

                var res = new Data(
                    name, symbol, uri, sellerFee, creators);

                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("could not decode account data from base64", ex);
            }
        }

        public static async Task<MetadataAccount> GetAccount(IRpcClient client, PublicKey pk)
        {
            var accInfoResponse = await client.GetAccountInfoAsync(pk.Key);

            if (accInfoResponse.WasSuccessful)
            {
                var accInfo = accInfoResponse.Result?.Value;
                if (accInfo == null)
                {
                    return null;
                }

                if (accInfo.Owner.Contains("meta"))
                {
                    return new MetadataAccount(accInfo);
                }
                else // if(accInfo.Owner.Contains("Token"))
                {
                    byte[] readdata = Convert.FromBase64String(accInfo.Data[0]);

                    PublicKey mintAccount;

                    if (readdata.Length == 165)
                    {
                        mintAccount = new PublicKey(readdata[..32]);
                    }
                    else // if( readdata.Length == 82)
                    {
                        mintAccount = pk;
                    }

                    byte[] metadataAddress = new byte[32];
                    int nonce;
                    AddressExtensions.TryFindProgramAddress(
                        new List<byte[]>
                        {
                            Encoding.UTF8.GetBytes("metadata"),
                            MetadataProgram.ProgramIdKey,
                            mintAccount
                        },
                        MetadataProgram.ProgramIdKey,
                        out metadataAddress,
                        out nonce);

                    return await GetAccount(client, new(metadataAddress));
                }
            }
            else
            {
                return null;
            }
        }

        // async public static MetadataAccount GetAccount ( string query )
        // {

        // }
    }
}