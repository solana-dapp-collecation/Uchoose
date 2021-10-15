// ------------------------------------------------------------------------------------------------------
// <copyright file="IJsonSerializer.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Text.Json;

using Uchoose.Utils.Contracts.Services;

namespace Uchoose.SerializationService.Interfaces
{
    /// <summary>
    /// Сериализатор json.
    /// </summary>
    public interface IJsonSerializer :
        IApplicationService
    {
        /// <summary>
        /// Сериализовать объект заданного типа в json cтроку.
        /// </summary>
        /// <typeparam name="T">Тип объекта для сериализации.</typeparam>
        /// <param name="obj">Сериализуемый объект.</param>
        /// <param name="settings">Настройки сериализации.</param>
        /// <returns>Возвращает сериализованный объект заданного типа в виде json строки.</returns>
        string Serialize<T>(T obj, JsonSerializerOptions settings = null);

        /// <summary>
        /// Сериализовать объект заданного типа в json cтроку.
        /// </summary>
        /// <typeparam name="T">Тип объекта для сериализации.</typeparam>
        /// <param name="obj">Сериализуемый объект.</param>
        /// <param name="type">Тип сериализуемого объекта.</param>
        /// <param name="settings">Настройки сериализации.</param>
        /// <returns>Возвращает сериализованный объект заданного типа в виде json строки.</returns>
        string Serialize<T>(T obj, Type type, JsonSerializerOptions settings = null);

        /// <summary>
        /// Десериализовать json строку в объект заданного типа.
        /// </summary>
        /// <typeparam name="T">Тип объекта для десериализации.</typeparam>
        /// <param name="text">Json строка.</param>
        /// <param name="settings">Настройки сериализации.</param>
        /// <returns>Возвращает десериализованный из json строки объект заданного типа.</returns>
        T Deserialize<T>(string text, JsonSerializerOptions settings = null);
    }
}