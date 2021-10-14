// ------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Life Loop">
// Copyright (c) Life Loop, 2021. All rights reserved.
// The core dev team: Nikolay Chebotov (unchase), Leonov Dmitry (gunfighter).
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Uchoose.Api.Common.Settings;
using Uchoose.DataAccess.Interfaces.Settings;
using Uchoose.Utils.Extensions;

namespace Uchoose.Bootstrapper
{
    public class Program
    {
        private static IConfiguration _configuration;

        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            try
            {
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                // var localizer = host.Services.GetRequiredService(typeof(IStringLocalizer)) as IStringLocalizer; // TODO - проверить

                Log.Fatal(ex, "A fatal error has occurred!"); // TODO - локализовать
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration(builder =>
                    {
                        const string configsDirectory = "Configs";
                        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
                        builder // TODO - вынести в методы расширения
                            .AddJsonFile(Path.Combine(configsDirectory, "appsettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, $"appsettings.{env}.json"), true, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "loggingsettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, $"loggingsettings.{env}.json"), true, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "cachesettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "jwtsettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, $"jwtsettings.{env}.json"), true, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "mailsettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, $"mailsettings.{env}.json"), true, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "smssettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, $"smssettings.{env}.json"), true, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "corssettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "forwardingsettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, $"forwardingsettings.{env}.json"), true, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "swaggersettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "kestrelsettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, $"kestrelsettings.{env}.json"), true, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "basicauthsettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, $"basicauthsettings.{env}.json"), true, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "hangfiresettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "identitysettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, $"identitysettings.{env}.json"), true, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "entitysettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, $"entitysettings.{env}.json"), true, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "behaviorsettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, $"behaviorsettings.{env}.json"), true, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "protectionsettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, $"protectionsettings.{env}.json"), true, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "localizationsettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "mongodbsettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, $"mongodbsettings.{env}.json"), true, true)
                            .AddJsonFile(Path.Combine(configsDirectory, "solanasettings.json"), false, true)
                            .AddJsonFile(Path.Combine(configsDirectory, $"solanasettings.{env}.json"), true, true)
                            .AddEnvironmentVariables();

                        // .AddUserSecrets()

                        _configuration = builder.Build();
                    });

#pragma warning disable RCS1163
                    webBuilder.ConfigureKestrel(options =>
#pragma warning restore RCS1163
                    {
                        var protectionSettings = _configuration.GetSettings<ProtectionSettings>();
                        var kestrelSettings = _configuration.GetSettings<KestrelSettings>();

#if !DEBUG
                        options.Listen(IPAddress.Loopback, kestrelSettings.HttpPort);

                        using var store = new X509Store(StoreName.Root);
                        store.Open(OpenFlags.ReadOnly);

                        var certs = store.Certificates.Find(X509FindType.FindBySubjectName, protectionSettings.CertificateSubjectName, false);
                        if (certs.Count > 0)
                        {
                            var certificate = certs[0];

                            Log.Information("Security certificate founded: {CertificateSubjectName} {CertificateFriendlyName}...", certificate.SubjectName.Name, certificate.FriendlyName);

                            options.Listen(IPAddress.Loopback, kestrelSettings.HttpsPort, listenOptions =>
                            {
                                listenOptions.UseHttps(certificate);
                            });
                        }
                        else
                        {
                            Log.Warning("Security certificate not found!");
                        }

#endif
                    });
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel();
                })
                .UseSerilog((hostingContext, loggerConfiguration) =>
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")));
    }
}