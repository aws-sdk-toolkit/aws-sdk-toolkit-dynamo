# aws-sdk-toolkit-dynamo
Responsible for facilitating integration with the AWS Dynamo service.

To use the library you will need to initialize it by following the steps below:

* In your application initialization file, the Dynamo access configuration must be referenced:

```csharp
builder.Services
    .AddAwsDefaultSettings(builder.Configuration, builder.Environment)
    .AddDynamoDb(builder.Configuration, builder.Environment)
```

* To configure the tables, you must complement the previous step with the other instructions:

```csharp
builder.Services
    .AddAwsDefaultSettings(builder.Configuration, builder.Environment)
    .AddDynamoDb(builder.Configuration, builder.Environment)
    .RegisterTables(new[]
    {
        new TablesMapper
        {
            TableName = "TABLE_NAME_DEFINED",
            Schema = new []
            {
                new SchemaTable
                {
                    Name = "HASH_KEY_DEFINED",
                    KeyType = KeyType.Hash,
                    PropertyType = PropertyType.String
                },
                new SchemaTable
                {
                    Name = "RANGE_KEY_DEFINED,
                    KeyType = KeyType.Range,
                    PropertyType = PropertyType.String
                }
            }
        }
    });
```

If more than one table is needed, simply add several **TablesMapper** to the structure above.

* If, when starting the application, you want to create tables/indexes, simply add the instruction to the code:

```csharp
await app.Services.CreateTables(CancellationToken.None);
```

* The SDK contains a facilitator to operate locally, or even run the application locally and using the dynamo resource on AWS. To do this, simply add the configuration below in the **appsettings.[environment].json** file.

```json5
"Aws": {
    "Dynamo": {
        "AccessKey": "local",
        "SecretKey": "local",
        "Token": "",
        "ServiceURL": "http://dynamodb-local:8000",
        "Region": "UsEast1"
    }
}
```
