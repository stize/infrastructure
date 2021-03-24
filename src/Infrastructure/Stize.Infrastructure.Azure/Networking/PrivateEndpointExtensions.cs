using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Inputs = Pulumi.AzureNextGen.Network.Latest.Inputs;

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
        /// Add a service connection defining the connectivity between the private endpoint and the resource that will use the private endpoint
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="serviceConnection"></param>
        /// <returns></returns>
        public static PrivateEndpointBuilder AddServiceConnection(this PrivateEndpointBuilder builder, Input<Inputs.PrivateLinkServiceConnectionArgs> serviceConnection)
        {
            builder.Arguments.PrivateLinkServiceConnections.Add(serviceConnection);
            return builder;
        }
    }
}
