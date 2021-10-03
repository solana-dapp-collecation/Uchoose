// ------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Uchoose.Api.Common.Filters.Swagger;
using Uchoose.Api.Common.Interceptors;
using Uchoose.Api.Common.Middlewares;
using Uchoose.Api.Common.Permissions;
using Uchoose.Api.Common.Providers;
using Uchoose.Api.Common.Settings;
using Uchoose.Api.Common.Swagger.Filters;
using Uchoose.Api.Common.Swagger.Providers;
using Uchoose.CurrentUserService.Extensions;
using Uchoose.DataAccess.Interfaces;
using Uchoose.DataAccess.PostgreSql.Extensions;
using Uchoose.DataAccess.PostgreSql.Identity.Extensions;
using Uchoose.DateTimeService.Extensions;
using Uchoose.Domain.Identity.Exceptions;
using Uchoose.Domain.Settings;
using Uchoose.EventLogService.Extensions;
using Uchoose.ExcelService.Extensions;
using Uchoose.IdentityService.Extensions;
using Uchoose.LocalFileStorageService.Extensions;
using Uchoose.MailService.Interfaces.Settings;
using Uchoose.RoleClaimService.Extensions;
using Uchoose.RoleService.Extensions;
using Uchoose.SerializationService.Extensions;
using Uchoose.SmsService.Interfaces.Settings;
using Uchoose.SmtpMailService.Extensions;
using Uchoose.TokenService.Extensions;
using Uchoose.TokenService.Interfaces.Settings;
using Uchoose.TwilioSmsService.Extensions;
using Uchoose.UseCases.Common.Extensions;
using Uchoose.UserClaimService.Extensions;
using Uchoose.UserService.Extensions;
using Uchoose.Utils.Exceptions;
using Uchoose.Utils.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Filters;

namespace Uchoose.Api.Common
{
#pragma warning disable 1591
    public static class Startup
#pragma warning restore 1591
    {
        #region ConfigureServices

        #region Add Common Api

        /// <summary>
        /// Добавить сервисы общего API.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddCommonApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddCaching();
            services.AddSerialization(configuration);
            services.AddCommonInfrastructure(configuration);
            services.AddInfrastructureServices(configuration);
            services.AddPersistence(configuration);
            services.AddApplicationServices(configuration);
            services.AddPermissions();
            services.AddJwtAuthentication(configuration);
            services.AddInterceptors();
            services.AddCommonUseCases(configuration);

            return services;
        }

        #endregion Add Common Api

        #region Add caching

        /// <summary>
        /// Добавить кэширование.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddCaching(this IServiceCollection services)
        {
            return services
                .AddMemoryCache()
                .AddDistributedMemoryCache();
        }

        #endregion

        #region Add serialization

        /// <summary>
        /// Добавить сериализацию json.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddSerialization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJsonSerializer();

