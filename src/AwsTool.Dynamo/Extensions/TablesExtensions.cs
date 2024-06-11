using System.Diagnostics.CodeAnalysis;
using AwsTool.Dynamo.Models.Tables;
using Microsoft.Extensions.DependencyInjection;

namespace AwsTool.Dynamo.Extensions;

[ExcludeFromCodeCoverage]
public static class TablesExtensions
{
    public static async Task CreateTables(this IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        var initializerTable = serviceProvider.GetService<IInitializerTables>();
        await initializerTable?.InitializeAsync(cancellationToken)!;
    }
}