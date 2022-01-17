using Edje.Bookmarks.UrlDirParser.Services;
using Edje.Bookmarks.UrlDirParser.Services.Models;
using IniParser;
using IniParser.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;
using UrlDirParser.Options;

namespace UrlDirParser;

public interface IPreProcessUrlDirService
{
    Task<bool> ProcessBookmarkCategoryFolderAsync(string folder, string rootFolder);

    Task<bool> ProcessBookmarkRootFoldersAsync();

    Task<bool> ProcessFileAsync(string fileName, string rootFolder);

    Task<bool> ReadWriteUrlFileAsync(string fileName, string rootFolder);
}

public class PreProcessUrlDirService : IPreProcessUrlDirService
{
    private const string defaultUrlSection = "InternetShortcut";
    private const string bookmarkSection = "Bookmark";
    private const string categoryFileName = "category.json";

    private readonly UrlDirParserOptions _options;
    private readonly ILogger<IPreProcessUrlDirService> _logger;

    public PreProcessUrlDirService(IOptionsMonitor<UrlDirParserOptions> options
        , ILogger<IPreProcessUrlDirService> logger)
    {
        _options = options.CurrentValue;
        _logger = logger;
    }

    /// <summary>
    /// Process all indicated folders containing bookmarks
    /// </summary>
    public async Task<bool> ProcessBookmarkRootFoldersAsync()
    {
        foreach (string? folder in _options.bookmarkDirs)
        {
            if (folder is not null)
            {
                await ProcessBookmarkCategoryFolderAsync(folder, folder);
            }
        }
        return true;
    }

    /// <summary>
    /// A category folder can contain bookmark files that belong to the category. It can also
    /// contains subcategory folders that contain bookmark files.
    /// If a category contains no bookmark files it is not outputted as a seperate category
    /// file since it is consided a path to a category.
    /// </summary>
    public async Task<bool> ProcessBookmarkCategoryFolderAsync(string folder, string rootFolder)
    {
        // Process the list of files found in the directory.
        await ProcessFileAsync(folder, rootFolder);

        // Recurse into subdirectories of this directory.
        string[] subdirectoryEntries = Directory.GetDirectories(folder);
        foreach (string subdirectory in subdirectoryEntries)
            await ProcessBookmarkCategoryFolderAsync(subdirectory, rootFolder);

        return true;
    }

    /// <summary>
    /// Handles files for now only .URL files
    /// </summary>
    /// <param name="path"></param>
    public async Task<bool> ProcessFileAsync(string folder, string rootFolder)
    {
        string[] fileEntries = Directory.GetFiles(folder);

        // If there is no category.json file then create it
        if (!fileEntries.Any(x => x == categoryFileName))
        {
            Category category = new()
            {
                Title = Path.GetDirectoryName(folder) ?? "New"
            };
            string serializedCategory = JsonSerializer.Serialize(category,
                new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            File.WriteAllText(Path.Combine(folder, categoryFileName), serializedCategory);
            _logger.LogDebug("wrote new category json for {folder}", folder);
        }

        // process url files
        foreach (string fileName in fileEntries)
        {
            _logger.LogDebug("Processing file {file}", fileName);

            if (Path.GetExtension(fileName).ToLowerInvariant() == ".url")
            {
                string? directory = Path.GetDirectoryName(fileName);
                if (directory is not null)
                {
                    await ReadWriteUrlFileAsync(fileName, rootFolder);
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Reads a url file and updates it with meta information
    /// </summary>
    public async Task<bool> ReadWriteUrlFileAsync(string fileName, string rootFolder)
    {
        FileIniDataParser? parser = new();
        IniData data = parser.ReadFile(fileName);
        string url = data[defaultUrlSection]["URL"];
        Console.WriteLine("url: '{0}'", url);
        //data[defaultUrlSection].RemoveKey("maincategory");
        data[bookmarkSection].AddFile(fileName, rootFolder);
        data[bookmarkSection].AddDomain(url);
        await data[bookmarkSection].AddFavicon(url, _options);
        parser.WriteFile(fileName, data);
        return true;
    }
}