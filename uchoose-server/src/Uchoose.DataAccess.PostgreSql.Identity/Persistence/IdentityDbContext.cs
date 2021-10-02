// ------------------------------------------------------------------------------------------------------
// <copyright file="IdentityDbContext.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Uchoose.CurrentUserService.Interfaces;
using Uchoose.DataAccess.Identity.Interfaces.Contexts;
using Uchoose.DataAccess.Interfaces.Contexts;
using Uchoose.DataAccess.Interfaces.Contracts;
using Uchoose.DataAccess.Interfaces.EventLogging;
using Uchoose.DataAccess.Interfaces.Settings;
using Uchoose.DataAccess.PostgreSql.Extensions;
using Uchoose.DateTimeService.Interfaces;
using Uchoose.Domain.Abstractions;
using Uchoose.Domain.Entities;
using Uchoose.Domain.Identity.Entities;
using Uchoose.Domain.Identity.Entities.ExtendedAttributes;
using Uchoose.Domain.Settings;
using Uchoose.SerializationService.Interfaces;

namespace Uchoose.DataAccess.PostgreSql.Identity.Persistence
{
    /// <inheritdoc cref="IIdentityDbContext"/>
    internal sealed class IdentityDbContext :
        IdentityDbContext<UchooseUser, UchooseRole, Guid, UchooseUserClaim, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, UchooseRoleClaim, IdentityUserToken<Guid>>,
        IIdentityDbContext,
        IExtendedAttributeDbContext<Guid, UchooseUser, UchooseUserExtendedAttribute>,
        IExtendedAttributeDbContext<Guid, UchooseRole, UchooseRoleExtendedAttribute>
    {
        private readonly IEventLogger _eventLogger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;
        private readonly IDateTimeService _dateTimeService;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IOptionsSnapshot<EntitySettings> _entitySettings;
        private readonly ProtectionSettings _protectionSettings;
        private readonly IPersonalDataProtector _protector;

        /// <summary>
        /// Инициализирует экземпляр <see cref="IdentityDbContext"/>.
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions"/>.</param>
        /// <param name="eventLogger"><see cref="IEventLogger"/>.</param>
        /// <param name="currentUserService"><see cref="ICurrentUserService"/>.</param>
        /// <param name="mediator"><see cref="IMediator"/>.</param>
        /// <param name="dateTimeService"><see cref="IDateTimeService"/>.</param>
        /// <param name="jsonSerializer"><see cref="IJsonSerializer"/>.</param>
        /// <param name="entitySettings"><see cref="EntitySettings"/>.</param>
        /// <param name="protectionSettings"><see cref="ProtectionSettings"/>.</param>
        /// <param name="protector"><see cref="IPersonalDataProtector"/>.</param>
        public IdentityDbContext(
            DbContextOptions<IdentityDbContext> options,
            IEventLogger eventLogger,
            ICurrentUserService currentUserService,
            IMediator mediator,
            IDateTimeService dateTimeService,
            IJsonSerializer jsonSerializer,
            IOptionsSnapshot<EntitySettings> entitySettings,
            IOptionsSnapshot<ProtectionSettings> protectionSettings,
            IPersonalDataProtector protector)
                : base(options)
        {
            _eventLogger = eventLogger ?? throw new ArgumentNullException(nameof(eventLogger));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
            _entitySettings = entitySettings ?? throw new ArgumentNullException(nameof(entitySettings));
            _protectionSettings = protectionSettings?.Value ?? throw new ArgumentNullException(nameof(protectionSettings));
            _protector = protector;
        }

        /// <inheritdoc/>
        public IDbConnection Connection => Database.GetDbConnection();

        /// <inheritdoc/>
        public bool HasChanges => ChangeTracker.HasChanges();

        /// <inheritdoc/>
        public DbSet<Audit> AuditTrails { get; set; }

        /// <inheritdoc/>
        DbSet<UchooseUserExtendedAttribute> IExtendedAttributeDbContext<Guid, UchooseUser, UchooseUserExtendedAttribute>.ExtendedAttributes { get; set; }

        /// <inheritdoc/>
        DbSet<UchooseRoleExtendedAttribute> IExtendedAttributeDbContext<Guid, UchooseRole, UchooseRoleExtendedAttribute>.ExtendedAttributes { get; set; }

        /// <summary>
        /// Схема БД, используемая по умолчанию.
        /// </summary>
        internal static string Schema => "Identity";

        /// <inheritdoc/>
        DbSet<UchooseUser> IExtendedAttributeDbContext<Guid, UchooseUser, UchooseUserExtendedAttribute>.GetEntities() => Users;

        /// <inheritdoc/>
        DbSet<UchooseRole> IExtendedAttributeDbContext<Guid, UchooseRole, UchooseRoleExtendedAttribute>.GetEntities() => Roles;

        #region SaveChanges

        /// <inheritdoc/>
        public async Task<int> BaseSaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc cref="ISupportsSavingChanges.SaveChangesAsync"/>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await this.SaveChangesWithAuditAndPublishEventsAsync(_eventLogger, _mediator, _currentUserService, _dateTimeService, _jsonSerializer, _entitySettings, cancellationToken);
        }

        /// <inheritdoc cref="ISupportsSavingChanges.SaveChanges"/>
        public override int SaveChanges()
        {
            return this.SaveChangesWithAuditAndPublishEvents(_eventLogger, _mediator, _currentUserService, _dateTimeService, _jsonSerializer, _entitySettings);
        }

        /// <inheritdoc/>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return SaveChanges();
        }

        #endregion SaveChanges

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(Schema);
            builder.Ignore<DomainEvent>();
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // builder.ApplyIdentityConfiguration(_protectionSettings, _protector);
        }
    }
}