// ------------------------------------------------------------------------------------------------------
// <copyright file="SystemTextJsonSerializer.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Text.Json;

using Microsoft.Extensions.Options;
using Uchoose.SerializationService.Interfaces;
using Uchoose.SerializationService.Interfaces.Converters;

namespace Uchoose.SerializationService.Serializers
{
    /// <summary>
    /// Сериализатор json с помощью <see cref="System.Text.Json"/>.
    /// </summary>
    public class SystemTextJsonSerializer :
        IJsonSerializer
    {
        private readonly JsonSerializerOptions _options;

        /// <summary>
        /// Инициализирует экземпляр <see cref="SystemTextJsonSerializer"/>.
        /// </summary>
        /// <param name="options"><see cref="JsonSerializerOptions"/>.</param>
        public SystemTextJsonSerializer(IOptions<JsonSerializerOptions> options)
        {
            _options = options.Value;

            // TODO - или конфигурировать не здесь?
            if (!_options.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
            {
                _options.Converters.Add(new TimespanJsonConverter());
            }
        }

        /// <inheritdoc/>
        public T Deserialize<T>(string data, JsonSerializerOptions options = null)
            => JsonSerializer.Deserialize<T>(data, options ?? _options);

        /// <inheritdoc/>
        public string Serialize<T>(T data, JsonSerializerOptions options = null)
            => JsonSerializer.Serialize(data, options ?? _options);

        /// <inheritdoc/>
        public string Serialize<T>(T data, Type type, JsonSerializerOptions options = null)
            => JsonSerializer.Serialize(data, type, options ?? _options);
    }
}