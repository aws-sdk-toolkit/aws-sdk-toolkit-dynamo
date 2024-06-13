namespace AwsTool.Sdk.Dynamo.Models.Tables;

/// <summary>
/// Identifies the property type.
/// </summary>
public enum PropertyType
{
    /// <summary>
    /// Used for alphanumeric fields and dates.
    /// </summary>
    String,
    /// <summary>
    /// Used in numeric values.
    /// </summary>
    Numeric
}