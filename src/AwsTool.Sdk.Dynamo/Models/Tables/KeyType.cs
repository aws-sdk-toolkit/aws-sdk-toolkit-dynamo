namespace AwsTool.Sdk.Dynamo.Models.Tables;

/// <summary>
/// Represents the key types of a table in Dynamo.
/// </summary>
public enum KeyType
{
    /// <summary>
    /// Represents the identifier.
    /// </summary>
    Hash,
    /// <summary>
    /// Represents the secondary identifier.
    /// </summary>
    Range
}