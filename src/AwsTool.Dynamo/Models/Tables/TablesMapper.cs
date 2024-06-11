namespace AwsTool.Dynamo.Models.Tables;

public class TablesMapper
{
    public string TableName { get; set; }

    public IEnumerable<SchemaTable> Schema { get; set; }
}