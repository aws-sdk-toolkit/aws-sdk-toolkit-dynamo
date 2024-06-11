using System.ComponentModel;
using Amazon;

namespace AwsTool.Dynamo.Models.Settings;

public class DynamoDbSettings
{
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string Token { get; set; }
    public string ServiceURL { get; set; }
    public AwsRegionType? Region { get; set; }

    public bool HasTokenDefined => !string.IsNullOrEmpty(Token);
    
    public RegionEndpoint GetRegionAws()
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