            return services;
        }

        #endregion

        #region Add common infrastructure

        /// <summary>
        /// Добавить сервисы общей инфраструктуры.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddCommonInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSettings<EntitySettings>(configuration);
            services.AddApplicationPersistence(configuration);
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            services.AddUchooseControllers(configuration);
            services.AddLocalization(configuration);
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddBasicAuthHandler(configuration);

            services.AddSettings<HangfireSettings>(configuration);
            services.AddHangfireServer(options =>
            {
                var hangfireSettings = configuration.GetSettings<HangfireSettings>();
                options.ServerName = hangfireSettings.ServerName;
            });
            services.AddSingleton<ExceptionHandlingMiddleware>();
            services.AddSwaggerDocumentation(configuration);
            services.AddForwardingProxy(configuration);
            services.AddCorsPolicy(configuration);
            services.AddSignalR();

            return services;
        }

        #endregion

        #region Add localization

        /// <summary>
        /// Добавить локализацию.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddLocalization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSettings<LocalizationSettings>(configuration);
            var localizationSettings = configuration.GetSettings<LocalizationSettings>();

            services.AddLocalization(options =>
            {
                options.ResourcesPath = localizationSettings.ResourcesPath;
            });

            var supportedCultures = localizationSettings.SupportedLanguages.ConvertAll(x => new CultureInfo(x.Code));
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new(supportedCultures[0]);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            return services;
        }

        #endregion Add localization

        #region Add services

        /// <summary>
        /// Добавить сервисы инфраструктуры.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCurrentUserService();
            services.AddDateTimeService();
            services
                .AddSettings<MailSettings>(configuration)
                .AddSmtpMailService();
            services
                .AddSettings<SmsSettings>(configuration)
                .AddTwilioSmsService();
            return services;
        }

        /// <summary>
        /// Добавить сервисы приложения.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityServices(configuration);
            services.AddExcelService();
            services
                .AddLocalFileStorageService();

                // .AddMongoDbFileStorageService(); // TODO - заменить на Mongo?

            services.AddEventLogService();

            return services;
        }

        /// <summary>
        /// Добавить сервисы Identity.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddIdentityService()
                .AddRoleService()
                .AddRoleClaimService()
                .AddUserClaimService()
                .AddSettings<JwtSettings>(configuration)
                .AddTokenService()
                .AddUserService();

            return services;
        }

        #endregion Add services

        #region Add persistence

        /// <summary>
        /// Добавить постоянные хранилища данных (БД).
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityPersistence(configuration);

            // services.AddNFTPersistence(configuration);

            // TODO: добавить больше persistence
            return services;
        }

        #endregion

        #region Add Controllers

        /// <summary>
        /// Добавить контроллеры с валидаторами.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddUchooseControllers(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddControllers() // TODO - добавить фильтр валидации https://stackoverflow.com/questions/55733521/asp-net-core-validation-after-filters, https://www.yogihosting.com/aspnet-core-filters/
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                })
                .AddMvcOptions(options =>
                {
                    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((value, propertyName)
                        => throw new BadRequestException(
                            $"{propertyName}: value '{value}' is invalid.")); // TODO - локализовать

                    // TODO - нужно ли остальные accessor'ы проверить?
                })
                .AddDataAnnotationsLocalization()
                .AddValidators();

            return services;
        }

        #endregion Add Controllers

        #region Add swagger

        /// <summary>
        /// Добавить swagger (OpenAPI) документацию.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSettings<SwaggerSettings>(configuration);
            var swaggerSettings = configuration.GetSettings<SwaggerSettings>();

            services.AddSwaggerGen(options =>
            {
                #region Add xml-commеnts

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!assembly.IsDynamic)
                    {
                        string xmlFile = $"{assembly.GetName().Name}.xml";
                        string xmlPath = Path.Combine(baseDirectory, xmlFile);
                        if (File.Exists(xmlPath))
                        {
                            if (swaggerSettings.IncludeXmlCommentsWithRemarks)
                            {
                                options.IncludeXmlCommentsWithRemarks(xmlPath);
                            }
                            else
                            {
                                options.IncludeXmlComments(xmlPath);
                            }
                        }
                    }
                }

                options.UseAllOfToExtendReferenceSchemas();
                if (swaggerSettings.IncludeXmlCommentsFromInheritDocs)
                {
                    options.IncludeXmlCommentsFromInheritDocs(true);
                }

                #endregion Add xml-commеnts

                options.AddSwaggerDocs(swaggerSettings);

                #region Add Versioning

                options.OperationFilter<RemoveVersionFromParameterFilter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                options.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out var methodInfo))
                    {
                        return false;
                    }

                    var versions = methodInfo
                        .DeclaringType?
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    var maps = methodInfo
                        .GetCustomAttributes(true)
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToList();

                    return versions?.Any(v => $"v{v}" == version) == true
                           && (maps.Count == 0 || maps.Any(v => $"v{v}" == version));
                });

                #endregion Add Versioning

                #region Add JWT Auth

                options.AddSecurityDefinition("Bearer", new()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - 'Bearer {your token here}' to access this API" // TODO - локализовать
                });
                options.AddSecurityRequirement(new()
                {
                    {
                        new()
                        {
                            Reference = new()
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        Array.Empty<string>()
                    }
                });

                #endregion Add JWT Auth

                #region Add Basic Auth

                var basicAuthSettings = configuration.GetSettings<BasicAuthSettings>();
                if (basicAuthSettings.UseBasicAuthAttribute)
                {
                    var basicSecurityScheme = new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Reference = new()
                        {
                            Id = "BasicAuth",
                            Type = ReferenceType.SecurityScheme
                        },
                        Scheme = "Basic"
                    };
                    options.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
                    options.AddSecurityRequirement(new()
                    {
                        {
                            basicSecurityScheme,
                            Array.Empty<string>()
                        }
                    });
                }

                #endregion Add Basic Auth

                options.EnableAnnotations();

                options.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Nullable = true,
                    Pattern = @"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$",

                    // Pattern = @"^(\d\d):(60|([0-5][0-9])):(60|([0-5][0-9]))$",
                    Example = new OpenApiString("00:00:01")
                });

                options.OperationFilter<SwaggerExcludeFilter>();
                options.IgnoreObsoleteProperties();
                options.IgnoreObsoleteActions();

                options.ExampleFilters();
                options.DocumentFilter<AppendActionCountToTagSummaryDocumentFilter>("(методов: {0})"); // TODO - вынести в настройки, локализовать
                options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.RelativePath}");

                // options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>(); // TODO - вынести в настройки
                options.DocumentFilter<TagOrderByNameDocumentFilter>();

                if (swaggerSettings.AddEnumsWithValuesFixFilters)
                {
                    options.AddEnumsWithValuesFixFilters(services, o =>
                    {
                        o.ApplyDocumentFilter = true;
                        o.ApplyParameterFilter = true;
                        o.ApplySchemaFilter = true;
                        o.IncludeDescriptions = true;
                        o.IncludeXEnumRemarks = true;
                        o.DescriptionSource = DescriptionSources.XmlComments;
                    });
                }

                options.OperationFilter<SwaggerLanguageHeaderOperationFilter>();
            });
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());

            if (swaggerSettings.UseCaching)
            {
                services.Replace(ServiceDescriptor.Transient<ISwaggerProvider, CachingSwaggerProvider>());
            }

            return services;
        }

        #region Add Swagger Docs

        /// <summary>
        /// Добавить документации swagger.
        /// </summary>
        /// <param name="options"><see cref="SwaggerGenOptions"/>.</param>
        /// <param name="swaggerSettings"><see cref="SwaggerSettings"/>.</param>
        private static void AddSwaggerDocs(this SwaggerGenOptions options, SwaggerSettings swaggerSettings)
        {
            foreach (var swaggerDoc in swaggerSettings.SwaggerDocs)
            {
                if (swaggerDoc.Enabled)
                {
                    options.SwaggerDoc(swaggerDoc.Version, new()
                    {
                        Version = swaggerDoc.Version,
                        Title = swaggerDoc.Title,
                        Description = swaggerDoc.Description
                    });
                }
            }
        }

        #endregion Add Swagger Docs

        #endregion

        #region Add CORS Policy

        /// <summary>
        /// Добавить политику CORS.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSettings<CorsSettings>(configuration);
            var corsSettings = configuration.GetSettings<CorsSettings>();
            return services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(corsSettings.Blazor);
                });
            });
        }

        #endregion Add CORS Policy

        #region Add Basic auth handler

        /// <summary>
        /// Добавить обработчик Basic авторизации.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddBasicAuthHandler(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSettings<BasicAuthSettings>(configuration);
            services.AddTransient<BasicAuthMiddleware>();

            return services;
        }

        #endregion Add Basic auth handler

        #region Add Forwarding Proxy

        /// <summary>
        /// Добавить перенаправление через прокси.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddForwardingProxy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSettings<ForwardingSettings>(configuration);
            var forwardingSettings = configuration.GetSettings<ForwardingSettings>();
            if (forwardingSettings.UseForwardingProxy)
            {
                services.Configure<ForwardedHeadersOptions>(options =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                    var proxyIps = forwardingSettings.ProxyIps.Where(x => x.IsPresent()).ToList();
                    if (proxyIps.Count > 0)
                    {
                        foreach (string proxyIp in proxyIps)
                        {
                            if (IPAddress.TryParse(proxyIp, out var proxyIpAddress))
                            {
                                options.KnownProxies.Add(proxyIpAddress);
                            }
                            else
                            {
                                // Log.Logger.Warning("Invalid Proxy IP of {ProxyIp}, Not Loaded", proxyIp); // TODO - добавить и локализовать
                            }
                        }
                    }
                });

                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder
                                .AllowCredentials()
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .WithOrigins(forwardingSettings.ApplicationUrl.TrimEnd('/'));
                        });
                });
            }

            return services;
        }

        #endregion Add Forwarding Proxy

        #region Add permissions

        /// <summary>
        /// Добавить обработку разрешений.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddPermissions(this IServiceCollection services)
        {
            services
                .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
                .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            return services;
        }

        #endregion

        #region Add JWT

        /// <summary>
        /// Добавить аутентификацию с помощью JWT.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSettings<JwtSettings>();
            byte[] key = Encoding.UTF8.GetBytes(jwtSettings.Key);
            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = jwtSettings.RequireHttpsMetadata; // TODO: поменять на true в production среде
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = jwtSettings.ValidateIssuer,
                        ValidateAudience = jwtSettings.ValidateAudience,
                        ValidAudience = jwtSettings.ValidateAudience ? jwtSettings.Audience : null,
                        ValidIssuer = jwtSettings.ValidateIssuer ? jwtSettings.Issuer : null,
                        RoleClaimType = ClaimTypes.Role,
                        ClockSkew = TimeSpan.Zero
                    };
                    bearer.Events = new()
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            if (!context.Response.HasStarted)
                            {
                                throw new IdentityException("You are not authorized.", statusCode: HttpStatusCode.Unauthorized);
                            }

                            return Task.CompletedTask;
                        },
                        OnForbidden = _ => throw new IdentityException("You are not authorized to access this resource.", statusCode: HttpStatusCode.Forbidden)
                    };
                });
            return services;
        }

        #endregion

        #region Add Interceptors

        /// <summary>
        /// Добавить перехватчики.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Возвращает <see cref="IServiceCollection"/>.</returns>
        private static IServiceCollection AddInterceptors(this IServiceCollection services)
        {
            services.AddTransient<IValidatorInterceptor, ValidatorInterceptor>();

            return services;
        }

        #endregion Add Interceptors

        #endregion ConfigureServices

        #region Configure

        #region Use Common Api

        /// <summary>
        /// Использовать в обработке запросов сервисы общего API.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>.</param>
        /// <param name="env"><see cref="IWebHostEnvironment"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IApplicationBuilder"/>.</returns>
