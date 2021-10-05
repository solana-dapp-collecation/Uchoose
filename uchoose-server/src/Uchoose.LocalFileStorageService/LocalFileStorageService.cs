// ------------------------------------------------------------------------------------------------------
// <copyright file="LocalFileStorageService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Uchoose.FileStorageService.Interfaces;
using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Enums;
using Uchoose.Utils.Exceptions;
using Uchoose.Utils.Extensions;

namespace Uchoose.LocalFileStorageService
{
    /// <summary>
    /// Сервис для доступа к локальному хранилищу файлов.
    /// </summary>
    internal sealed class LocalFileStorageService :
        IFileStorageService,
        IScopedService
    {
        private const string NumberPattern = "-{0}";

        private readonly IStringLocalizer<LocalFileStorageService> _localizer;

        /// <summary>
        /// Инициализирует экземпляр <see cref="LocalFileStorageService"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer{T}"/>.</param>
        public LocalFileStorageService(
            IStringLocalizer<LocalFileStorageService> localizer)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        /// <inheritdoc/>
        public async Task<byte[]> DownloadAsync(string fileId, CancellationToken cancellationToken = default)
        {
            string fileFullPath = Path.Combine(Directory.GetCurrentDirectory(), fileId.Replace("/", @"\"));
            byte[] fileBytes = Array.Empty<byte>();
            await using var stream = new FileStream(fileFullPath, FileMode.Open);
            int count = await stream.ReadAsync(fileBytes, cancellationToken); // TODO - разобраться, почему не считывает файл

            return fileBytes;
        }

        /// <inheritdoc/>
        public async Task<string> UploadAsync<T>(IFormFile file, FileType supportedFileType, CancellationToken cancellationToken = default)
            where T : class
        {
            if (file == null)
            {
                return string.Empty;
            }

            string extension = new FileInfo(file.FileName).Extension;
            if (!supportedFileType.GetDescriptionList().Contains(extension))
            {
                throw new CustomException(_localizer["File Format Not Supported."], statusCode: HttpStatusCode.BadRequest);
            }

            await using var streamData = file.OpenReadStream();
            if (streamData.Length > 0)
            {
                string folder = typeof(T)
                    .GetGenericTypeName()
                    .Replace('>', '_')
                    .Replace('<', '_')
                    .Trim('_');

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    folder = folder.Replace(@"\", "/");
                }

                string folderName = Path.Combine("Files", folder);
                folderName = supportedFileType switch
                {
                    FileType.Image => Path.Combine(folderName, "Images"),
                    FileType.Template => Path.Combine(folderName, "Templates"),
                    _ => Path.Combine(folderName, "Others"),
                };
                string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                bool exists = Directory.Exists(pathToSave);
                if (!exists)
                {
                    Directory.CreateDirectory(pathToSave);
                }

                string fileName = file.Name.Trim('"');
                fileName = fileName.ReplaceWhitespace("-");
                fileName += extension.Trim();
                string fullPath = Path.Combine(pathToSave, fileName);
                string dbPath = Path.Combine(folderName, fileName);
                if (File.Exists(dbPath))
                {
                    dbPath = NextAvailableFilename(dbPath);
                    fullPath = NextAvailableFilename(fullPath);
                }

                await using var stream = new FileStream(fullPath, FileMode.Create);
                await streamData.CopyToAsync(stream, cancellationToken);
                return dbPath.Replace("\\", "/");
            }
            else
            {
                return string.Empty;
            }
        }

        private static string NextAvailableFilename(string path)
        {
            if (!File.Exists(path))
            {
                return path;
            }

            if (Path.HasExtension(path))
            {
                return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path), StringComparison.Ordinal), NumberPattern));
            }

            return GetNextFilename(path + NumberPattern);
        }

        private static string GetNextFilename(string pattern)
        {
            string tmp = string.Format(pattern, 1);

            if (!File.Exists(tmp))
            {
                return tmp;
            }

            int min = 1, max = 2;

            while (File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;
                if (File.Exists(string.Format(pattern, pivot)))
                {
                    min = pivot;
                }
                else
                {
                    max = pivot;
                }
            }

            return string.Format(pattern, max);
        }
    }
}