// ------------------------------------------------------------------------------------------------------
// <copyright file="OrderByConverter.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

using AutoMapper;
using Uchoose.Utils.Extensions;

namespace Uchoose.Utils.Mappings.Converters
{
    /// <summary>
    /// Конвертер строки OrderBy из фильтра в массив строк для сортировки по полям, и обратно.
    /// </summary>
    public class OrderByConverter :
        IValueConverter<string, string[]>,
        IValueConverter<string[], string>
    {
        /// <inheritdoc/>
        public string[] Convert(string orderBy, ResolutionContext context = null)
        {
            if (orderBy.IsPresent())
            {
                return orderBy
                    .Split(',')
                    .Where(x => x.IsPresent())
                    .Select(x => x.Trim()).ToArray();
            }

            return Array.Empty<string>();
        }

        /// <inheritdoc/>
        public string Convert(string[] orderBy, ResolutionContext context = null) => orderBy?.Any() == true ? string.Join(",", orderBy) : null;
    }
}