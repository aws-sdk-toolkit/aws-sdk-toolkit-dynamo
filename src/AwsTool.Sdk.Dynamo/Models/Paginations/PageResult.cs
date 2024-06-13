using System.Text;

namespace AwsTool.Sdk.Dynamo.Models.Paginations;

/// <summary>
/// Result of a query paged in dynamo.
/// </summary>
/// <typeparam name="T">Type that represents the table data.</typeparam>
public class PageResult<T> where T : new()
{
    /// <summary>
    /// Result found in the query.
    /// </summary>
    public IEnumerable<T> Value { get; }
    
    /// <summary>
    /// Next page if existing.
    /// </summary>
    public string? NextPage { get; }

    /// <summary>
    /// Creates an instance of <see cref="PageResult{T}"/>.
    /// </summary>
    /// <param name="value">Query result.</param>
    /// <param name="nextPage">Identifier of the next page.</param>
    public PageResult(IEnumerable<T> value, string? nextPage)
    {
        Value = value;
        NextPage = ToBase64(nextPage);
    }
    
    private string? ToBase64(string value)
    {
        if (string.IsNullOrEmpty(value) || "{}".Equals(value)) return null;
        
        var bytes = Encoding.ASCII.GetBytes(value);
        return Convert.ToBase64String(bytes);
    }
}