#pragma warning disable SA1202
        public static IApplicationBuilder UseCommonApi(
#pragma warning restore SA1202
            this IApplicationBuilder app,
            IWebHostEnvironment env,
            IConfiguration configuration)
        {
            app.UseForwarding(configuration);
            app.UseExceptionHandling();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseBasicAuth();

            app.UseUchooseLocalization(configuration);

            var hangFireSettings = configuration.GetSettings<HangfireSettings>();
            app.UseHangfireDashboard(hangFireSettings.PathMatch, new()
            {
                DashboardTitle = hangFireSettings.DashboardTitle,
                AppPath = hangFireSettings.AppPath
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseUchooseStaticFiles(env);

            app.UseSwaggerDocumentation(configuration);
            app.Initialize();

            return app;
        }

        #endregion Use Common Api

        #region Initialize

        /// <summary>
        /// Инициализация приложения.
        /// </summary>
        /// <remarks>
        /// Наполняет БД начальными данными, если они не заданы.
        /// </remarks>
        /// <param name="app"><see cref="IApplicationBuilder"/>.</param>
        /// <returns>Возвращает <see cref="IApplicationBuilder"/>.</returns>
        private static IApplicationBuilder Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            foreach (var initializer in serviceScope.ServiceProvider.GetServices<IDatabaseSeeder>())
            {
                initializer.Initialize();
            }

            return app;
        }

        #endregion Initialize

        #region UseForwarding

        /// <summary>
        /// Использовать статические файлы.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IApplicationBuilder"/>.</returns>
        private static IApplicationBuilder UseForwarding(this IApplicationBuilder app, IConfiguration configuration)
        {
            var forwardingSettings = configuration.GetSettings<ForwardingSettings>();
            if (forwardingSettings.UseForwardingProxy)
            {
                app.UseCors();
                app.UseForwardedHeaders();
            }

            return app;
        }

        #endregion UseForwarding

        #region UseUchooseStaticFiles

        /// <summary>
        /// Использовать статические файлы.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>.</param>
        /// <param name="env"><see cref="IWebHostEnvironment"/>.</param>
        /// <returns>Возвращает <see cref="IApplicationBuilder"/>.</returns>
        private static IApplicationBuilder UseUchooseStaticFiles(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "ui", "resources")),
                RequestPath = "/ui/resources",
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public, max-age=2628000");
                }
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(env.ContentRootPath),
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public, max-age=2628000");
                }
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Files")),
                RequestPath = new("/Files")
            });

            return app;
        }

        #endregion UseUchooseStaticFiles

        #region UseUchooseLocalization

        /// <summary>
        /// Использовать локализацию.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IApplicationBuilder"/>.</returns>
        private static IApplicationBuilder UseUchooseLocalization(this IApplicationBuilder app, IConfiguration configuration)
        {
            var localizationSettings = configuration.GetSettings<LocalizationSettings>();

            string[] supportedCultures = localizationSettings
                .SupportedLanguages
                .ConvertAll(x => new CultureInfo(x.Code))
                .Select(a => a.TwoLetterISOLanguageName).ToArray();

            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            return app;
        }

        #endregion UseUchooseLocalization

        #region Use Swagger Documentation

        /// <summary>
        /// Использовать swagger (OpenAPI) документацию.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Возвращает <see cref="IApplicationBuilder"/>.</returns>
        private static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();

            var swaggerSettings = configuration.GetSettings<SwaggerSettings>();

            if (swaggerSettings.UseSwaggerUI)
            {
                app.UseSwaggerUI(options =>
                {
                    // options.DefaultModelsExpandDepth(-1);
                    foreach (var swaggerDoc in swaggerSettings.SwaggerDocs)
                    {
                        if (swaggerDoc.Enabled)
                        {
                            options.SwaggerEndpoint(swaggerDoc.Url, swaggerDoc.Name);
                        }
                    }

                    options.RoutePrefix = swaggerSettings.SwaggerUiRoutePrefix;
                    options.DisplayRequestDuration();
                    options.DocExpansion(DocExpansion.None);

                    options.InjectStylesheet(swaggerSettings.UseDarkTheme
                        ? "/ui/resources/Uchoose-swagger-ui-dark.css"
                        : "/ui/resources/Uchoose-swagger-ui.css");

                    options.DocumentTitle = swaggerSettings.SwaggerUiDocumentTitle;
                    options.ConfigObject.DisplayOperationId = true;
                    options.ConfigObject.ShowCommonExtensions = true;
                    options.ConfigObject.ShowExtensions = true;
                    options.EnableDeepLinking();
                });
            }

            if (swaggerSettings.UseReDoc)
            {
                app.UseReDoc(options =>
                {
                    options.RoutePrefix = swaggerSettings.ReDocRoutePrefix;
                    options.DocumentTitle = swaggerSettings.ReDocDocumentTitle;

                    // TODO - добавить стили Спартака
                    // https://github.com/Redocly/redoc/blob/master/README.md#redoc-options-object
                    // options.InjectStylesheet("/ui/resources/Uchoose-redoc.css");
                });
            }

            return app;
        }

        #endregion Use Swagger Documentation

        #region Use Exception Handling

        /// <summary>
        /// Использовать <see cref="ExceptionHandlingMiddleware"/>.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>.</param>
        /// <returns>Возвращает <see cref="IApplicationBuilder"/>.</returns>
        private static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        #endregion Use Exception Handling

        #region Use BasicAuth Handling

        /// <summary>
        /// Использовать <see cref="BasicAuthMiddleware"/>.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>.</param>
        /// <returns>Возвращает <see cref="IApplicationBuilder"/>.</returns>
        private static IApplicationBuilder UseBasicAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<BasicAuthMiddleware>();
        }

        #endregion Use BasicAuth Handling

        #endregion Configure
    }
}