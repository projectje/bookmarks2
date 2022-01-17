using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scrutor;
using Serilog;
using Serilog.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;

namespace bookmarks.Extensions;

public static class BookmarkExtensions
{
    /// <summary>
    /// Configuration
    /// </summary>
    public static IHostBuilder ReadConfiguration(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureAppConfiguration(hostConfig =>
        {
            hostConfig.SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory);
            hostConfig.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            hostConfig.AddEnvironmentVariables();
            hostConfig.AddIniFile("appsettings.ini", optional: true, reloadOnChange: true);
            hostConfig.AddXmlFile("apssettings.xml", optional: true, reloadOnChange: true);
        });

        return hostBuilder;
    }

    /// <summary>
    /// Services
    /// </summary>
    public static IHostBuilder AddBookmarkServices(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((context, services) =>
        {
            services
                .Scan(scan => scan
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses()
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .WithTransientLifetime());
        });
        return hostBuilder;
    }

    /// <summary>
    /// Logging
    /// </summary>
    public static IHostBuilder AddLogging(this IHostBuilder hostBuilder)
    {
        LoggerProviderCollection Providers = new();

        hostBuilder.ConfigureLogging((context, builder) =>
        {
            LoggerConfiguration config = new LoggerConfiguration()
                   .ReadFrom.Configuration(context.Configuration)
                   .WriteTo.Providers(Providers);

            config.Enrich.FromLogContext();
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            Serilog.Core.Logger? logger = config.CreateLogger();

            builder.AddSerilog(logger);
        });

        return hostBuilder;
    }
}