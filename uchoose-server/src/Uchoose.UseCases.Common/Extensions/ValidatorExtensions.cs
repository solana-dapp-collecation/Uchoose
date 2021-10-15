// ------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

#nullable enable
using FluentValidation;
using Uchoose.SerializationService.Interfaces;

namespace Uchoose.UseCases.Common.Extensions
{
    /// <summary>
    /// Методы расширения для валидаторов данных.
    /// </summary>
    public static class ValidatorExtensions
    {
        #region MustBeJson

        /// <summary>
        /// Правило, указывающее, что значение должно быть json строкой.
        /// </summary>
        /// <typeparam name="T">Тип проверяемого класса.</typeparam>
        /// <param name="ruleBuilder">Строитель правил.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        /// <returns>Возвращает <see cref="IRuleBuilderOptions{T,TRpoperty}"/>.</returns>
        public static IRuleBuilderOptions<T, string?> MustBeJson<T>(this IRuleBuilderInitial<T, string?> ruleBuilder, IJsonSerializer jsonSerializer)
            where T : class
            => ruleBuilder
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(value =>
                {
                    if (value == null)
                    {
                        return false;
                    }

                    bool isJson = true;
                    value = value.Trim();
                    try
                    {
                        jsonSerializer.Deserialize<object>(value);
                    }
                    catch
                    {
                        isJson = false;
                    }

                    return (isJson && value.StartsWith("{") && value.EndsWith("}")) || (value.StartsWith("[") && value.EndsWith("]"));
                })
                .WithMessage("The '{PropertyName}' property must be a valid JSON string.");

        #endregion MustBeJson
    }
}