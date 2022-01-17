namespace Edje.Bookmarks.UrlDirParser.Services.Models;

/// <summary>
/// A bookmark always has one main category but can in addition be part of multiple categories
/// </summary>
public class Category
{
    public string Title { get; set; } = "category";

    public string? SubTitle { get; set; }

    public string? SubTitleLink { get; set; }

    public string? UniqueId { get; set; }

    /// <summary>
    /// Sub categories
    /// </summary>
    public List<Category>? CategoryList { get; set; }

    /// <summary>
    /// Bookmarks in category
    /// </summary>
    public List<Bookmark>? BookmarkList { get; set; }
}