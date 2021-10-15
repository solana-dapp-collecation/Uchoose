// ------------------------------------------------------------------------------------------------------
// <copyright file="ExportPaginationFilterValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Localization;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Contracts.Exporting;
using Uchoose.Utils.Contracts.Searching;

namespace Uchoose.Utils.Filters.Validators
{
    /// <inheritdoc cref="IExportPaginationFilterValidator{TEntityId, TEntity, TFilter}"/>
    public abstract class ExportPaginationFilterValidator<TEntityId, TEntity, TFilter> :
        PaginationFilterValidator<TEntityId, TEntity, TFilter>,
        IExportPaginationFilterValidator<TEntityId, TEntity, TFilter>
        where TEntity : class, IEntity<TEntityId>, IExportable<TEntityId, TEntity>, ISearchable<TEntityId, TEntity>, new()
        where TFilter : ExportPaginationFilter<TEntityId, TEntity>
    {
        /// <summary>
        /// Инициализирует экземпляр класса, наследующего <see cref="ExportPaginationFilterValidator{TEntityId, TEntity, TImportCommand}"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        protected ExportPaginationFilterValidator(IStringLocalizer localizer)
            : base(localizer)
        {
            IExportPaginationFilterValidator<TEntityId, TEntity, TFilter>.UseRules(this, localizer);
        }
    }
}