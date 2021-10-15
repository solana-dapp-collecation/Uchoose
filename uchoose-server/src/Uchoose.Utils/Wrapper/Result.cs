// ------------------------------------------------------------------------------------------------------
// <copyright file="Result.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using Uchoose.Utils.Attributes.Logging;
using Uchoose.Utils.Contracts.Logging;
using Uchoose.Utils.Contracts.Properties;

namespace Uchoose.Utils.Wrapper
{
    /// <inheritdoc cref="IResult"/>
    public record Result :
        IResult,
        ILoggable
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="Result"/>.
        /// </summary>
        public Result()
        {
        }

        /// <inheritdoc/>
        public List<string> Messages { get; set; } = new();

        /// <inheritdoc/>
        /// <example>true</example>
        public bool Succeeded { get; init; }

        /// <summary>
        /// Неуспешный результат операции.
        /// </summary>
        /// <returns>Возвращает <see cref="IResult"/>.</returns>
        public static IResult Fail()
        {
            return new Result { Succeeded = false };
        }

        /// <summary>
        /// Неуспешный результат операции.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Возвращает <see cref="IResult"/>.</returns>
        public static IResult Fail(string message)
        {
            return new Result { Succeeded = false, Messages = new() { message } };
        }

        /// <summary>
        /// Неуспешный результат операции.
        /// </summary>
        /// <param name="messages">Список сообщений.</param>
        /// <returns>Возвращает <see cref="IResult"/>.</returns>
        public static IResult Fail(List<string> messages)
        {
            return new Result { Succeeded = false, Messages = messages };
        }

        /// <summary>
        /// Неуспешный результат операции.
        /// </summary>
        /// <returns>Возвращает <see cref="Task{IResult}"/>.</returns>
        public static Task<IResult> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        /// <summary>
        /// Неуспешный результат операции.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Возвращает <see cref="Task{IResult}"/>.</returns>
        public static Task<IResult> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        /// <summary>
        /// Неуспешный результат операции.
        /// </summary>
        /// <param name="messages">Список сообщений.</param>
        /// <returns>Возвращает <see cref="Task{IResult}"/>.</returns>
        public static Task<IResult> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        /// <summary>
        /// Успешный результат операции.
        /// </summary>
        /// <returns>Возвращает <see cref="IResult"/>.</returns>
        public static IResult Success()
        {
            return new Result { Succeeded = true };
        }

        /// <summary>
        /// Успешный результат операции.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Возвращает <see cref="IResult"/>.</returns>
        public static IResult Success(string message)
        {
            return new Result { Succeeded = true, Messages = new() { message } };
        }

        /// <summary>
        /// Успешный результат операции.
        /// </summary>
        /// <param name="messages">Список сообщений.</param>
        /// <returns>Возвращает <see cref="IResult"/>.</returns>
        public static IResult Success(List<string> messages)
        {
            return new Result { Succeeded = true, Messages = messages };
        }

