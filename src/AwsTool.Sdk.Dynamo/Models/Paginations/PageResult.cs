using System.Text;

namespace AwsTool.Sdk.Dynamo.Models.Paginations;

public class PageResult<T> where T : new()
{
    public IEnumerable<T> Value { get; }
    public string? NextPage { get; }

    public PageResult(IEnumerable<T> value, string? nextPage)
    {
        Value = value;
        NextPage = ToBase64(nextPage);
    }
    
    private string? ToBase64(string value)
    {
        if (string.IsNullOrEmpty(value)) return null;
        
        var bytes = Encoding.ASCII.GetBytes(value);
        return Convert.ToBase64String(bytes);
    }
}