using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scrutor;
using System.Reflection;
using UrlDirParser.Options;

namespace UrlDirParser.Extensions;

public static class ParseUrlDirExtensions
{
    internal static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name
        ?? "Edje.Bookmarks.UrlDirParser";

    private static readonly string SettingsPath = AssemblyName.Replace('.', ':');

    public static IHostBuilder AddUrlDirParser(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((context, services) =>
        {
            services.Configure<UrlDirParserOptions>(context.Configuration.GetSection(SettingsPath));

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
}