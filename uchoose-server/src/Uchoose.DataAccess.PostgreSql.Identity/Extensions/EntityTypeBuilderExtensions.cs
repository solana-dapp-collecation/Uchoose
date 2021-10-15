// ------------------------------------------------------------------------------------------------------
// <copyright file="EntityTypeBuilderExtensions.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uchoose.DataAccess.Interfaces.Settings;
using Uchoose.DataAccess.PostgreSql.Protection;
using Uchoose.Utils.Contracts.Common;
using Uchoose.Utils.Extensions;

namespace Uchoose.DataAccess.PostgreSql.Identity.Extensions
{
    /// <summary>
    /// Методы расширения <see cref="EntityTypeBuilder"/>.
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// Защитить персональные данные в свойствах, помеченных атрибутом <see cref="ProtectedPersonalDataAttribute"/>.
        /// </summary>
        /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/>.</param>
        /// <param name="protector"><see cref="IPersonalDataProtector"/>.</param>
        /// <param name="protectionSettings"><see cref="ProtectionSettings"/>.</param>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        public static void ProtectProperties<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            IPersonalDataProtector protector,
            ProtectionSettings protectionSettings)
                where TEntity : class, IEntity
        {
            if (!protectionSettings.ProtectPersonalData)
            {
                return;
            }

            var converter = new ProtectedPersonalDataConverter(protector);

            string genericTypeName = typeof(TEntity).GetGenericTypeName();
            var personalDataProps = typeof(TEntity).GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(ProtectedPersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                if (protectionSettings.ExcludedEntityProperties?.ContainsKey(genericTypeName) == true)
                {
                    // если в значениях словаря пустой список, то считаем, что вся сущность исключена из защиты персональных данных
                    if (protectionSettings.ExcludedEntityProperties[genericTypeName]?.Any() != true)
                    {
                        continue;
                    }

                    if (protectionSettings.ExcludedEntityProperties[genericTypeName]?.Any(x => x.Equals(p.Name, StringComparison.OrdinalIgnoreCase)) == true)
                    {
                        continue;
                    }
                }

                if (p.PropertyType != typeof(string))
                {
                    throw new InvalidOperationException("[ProtectedPersonalData] only works strings by default."); // TODO - локализовать?
                }

                builder.Property(typeof(string), p.Name).HasConversion(converter);
            }
        }
    }
}