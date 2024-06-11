using Amazon.DynamoDBv2;

namespace AwsTool.Dynamo.Models.Tables;

public class SchemaTable
{
    public string Name { get; set; }
    public KeyType KeyType { get; set; }
    public PropertyType PropertyType { get; set; }
    
    public Amazon.DynamoDBv2.KeyType GetKeyType()
        => KeyType switch
        {
            KeyType.Hash => Amazon.DynamoDBv2.KeyType.HASH,
            KeyType.Range => Amazon.DynamoDBv2.KeyType.RANGE,
            _ => throw new ArgumentOutOfRangeException($"Invalid KeyType {KeyType} for Dynamo denifition.")
        };
    
    public ScalarAttributeType GetScalarType()
        => PropertyType switch
        {
            PropertyType.String => ScalarAttributeType.S,
            PropertyType.Numeric => ScalarAttributeType.N,
            _ => throw new ArgumentOutOfRangeException($"Invalid PropertyType {PropertyType} for Dynamo denifition.")
        };
}