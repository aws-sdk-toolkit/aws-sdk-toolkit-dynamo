using System.Diagnostics.CodeAnalysis;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AwsTool.Sdk.Dynamo.Models.Settings;
using AwsTool.Sdk.Dynamo.Models.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AwsTool.Sdk.Dynamo.Extensions;

[ExcludeFromCodeCoverage]
public static class DynamoDbExtensions
{
    /// <summary>
    /// Add basic dynamo resource access settings.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    /// <param name="environment">Provides information about the hosting environment an application is running in.</param>
    /// <returns>Specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection AddDynamoDb(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            var client = GetClient(configuration);
            services.AddSingleton<IAmazonDynamoDB>(client);
        }
        else
        {
            services.AddAWSService<IAmazonDynamoDB>();
        }

        services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="tables">Dynamo tables that will be used.</param>
    /// <returns>Specifies the contract for a collection of service descriptors.</returns>
    public static IServiceCollection RegisterTables(this IServiceCollection services, IEnumerable<TablesMapper> tables)
    {
        services.AddSingleton(tables);
        services.AddSingleton<IInitializerTables, InitializerTables>();
        return services;
    }
    
    private static AmazonDynamoDBClient GetClient(IConfiguration configuration)
    {
        var dynamoSettings = new DynamoDbSettings();
        configuration.GetSection("Aws").GetSection("Dynamo").Bind(dynamoSettings);

        if (dynamoSettings == null)
            throw new InvalidOperationException("Aws.Dynamo not defined in configuration.");
        
        return dynamoSettings.HasTokenDefined
            ? new AmazonDynamoDBClient(dynamoSettings.AccessKey, dynamoSettings.SecretKey, dynamoSettings.GetRegionAws())
            : new AmazonDynamoDBClient(dynamoSettings.AccessKey, dynamoSettings.SecretKey, new AmazonDynamoDBConfig { ServiceURL = dynamoSettings.ServiceURL});
    }
}