        /// <summary>
        /// Успешный результат операции.
        /// </summary>
        /// <returns>Возвращает <see cref="Task{IResult}"/>.</returns>
        public static Task<IResult> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        /// <summary>
        /// Успешный результат операции.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Возвращает <see cref="Task{IResult}"/>.</returns>
        public static Task<IResult> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        /// <summary>
        /// Успешный результат операции.
        /// </summary>
        /// <param name="messages">Список сообщений.</param>
        /// <returns>Возвращает <see cref="Task{IResult}"/>.</returns>
        public static Task<IResult> SuccessAsync(List<string> messages)
        {
            return Task.FromResult(Success(messages));
        }
    }

    /// <summary>
    /// Результат операции с ошибкой и возвращаемыми данными.
    /// </summary>
    /// <typeparam name="T">Тип возвращаемых данных.</typeparam>
    public record ErrorResult<T> :
        Result<T>,
        IHasErrorCode
    {
        /// <summary>
        /// Источник ошибки.
        /// </summary>
        /// <example>Example Source</example>
        public string Source { get; set; }

        /// <summary>
        /// Исключение.
        /// </summary>
        /// <example>Example Exception</example>
        public string Exception { get; set; }

        /// <summary>
        /// Трассировка стека вызовов.
        /// </summary>
        /// <example>Example Stack Trace</example>
        public string StackTrace { get; set; }

        /// <inheritdoc/>
        public int ErrorCode { get; set; }
    }

    /// <inheritdoc cref="IResult{T}"/>
    public record Result<T> : Result, IResult<T>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="Result"/>.
        /// </summary>
        public Result()
        {
        }

        /// <inheritdoc/>
        [LogMasked(ShowFirst = 40)]
        public T Data { get; init; }

        /// <summary>
        /// Неуспешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <returns>Возвращает <see cref="Result{T}"/>.</returns>
        public static new Result<T> Fail()
        {
            return new() { Succeeded = false };
        }

        /// <summary>
        /// Неуспешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Возвращает <see cref="Result{T}"/>.</returns>
        public static new Result<T> Fail(string message)
        {
            return new() { Succeeded = false, Messages = new() { message } };
        }

        /// <summary>
        /// Неуспешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="messages">Список сообщений.</param>
        /// <returns>Возвращает <see cref="Result{T}"/>.</returns>
        public static new Result<T> Fail(List<string> messages)
        {
            return new() { Succeeded = false, Messages = messages };
        }

        /// <summary>
        /// Неуспешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="data">Возвращаемые данные.</param>
        /// <returns>Возвращает <see cref="Result{T}"/>.</returns>
        public static Result<T> Fail(T data)
        {
            return new() { Succeeded = false, Data = data };
        }

        /// <summary>
        /// Неуспешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="data">Возвращаемые данные.</param>
        /// <param name="message">Сообщение.</param>
        /// <returns>Возвращает <see cref="Result{T}"/>.</returns>
        public static Result<T> Fail(T data, string message)
        {
            return new() { Succeeded = false, Data = data, Messages = new() { message } };
        }

        /// <summary>
        /// Неуспешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="data">Возвращаемые данные.</param>
        /// <param name="messages">Список сообщений.</param>
        /// <returns>Возвращает <see cref="Result{T}"/>.</returns>
        public static Result<T> Fail(T data, List<string> messages)
        {
            return new() { Succeeded = false, Data = data, Messages = messages };
        }

        /// <summary>
        /// Ошибка с возвращаемыми данными.
        /// </summary>
        /// <returns>Возвращает <see cref="ErrorResult{T}"/>.</returns>
        public static ErrorResult<T> ReturnError()
        {
            return new() { Succeeded = false, ErrorCode = 500 };
        }

        /// <summary>
        /// Ошибка с возвращаемыми данными.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Возвращает <see cref="ErrorResult{T}"/>.</returns>
        public static ErrorResult<T> ReturnError(string message)
        {
            return new() { Succeeded = false, Messages = new() { message }, ErrorCode = 500 };
        }

        /// <summary>
        /// Ошибка с возвращаемыми данными.
        /// </summary>
        /// <param name="messages">Список сообщений.</param>
        /// <returns>Возвращает <see cref="ErrorResult{T}"/>.</returns>
        public static ErrorResult<T> ReturnError(List<string> messages)
        {
            return new() { Succeeded = false, Messages = messages, ErrorCode = 500 };
        }

        /// <summary>
        /// Неуспешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <returns>Возвращает <see cref="Task{Result}"/>.</returns>
        public static new Task<Result<T>> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        /// <summary>
        /// Неуспешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Возвращает <see cref="Task{Result}"/>.</returns>
        public static new Task<Result<T>> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        /// <summary>
        /// Неуспешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="messages">Список сообщений.</param>
        /// <returns>Возвращает <see cref="Task{Result}"/>.</returns>
        public static new Task<Result<T>> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        /// <summary>
        /// Неуспешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="data">Возвращаемые данные.</param>
        /// <returns>Возвращает <see cref="Task{Result}"/>.</returns>
        public static Task<Result<T>> FailAsync(T data)
        {
            return Task.FromResult(Fail(data));
        }

        /// <summary>
        /// Неуспешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="data">Возвращаемые данные.</param>
        /// <param name="message">Сообщение.</param>
        /// <returns>Возвращает <see cref="Task{Result}"/>.</returns>
        public static Task<Result<T>> FailAsync(T data, string message)
        {
            return Task.FromResult(Fail(data, message));
        }

        /// <summary>
        /// Неуспешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="data">Возвращаемые данные.</param>
        /// <param name="messages">Список сообщений.</param>
        /// <returns>Возвращает <see cref="Task{Result}"/>.</returns>
        public static Task<Result<T>> FailAsync(T data, List<string> messages)
        {
            return Task.FromResult(Fail(data, messages));
        }

        /// <summary>
        /// Ошибка с возвращаемыми данными.
        /// </summary>
        /// <returns>Возвращает <see cref="Task{ErrorResult}"/>.</returns>
        public static Task<ErrorResult<T>> ReturnErrorAsync()
        {
            return Task.FromResult(ReturnError());
        }

        /// <summary>
        /// Ошибка с возвращаемыми данными.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Возвращает <see cref="Task{ErrorResult}"/>.</returns>
        public static Task<ErrorResult<T>> ReturnErrorAsync(string message)
        {
            return Task.FromResult(ReturnError(message));
        }

        /// <summary>
        /// Ошибка с возвращаемыми данными.
        /// </summary>
        /// <param name="messages">Список сообщений.</param>
        /// <returns>Возвращает <see cref="Task{ErrorResult}"/>.</returns>
        public static Task<ErrorResult<T>> ReturnErrorAsync(List<string> messages)
        {
            return Task.FromResult(ReturnError(messages));
        }

        /// <summary>
        /// Успешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <returns>Возвращает <see cref="Result{T}"/>.</returns>
        public static new Result<T> Success()
        {
            return new() { Succeeded = true };
        }

        /// <summary>
        /// Успешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Возвращает <see cref="Result{T}"/>.</returns>
        public static new Result<T> Success(string message)
        {
            return new() { Succeeded = true, Messages = new() { message } };
        }

        /// <summary>
        /// Успешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="messages">Список сообщений.</param>
        /// <returns>Возвращает <see cref="Result{T}"/>.</returns>
        public static new Result<T> Success(List<string> messages)
        {
            return new() { Succeeded = true, Messages = messages };
        }

        /// <summary>
        /// Успешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="data">Возвращаемые данные.</param>
        /// <returns>Возвращает <see cref="Result{T}"/>.</returns>
        public static Result<T> Success(T data)
        {
            return new() { Succeeded = true, Data = data };
        }

        /// <summary>
        /// Успешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="data">Возвращаемые данные.</param>
        /// <param name="message">Сообщение.</param>
        /// <returns>Возвращает <see cref="Result{T}"/>.</returns>
        public static Result<T> Success(T data, string message)
        {
            return new() { Succeeded = true, Data = data, Messages = new() { message } };
        }

        /// <summary>
        /// Успешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="data">Возвращаемые данные.</param>
        /// <param name="messages">Список сообщений.</param>
        /// <returns>Возвращает <see cref="Result{T}"/>.</returns>
        public static Result<T> Success(T data, List<string> messages)
        {
            return new() { Succeeded = true, Data = data, Messages = messages };
        }

        /// <summary>
        /// Успешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <returns>Возвращает <see cref="Task{Result}"/>.</returns>
        public static new Task<Result<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        /// <summary>
        /// Успешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Возвращает <see cref="Task{Result}"/>.</returns>
        public static new Task<Result<T>> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        /// <summary>
        /// Успешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="messages">Список сообщений.</param>
        /// <returns>Возвращает <see cref="Task{Result}"/>.</returns>
        public static new Task<Result<T>> SuccessAsync(List<string> messages)
        {
            return Task.FromResult(Success(messages));
        }

        /// <summary>
        /// Успешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="data">Возвращаемые данные.</param>
        /// <returns>Возвращает <see cref="Task{Result}"/>.</returns>
        public static Task<Result<T>> SuccessAsync(T data)
        {
            return Task.FromResult(Success(data));
        }

        /// <summary>
        /// Успешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="data">Возвращаемые данные.</param>
        /// <param name="message">Сообщение.</param>
        /// <returns>Возвращает <see cref="Task{Result}"/>.</returns>
        public static Task<Result<T>> SuccessAsync(T data, string message)
        {
            return Task.FromResult(Success(data, message));
        }

        /// <summary>
        /// Успешный результат операции с возвращаемыми данными.
        /// </summary>
        /// <param name="data">Возвращаемые данные.</param>
        /// <param name="messages">Список сообщений.</param>
        /// <returns>Возвращает <see cref="Task{Result}"/>.</returns>
        public static Task<Result<T>> SuccessAsync(T data, List<string> messages)
        {
            return Task.FromResult(Success(data, messages));
        }
    }
}