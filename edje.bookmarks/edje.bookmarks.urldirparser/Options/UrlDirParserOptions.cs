using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlDirParser.Options;

public class FaviconGrabbersOptions
{
    public string Grabber { get; set; } = string.Empty;

    public string Empty { get; set; } = string.Empty;
}

public class UrlDirParserOptions
{
    public List<string> bookmarkDirs { get; set; } = new List<string>();

    public string OutputDir { get; set; } = string.Empty;

    public string FavIconDir { get; set; } = string.Empty;

    public List<FaviconGrabbersOptions> FaviconGrabbers { get; set; } = new List<FaviconGrabbersOptions>();
}