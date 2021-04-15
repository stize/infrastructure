using Pulumi;
using Pulumi.AzureNative.Network;
using Pulumi.AzureNative.Network.Inputs;
using Inputs = Pulumi.AzureNative.Network.Inputs;

namespace Stize.Infrastructure.Azure.Networking
{
    public static class PrivateEndpointExtensions
    {
        /// <summary>
        /// Set the name of the private endpoint
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PrivateEndpointBuilder Name(this PrivateEndpointBuilder builder, Input<string> name)
        {
            builder.Arguments.PrivateEndpointName = name;
            return builder;
        }

        /// <summary>
        /// Set the location of the private endpoint
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static PrivateEndpointBuilder Location(this PrivateEndpointBuilder builder, Input<string> location)
        {
            builder.Arguments.Location = location;
            return builder;
        }

        /// <summary>
        /// Set the resource group of the private endpoint 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="resourceGroup"></param>
        /// <returns></returns>
        public static PrivateEndpointBuilder In(this PrivateEndpointBuilder builder, Input<string> resourceGroup)
        {
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }

        /// <summary>
        /// Set the subnet used for the private endpoint
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="subnet"></param>
        /// <returns></returns>
        public static PrivateEndpointBuilder InSubnet(this PrivateEndpointBuilder builder, Input<Subnet> subnet)
        {
            builder.Arguments.Subnet = new Inputs.SubnetArgs { Id = subnet.Apply(s => s.Id) };
            return builder;
        }

        /// <summary>
        /// Connects a resource to the private endpoint. Requires manual approval from the remote resource owner.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="resource">The resource that connects to the private endpoint</param>
        /// <param name="connectionName">The name of the connection between the resource and the private endpoint</param>
        /// <param name="groupIds">A list of group IDs which the private endpoint is able to connect to</param>
        /// <param name="requestMessage">A request message to send to the connection approver</param>
        /// <returns></returns>
        public static PrivateEndpointBuilder ForResource(this PrivateEndpointBuilder builder, Input<CustomResource> resource, 
            Input<string> connectionName, InputList<string> groupIds, Input<string> requestMessage)
        {
            builder.Arguments.ManualPrivateLinkServiceConnections.Add(new PrivateLinkServiceConnectionArgs()
            {
                Name = connectionName,
                PrivateLinkServiceId = resource.Apply(r => r.Id),
                GroupIds = groupIds,
                RequestMessage = requestMessage
            });
            return builder;
        }

        /// <summary>
        /// Connects a resource to the private endpoint. Connection is auto-approved.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="resource">The resource that connects to the private endpoint</param>
        /// <param name="connectionName">The name of the connection between the resource and the private endpoint</param>
        /// <param name="groupIds">A list of group IDs which the private endpoint uses</param>
        /// <returns></returns>
        public static PrivateEndpointBuilder ForResource(this PrivateEndpointBuilder builder, Input<CustomResource> resource,
            Input<string> connectionName, InputList<string> groupIds)
        {
            builder.Arguments.PrivateLinkServiceConnections.Add(new Inputs.PrivateLinkServiceConnectionArgs()
            {
                Name = connectionName,
                PrivateLinkServiceId = resource.Apply(r => r.Id),
                GroupIds = groupIds
            });
            return builder;
        }

        /// <summary>
        /// Set the configuration for the A record of the private endpoint 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="recordName">The name of the DNS record</param>
        /// <param name="timeToLive">Time to live in seconds</param>
        /// <returns></returns>
        public static PrivateEndpointBuilder DnsRecord(this PrivateEndpointBuilder builder, Input<string> recordName,
            Input<double> timeToLive)
        {
            //builder.RecordBuilder.Name = recordName.Apply(n => n).GetValueAsync().Result;
            builder.RecordBuilder.Name(recordName);
            builder.RecordBuilder.TimeToLive(timeToLive);
            return builder;
        }

        /// <summary>
        /// Set the Private DNS Zone that will contain the A record of the Private Endpoint
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="privateDnsZone">The name of the Private DNS Zone that will contain the DNS record</param>
        /// <param name="resourceGroup">The resource group of the Private DNS Zone that will contain the DNS record</param>
        /// <returns></returns>
        public static PrivateEndpointBuilder PrivateDnsZone(this PrivateEndpointBuilder builder, 
            Input<string> privateDnsZone, Input<string> resourceGroup)
        {
            builder.RecordBuilder.In(resourceGroup);
            builder.RecordBuilder.InPrivateDnsZone(privateDnsZone);
            return builder;
        }
    }
}
