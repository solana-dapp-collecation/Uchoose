// ------------------------------------------------------------------------------------------------------
// <copyright file="AddExtendedAttributeCommandValidator.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using FluentValidation;
using Microsoft.Extensions.Localization;
using Uchoose.Domain.Enums;
using Uchoose.SerializationService.Interfaces;
using Uchoose.UseCases.Common.Extensions;
using Uchoose.Utils.Contracts.Common;

namespace Uchoose.UseCases.Common.Features.ExtendedAttributes.Base.Commands.Validators.Abstractions
{
    /// <summary>
    /// Валидатор команды для добавления расширенного атрибута сущности.
    /// </summary>
    /// <typeparam name="TEntityId">Тип идентификатора сущности.</typeparam>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public abstract class AddExtendedAttributeCommandValidator<TEntityId, TEntity> : AbstractValidator<AddExtendedAttributeCommand<TEntityId, TEntity>>
        where TEntity : class, IEntity<TEntityId>
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="AddExtendedAttributeCommandValidator{TEntityId, TEntity}"/>.
        /// </summary>
        /// <param name="localizer"><see cref="IStringLocalizer"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        protected AddExtendedAttributeCommandValidator(IStringLocalizer localizer, IJsonSerializer jsonSerializer)
        {
            RuleFor(request => request.EntityId)
                .NotEmpty().NotEqual(default(TEntityId)).WithMessage(_ => localizer["The '{PropertyName}' property value cannot be null, default or empty."]);
            RuleFor(request => request.Key)
                .NotEmpty().WithMessage(_ => localizer["The '{PropertyName}' property value cannot be empty."]);

            When(request => request.Type == ExtendedAttributeType.Decimal, () =>
            {
                RuleFor(request => request.Decimal).NotNull().WithMessage(x => string.Format(localizer["Decimal value is required using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Text).Null().WithMessage(x => string.Format(localizer["Text value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.DateTime).Null().WithMessage(x => string.Format(localizer["DateTime value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Json).Null().WithMessage(x => string.Format(localizer["Json value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Boolean).Null().WithMessage(x => string.Format(localizer["Boolean value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Integer).Null().WithMessage(x => string.Format(localizer["Integer value should be null using '{0}' type!"], x.Type.ToString()));
            });
            When(request => request.Type == ExtendedAttributeType.Text, () =>
            {
                RuleFor(request => request.Text).NotEmpty().WithMessage(x => string.Format(localizer["Text value is required using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Decimal).Null().WithMessage(x => string.Format(localizer["Decimal value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.DateTime).Null().WithMessage(x => string.Format(localizer["DateTime value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Json).Null().WithMessage(x => string.Format(localizer["Json value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Boolean).Null().WithMessage(x => string.Format(localizer["Boolean value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Integer).Null().WithMessage(x => string.Format(localizer["Integer value should be null using '{0}' type!"], x.Type.ToString()));
            });
            When(request => request.Type == ExtendedAttributeType.DateTime, () =>
            {
                RuleFor(request => request.DateTime).NotNull().WithMessage(x => string.Format(localizer["DateTime value is required using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Decimal).Null().WithMessage(x => string.Format(localizer["Decimal value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Text).Null().WithMessage(x => string.Format(localizer["Text value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Json).Null().WithMessage(x => string.Format(localizer["Json value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Boolean).Null().WithMessage(x => string.Format(localizer["Boolean value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Integer).Null().WithMessage(x => string.Format(localizer["Integer value should be null using '{0}' type!"], x.Type.ToString()));
            });
            When(request => request.Type == ExtendedAttributeType.Json, () =>
            {
                RuleFor(request => request.Json).MustBeJson(jsonSerializer)
                    .WithMessage(x => string.Format(localizer["Json value must be a valid JSON string using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Json).NotEmpty().WithMessage(x => string.Format(localizer["Json value is required using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Decimal).Null().WithMessage(x => string.Format(localizer["Decimal value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Text).Null().WithMessage(x => string.Format(localizer["Text value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.DateTime).Null().WithMessage(x => string.Format(localizer["DateTime value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Boolean).Null().WithMessage(x => string.Format(localizer["Boolean value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Integer).Null().WithMessage(x => string.Format(localizer["Integer value should be null using '{0}' type!"], x.Type.ToString()));
            });
            When(request => request.Type == ExtendedAttributeType.Boolean, () =>
            {
                RuleFor(request => request.Boolean).NotNull().WithMessage(x => string.Format(localizer["Boolean value is required using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Decimal).Null().WithMessage(x => string.Format(localizer["Decimal value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Text).Null().WithMessage(x => string.Format(localizer["Text value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.DateTime).Null().WithMessage(x => string.Format(localizer["DateTime value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Json).Null().WithMessage(x => string.Format(localizer["Json value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Integer).Null().WithMessage(x => string.Format(localizer["Integer value should be null using '{0}' type!"], x.Type.ToString()));
            });
            When(request => request.Type == ExtendedAttributeType.Integer, () =>
            {
                RuleFor(request => request.Integer).NotNull().WithMessage(x => string.Format(localizer["Integer value is required using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Decimal).Null().WithMessage(x => string.Format(localizer["Decimal value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Text).Null().WithMessage(x => string.Format(localizer["Text value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.DateTime).Null().WithMessage(x => string.Format(localizer["DateTime value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Json).Null().WithMessage(x => string.Format(localizer["Json value should be null using '{0}' type!"], x.Type.ToString()));
                RuleFor(request => request.Boolean).Null().WithMessage(x => string.Format(localizer["Boolean value should be null using '{0}' type!"], x.Type.ToString()));
            });
        }
    }
}