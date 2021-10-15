// ------------------------------------------------------------------------------------------------------
// <copyright file="MarketplaceDbSeeder.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Uchoose.DataAccess.Interfaces;
using Uchoose.DataAccess.Marketplace.Interfaces.Contexts;
using Uchoose.Domain.Marketplace.Entities;
using Uchoose.SerializationService.Interfaces;

namespace Uchoose.DataAccess.PostgreSql.Marketplace.Persistence
{
    /// <summary>
    /// Наполнитель БД данными, связанными с маркетплейсом NFT.
    /// </summary>
    internal sealed class MarketplaceDbSeeder :
        IDatabaseSeeder
    {
        private readonly ILogger<MarketplaceDbSeeder> _logger;
        private readonly IMarketplaceDbContext _context;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IStringLocalizer<MarketplaceDbSeeder> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="MarketplaceDbSeeder"/>.
        /// </summary>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        /// <param name="context"><see cref="IMarketplaceDbContext"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public MarketplaceDbSeeder(
            ILogger<MarketplaceDbSeeder> logger,
            IMarketplaceDbContext context,
            IJsonSerializer jsonSerializer,
            IStringLocalizer<MarketplaceDbSeeder> localizer)
        {
            _logger = logger;
            _context = context;
            _jsonSerializer = jsonSerializer;
            _localizer = localizer;
        }

        /// <summary>
        /// Путь к каталогу с данными для наполнения БД.
        /// </summary>
        private static string SeedDataPath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "Persistence", "SeedData");

        /// <inheritdoc/>
        public void Initialize()
        {
            try
            {
                AddNftImageLayerTypes();

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e, _localizer["An error occurred while seeding Marketplace data."]);
            }
        }

        /// <summary>
        /// Добавить типы слоя изображения NFT.
        /// </summary>
        private void AddNftImageLayerTypes()
        {
            Task.Run(async () =>
            {
                if (!_context.NftImageLayerTypes.Any())
                {
                    string nftImageLayerTypeData = await File.ReadAllTextAsync(Path.Combine(SeedDataPath, "nftImageLayerTypes.json"), Encoding.UTF8);
                    var nftImageLayerTypes = _jsonSerializer.Deserialize<List<NftImageLayerType>>(nftImageLayerTypeData);

                    if (nftImageLayerTypes != null)
                    {
                        foreach (var nftImageLayerType in nftImageLayerTypes)
                        {
                            nftImageLayerType.IncrementVersion();
                            await _context.NftImageLayerTypes.AddAsync(nftImageLayerType);
                        }
                    }

                    await _context.SaveChangesAsync();
                    _logger.LogInformation(_localizer["Seeded NFT Image Layer Types."]);
                }
            }).GetAwaiter().GetResult();
        }
    }
}