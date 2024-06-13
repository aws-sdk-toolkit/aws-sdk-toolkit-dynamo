using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AwsTool.Sdk.Dynamo.Models.Paginations;

namespace AwsTool.Sdk.Dynamo;

/// <summary>
/// Base repository to perform query and writing operations in Dynamo.
/// </summary>
/// <param name="dynamoDbContext">Connection context with dynamo.</param>
/// <typeparam name="T">Type of object to be stored.</typeparam>
public abstract class RepositoryBase<T> (IDynamoDBContext dynamoDbContext)
    where T : class, new()
{
    /// <summary>
    /// Create or update a record in dynamo.
    /// </summary>
    /// <param name="item">Record to be stored.</param>
    /// <param name="cancellationToken">Token which can be used to cancel the task.</param>
    /// <typeparam name="T">Type of object to be stored.</typeparam>
    protected async Task SaveAsync<T>(T item, CancellationToken cancellationToken)
    {
        var batchWrite = dynamoDbContext.CreateBatchWrite<T>();
        batchWrite.AddPutItem(item);
        await batchWrite.ExecuteAsync(cancellationToken);
    }
    
    /// <summary>
    /// Create or update a records in dynamo.
    /// </summary>
    /// <param name="itens">Record to be stored.</param>
    /// <param name="cancellationToken">Token which can be used to cancel the task.</param>
    /// <typeparam name="T">Type of object to be stored.</typeparam>
    protected async Task SaveAsync<T>(IEnumerable<T> itens, CancellationToken cancellationToken)
    {
        var batchWrite = dynamoDbContext.CreateBatchWrite<T>();
        batchWrite.AddPutItems(itens);
        await batchWrite.ExecuteAsync(cancellationToken);
    }
    
    /// <summary>
    /// Executes a query defined in Dynamo.
    /// </summary>
    /// <param name="query">Query to be executed.</param>
    /// <param name="pageFilter">Configuration for pagination control.</param>
    /// <param name="cancellationToken">Token which can be used to cancel the task.</param>
    /// <returns>Query result with the identifier of the next page.</returns>
    protected async Task<PageResult<T>> GetAsync(QueryFilter query, PageFilter pageFilter, CancellationToken cancellationToken)
    {
        var logTable = dynamoDbContext.GetTargetTable<T>();
        var search = logTable.Query(new QueryOperationConfig
        {
            Filter = query,
            Limit = pageFilter.Limit,
            PaginationToken = pageFilter.GetPage()
        });
        
        var documents = await search.GetNextSetAsync(cancellationToken);
        var result = dynamoDbContext.FromDocuments<T>(documents);
        
        return new PageResult<T>(result, search.PaginationToken);
    }
}