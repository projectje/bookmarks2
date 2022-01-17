using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlDirParser;

namespace Edje.Bookmarks.Services;

public interface IBookmarksService
{
}

[Command(Name = "BookmarkCliService", OptionsComparison = System.StringComparison.InvariantCultureIgnoreCase)]
public class BookmarkCliService
{
    private readonly ILogger<BookmarkCliService> _logger;
    private readonly IPreProcessUrlDirService _parserUrlDirService;

    public BookmarkCliService(ILogger<BookmarkCliService> logger, IPreProcessUrlDirService parserUrlDirService)
    {
        _logger = logger;
        _parserUrlDirService = parserUrlDirService;
    }

    protected void OnExecute(CommandLineApplication app)
    {
        _parserUrlDirService.ProcessBookmarkRootFoldersAsync();
    }
}