using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace AwsTool.Sdk.Dynamo.Extensions;

[ExcludeFromCodeCoverage]
public static class TablesExtensions
{
    /// <summary>
    /// Executes the creation of pre-configured tables and indexes in dynamo.
    /// </summary>
    /// <param name="serviceProvider">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="cancellationToken">Token which can be used to cancel the task.</param>
    public static async Task CreateTables(this IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        var initializerTable = serviceProvider.GetService<IInitializerTables>();
        await initializerTable?.InitializeAsync(cancellationToken)!;
    }
}