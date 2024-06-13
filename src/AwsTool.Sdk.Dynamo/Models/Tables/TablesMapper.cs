namespace AwsTool.Sdk.Dynamo.Models.Tables;

/// <summary>
/// Represents the structure of a table in Dynamo.
/// </summary>
public class TablesMapper
{
    /// <summary>
    /// Table name.
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// Properties of the item stored in the table.
    /// </summary>
    public IEnumerable<SchemaTable> Schema { get; set; }
}