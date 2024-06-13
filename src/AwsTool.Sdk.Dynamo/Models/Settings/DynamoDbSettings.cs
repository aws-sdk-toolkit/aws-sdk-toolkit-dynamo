using System.ComponentModel;
using Amazon;

namespace AwsTool.Sdk.Dynamo.Models.Settings;

/// <summary>
/// Represents the settings used to connect to the dynamo.
/// </summary>
public class DynamoDbSettings
{
    /// <summary>
    /// Access key.
    /// </summary>
    public string AccessKey { get; set; }
    /// <summary>
    /// Secret key.
    /// </summary>
    public string SecretKey { get; set; }
    /// <summary>
    /// URL to access a local dynamo table.
    /// </summary>
    public string ServiceURL { get; set; }
    /// <summary>
    /// Active token of the section (only if connecting to a Dynamo instance on AWS).
    /// </summary>
    public string Token { get; set; }
    /// <summary>
    /// Region where the dynamo resource is provisioned. 
    /// </summary>
    public AwsRegionType? Region { get; set; }

    internal bool HasTokenDefined => !string.IsNullOrEmpty(Token);
    
    internal RegionEndpoint GetRegionAws()
        => Region switch
        {
            AwsRegionType.UsEast1 => RegionEndpoint.USEast1,
            AwsRegionType.UsEast2 => RegionEndpoint.USEast2,
            AwsRegionType.UsWest1 => RegionEndpoint.USWest1,
            AwsRegionType.UsWest2 => RegionEndpoint.USWest2,

            AwsRegionType.SaEast1 => RegionEndpoint.SAEast1,

            AwsRegionType.ApNortheast1 => RegionEndpoint.APNortheast1,
            AwsRegionType.ApSoutheast1 => RegionEndpoint.APSoutheast1,

            AwsRegionType.EuWest1 => RegionEndpoint.EUWest1,
            AwsRegionType.EuCentral1 => RegionEndpoint.EUCentral1,

            _ => throw new InvalidEnumArgumentException("Aws.Dynamo.Region not defined in configuration.")
        };
}