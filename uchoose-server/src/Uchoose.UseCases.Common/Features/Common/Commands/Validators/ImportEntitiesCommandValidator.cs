// ------------------------------------------------------------------------------------------------------
// <copyright file="ImportEntitiesCommandValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using FluentValidation;
using Microsoft.Extensions.Localization;
using Uchoose.Utils.Contracts.Common;

namespace Uchoose.UseCases.Common.Features.Common.Commands.Validators
{
    /// <inheritdoc cref="IImportEntitiesCommandValidator{TEntityId, TEntity, TImportCommand}"/>
    internal abstract class ImportEntitiesCommandValidator<TEntityId, TEntity, TImportCommand>
        : AbstractValidator<TImportCommand>,
        IImportEntitiesCommandValidator<TEntityId, TEntity, TImportCommand>
        where TEntity : class, IEntity<TEntityId>
        where TImportCommand : ImportEntitiesCommand
    {
        /// <summary>
        /// Инициализирует экземпляр класса, наследующего <see cref="ImportEntitiesCommandValidator{TEntityId, TEntity, TImportCommand}"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        protected ImportEntitiesCommandValidator(IStringLocalizer localizer)
        {
            IImportEntitiesCommandValidator<TEntityId, TEntity, TImportCommand>.UseRules(this, localizer);
        }
    }
}