// ------------------------------------------------------------------------------------------------------
// <copyright file="ISupportsCheckingRules.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// The Application under the Commercial license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Uchoose.Domain.Exceptions;

namespace Uchoose.Domain.Contracts
{
    /// <summary>
    /// Поддерживает проверку <see cref="IBusinessRule"/>.
    /// </summary>
    public interface ISupportsCheckingRules
    {
        /// <summary>
        /// Проверить бизнес-правило.
        /// </summary>
        /// <param name="rule">Бизнес-правило.</param>
        public virtual void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}