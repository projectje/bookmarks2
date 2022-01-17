using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookmarks.Settings;

public class BookmarkOptions
{
    private List<string> UrlDirs { get; set; } = new List<string>();

    private string JsonOutputDir { get; set; } = string.Empty;
}