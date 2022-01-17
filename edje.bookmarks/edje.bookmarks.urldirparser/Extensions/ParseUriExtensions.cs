using IniParser.Model;
using Nager.PublicSuffix;
using UrlDirParser.Options;

namespace Edje.Bookmarks.UrlDirParser.Services;

/// <summary>
/// Stuff that goes into .URL files
/// </summary>
public static class ParseUriExtensions
{
    /// <summary>
    /// Adds a property domain to an url file
    /// </summary>
    public static KeyDataCollection AddDomain(this KeyDataCollection bookmarkSectionCollection, string url)
    {
        string? domain = null;
        if (url.StartsWith("http"))
        {
            Uri uri = new(url);
            string? host = uri.Host;
            if (host is not null)
            {
                DomainParser? domainParser = new DomainParser(new WebTldRuleProvider());
                if (domainParser is not null)
                {
                    DomainInfo? domainInfo = domainParser.Parse(host);
                    if (domainInfo is not null)
                    {
                        domain = domainInfo.Domain;
                    }
                }
            }
        }
        if (domain is not null)
        {
            bookmarkSectionCollection.AddKey("Domain", domain);
        }
        return bookmarkSectionCollection;
    }

    /// <summary>
    /// Add file and directory info to an url file
    /// </summary>
    public static KeyDataCollection AddFile(this KeyDataCollection bookmarkSectionCollection, string fileName,
        string rootFolder)
    {
        bookmarkSectionCollection.AddKey("Title", Path.GetFileNameWithoutExtension(fileName));
        string? directory = Path.GetDirectoryName(fileName);
        if (directory is not null)
        {
            bookmarkSectionCollection.AddKey("MainCategory", Path.GetRelativePath(rootFolder, directory));
        }
        return bookmarkSectionCollection;
    }

    /// <summary>
    /// Downloads a favicon and includes the reference in the ini file
    /// </summary>
    public static async Task<KeyDataCollection> AddFavicon(this KeyDataCollection bookmarkSectionCollection,
        string url, UrlDirParserOptions options)
    {
        Uri uri = new(url);
        string? host = uri.Host;
        if (host is not null)
        {
            IEnumerable<string>? hostChunks = host.Split(".").Reverse();
            if (hostChunks is not null && hostChunks.Any())
            {
                string? reverseHost = string.Join('\\', hostChunks);
                if (reverseHost is not null)
                {
                    string favIconPathIco = options.FavIconDir + reverseHost + @"\Favicon.ico";
                    string favIconPathPng = options.FavIconDir + reverseHost + @"\Favicon.png";

                    // check if file already exists
                    if (File.Exists(favIconPathIco))
                    {
                        bookmarkSectionCollection.AddKey("Favicon", favIconPathIco);
                    }
                    else if (File.Exists(favIconPathPng))
                    {
                        bookmarkSectionCollection.AddKey("Favicon", favIconPathPng);
                    }
                    else
                    {
                        foreach (FaviconGrabbersOptions? grabber in options.FaviconGrabbers)
                        {
                            if (grabber is not null)
                            {
                                string? grabberUrl = string.Format(grabber.Grabber, host);
                                if (grabberUrl is not null)
                                {
                                    try
                                    {
                                        HttpClient client = new HttpClient();
                                        byte[]? arrBytes = await client.GetByteArrayAsync(grabberUrl);
                                        if (arrBytes is not null)
                                        {
                                            Directory.CreateDirectory(options.FavIconDir + reverseHost);
                                            File.WriteAllBytes(favIconPathIco, arrBytes);
                                            Console.WriteLine("icon downloaded using: '{0}' to {1}", grabberUrl, favIconPathIco);
                                            bookmarkSectionCollection.AddKey("Favicon", favIconPathIco);
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                        // try another one
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return bookmarkSectionCollection;
    }
}