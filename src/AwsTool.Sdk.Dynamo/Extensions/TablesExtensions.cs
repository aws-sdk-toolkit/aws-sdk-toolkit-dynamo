using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace AwsTool.Sdk.Dynamo.Extensions;

[ExcludeFromCodeCoverage]
public static class TablesExtensions
{
    public static async Task CreateTables(this IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        var initializerTable = serviceProvider.GetService<IInitializerTables>();
        await initializerTable?.InitializeAsync(cancellationToken)!;
    }
}