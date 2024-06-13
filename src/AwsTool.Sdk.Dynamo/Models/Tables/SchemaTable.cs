using Amazon.DynamoDBv2;

namespace AwsTool.Sdk.Dynamo.Models.Tables;

/// <summary>
/// Represents the definitions of a property in the table.
/// </summary>
public class SchemaTable
{
    /// <summary>
    /// Property name.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Represents the key types of a table in Dynamo..
    /// </summary>
    public KeyType KeyType { get; set; }
    /// <summary>
    /// Identifies the type of property.
    /// </summary>
    public PropertyType PropertyType { get; set; }
    
    internal Amazon.DynamoDBv2.KeyType GetKeyType()
        => KeyType switch
        {
            KeyType.Hash => Amazon.DynamoDBv2.KeyType.HASH,
            KeyType.Range => Amazon.DynamoDBv2.KeyType.RANGE,
            _ => throw new ArgumentOutOfRangeException($"Invalid KeyType {KeyType} for Dynamo denifition.")
        };
    
    internal ScalarAttributeType GetScalarType()
        => PropertyType switch
        {
            PropertyType.String => ScalarAttributeType.S,
            PropertyType.Numeric => ScalarAttributeType.N,
            _ => throw new ArgumentOutOfRangeException($"Invalid PropertyType {PropertyType} for Dynamo denifition.")
        };
}