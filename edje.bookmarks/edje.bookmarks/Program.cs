using bookmarks.Extensions;
using Edje.Bookmarks.Db.Extensions;
using Edje.Bookmarks.Services;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Hosting;
using UrlDirParser.Extensions;

namespace Edje.Bookmarks;

[Command(Name = "bookmarks", Description = "Bookmarks Parser")]
internal class Program
{
    public static async Task<int> Main(string[] args) => await new HostBuilder()
            .ReadConfiguration()
            .AddLogging()
            .AddBookmarkServices()
            .AddUrlDirParser()
            .AddBookmarkDb()
            .RunCommandLineApplicationAsync<BookmarkCliService>(args);
}