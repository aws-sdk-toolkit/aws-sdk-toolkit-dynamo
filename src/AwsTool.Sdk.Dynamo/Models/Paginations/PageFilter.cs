using System.Text;

namespace AwsTool.Sdk.Dynamo.Models.Paginations;

public class PageFilter
{
    public int Limit { get; }
    private readonly string _page;

    public PageFilter(int? limit, string page)
    {
        Limit = limit > 0 ? limit.Value : 10;
        _page = page;
    }

    public string? GetPage()
    {
        if (string.IsNullOrEmpty(_page)) return null;
        
        var bytes = Convert.FromBase64String(_page);
        return Encoding.ASCII.GetString(bytes);
    }
}