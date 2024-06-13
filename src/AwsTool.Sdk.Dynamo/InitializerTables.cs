using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using AwsTool.Sdk.Dynamo.Models.Tables;

namespace AwsTool.Sdk.Dynamo;

internal class InitializerTables : IInitializerTables
{
    private readonly IAmazonDynamoDB _dynamoDb;
    private readonly IEnumerable<TablesMapper> _tables;

    public InitializerTables(IAmazonDynamoDB dynamoDb, IEnumerable<TablesMapper> tables)
    {
        _dynamoDb = dynamoDb;
        _tables = tables;
    }
    
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        var tables = await _dynamoDb.ListTablesAsync(cancellationToken);
        
        foreach (var table in _tables)
        {
            if (tables.TableNames.Contains(table.TableName))
                continue;

            var request = new CreateTableRequest
            {
                TableName = table.TableName,
                AttributeDefinitions = table.Schema
                    .Select(s => new AttributeDefinition(s.Name, s.GetScalarType()))
                    .ToList(),
                KeySchema = table.Schema
                    .Select(s => new KeySchemaElement(s.Name, s.GetKeyType()))
                    .ToList(),
                BillingMode = BillingMode.PAY_PER_REQUEST
            };
            
            await _dynamoDb.CreateTableAsync(request, cancellationToken);
        }
    }
}

internal interface IInitializerTables
{
    Task InitializeAsync(CancellationToken cancellationToken);
}
