using System.Text;

namespace AwsTool.Sdk.Dynamo.Models.Paginations;

/// <summary>
/// Represents a structure for pagination in table and index queries.
/// </summary>
public class PageFilter
{
    /// <summary>
    /// Limit of items per page.
    /// </summary>
    public int Limit { get; }
    private readonly string _page;

    /// <summary>
    /// Constructs an instance of type <see cref="PageFilter"/>.
    /// </summary>
    /// <param name="limit">Limit of items per page.</param>
    /// <param name="page">Page to be displayed.</param>
    public PageFilter(int? limit, string page)
    {
        Limit = limit > 0 ? limit.Value : 10;
        _page = page;
    }

    /// <summary>
    /// Returns the page to be displayed.
    /// </summary>
    /// <returns>Token that identifies pagination.</returns>
    public string? GetPage()
    {
        if (string.IsNullOrEmpty(_page)) return null;
        
        var bytes = Convert.FromBase64String(_page);
        return Encoding.ASCII.GetString(bytes);
    }
}