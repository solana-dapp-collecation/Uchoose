// ------------------------------------------------------------------------------------------------------
// <copyright file="BusinessRuleValidationException.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;

using Uchoose.Domain.Contracts;

namespace Uchoose.Domain.Exceptions
{
    /// <summary>
    /// Исключение, указывающее на то, что бизнес-правило не выполнилось.
    /// </summary>
    public class BusinessRuleValidationException :
        DomainException
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="BusinessRuleValidationException"/>.
        /// </summary>
        /// <param name="brokenRule"><see cref="IBusinessRule"/>.</param>
        public BusinessRuleValidationException(IBusinessRule brokenRule)
            : base(brokenRule.Message, statusCode: HttpStatusCode.Conflict)
        {
            BrokenRule = brokenRule;
        }

        /// <summary>
        /// <see cref="IBusinessRule"/>.
        /// </summary>
        public IBusinessRule BrokenRule { get; }
    }
}