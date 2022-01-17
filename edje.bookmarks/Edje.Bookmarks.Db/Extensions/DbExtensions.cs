using Edje.Bookmarks.Db.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scrutor;
using System.Reflection;

namespace Edje.Bookmarks.Db.Extensions
{
    public static class DbExtensions
    {
        internal static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name
       ?? "Edje.Bookmarks.Db";

        private static readonly string SettingsPath = AssemblyName.Replace('.', ':');

        public static IHostBuilder AddBookmarkDb(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<BookmarkContext>(options =>
                {
                    options.UseSqlite(context.Configuration.GetConnectionString("BookmarkDb"));
                });

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
}