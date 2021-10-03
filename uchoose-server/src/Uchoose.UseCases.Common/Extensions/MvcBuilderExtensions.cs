// ------------------------------------------------------------------------------------------------------
// <copyright file="MvcBuilderExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Reflection;

using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Uchoose.UseCases.Common.Extensions
{
    /// <summary>
    /// Методы расширения для <see cref="IMvcBuilder"/>.
    /// </summary>
    public static class MvcBuilderExtensions
    {
        /// <summary>
        /// Добавить валидаторы.
        /// </summary>
        /// <param name="builder"><see cref="IMvcBuilder"/>.</param>
        /// <returns>Возвращает <see cref="IMvcBuilder"/>.</returns>
        public static IMvcBuilder AddValidators(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true));
            return builder;
        }
    }
}