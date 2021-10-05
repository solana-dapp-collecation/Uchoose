// ------------------------------------------------------------------------------------------------------
// <copyright file="IFileStorageService.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

using Uchoose.Utils.Contracts.Services;
using Uchoose.Utils.Contracts.Uploading;
using Uchoose.Utils.Enums;

namespace Uchoose.FileStorageService.Interfaces
{
    /// <summary>
    /// Сервис для доступа к хранилищу файлов.
    /// </summary>
    public interface IFileStorageService :
        IApplicationService
    {
        /// <summary>
        /// Получить файл из хранилища по его идентификатору.
        /// </summary>
        /// <param name="fileId">Идентификатор файла в хранилище.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Возвращает данные файла из хранилища по его идентификатору.</returns>
        Task<byte[]> DownloadAsync(string fileId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Загрузить файл в хранилище.
        /// </summary>
        /// <typeparam name="T">Тип данных, для которых осуществляется загрузка.</typeparam>
        /// <param name="request">Запрос на загрузку файла.</param>
        /// <param name="supportedFileType">Поддерживаемый тип файла.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Возвращает путь к загруженному файлу или его идентификатор в файловом хранилище.</returns>
        Task<string> UploadAsync<T>(IFileUploadRequest request, FileType supportedFileType, CancellationToken cancellationToken = default)
            where T : class;

        // TODO - добавить метод для удаления из файлового хранилища
    }
}