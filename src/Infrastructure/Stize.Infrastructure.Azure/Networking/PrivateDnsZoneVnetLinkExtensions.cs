using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Inputs = Pulumi.AzureNextGen.Network.Latest.Inputs;

namespace Stize.Infrastructure.Azure.Networking
{
    public static class PrivateDnsZoneVnetLinkExtensions
    {
        /// <summary>
        /// Set the name of the private DNS zone vnet link
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PrivateDnsZoneVnetLinkBuilder Name(this PrivateDnsZoneVnetLinkBuilder builder, Input<string> name)
        {
            builder.Arguments.VirtualNetworkLinkName = name;
            return builder;
        }

        /// <summary>
        /// Set the location of the private DNS zone vnet link
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static PrivateDnsZoneVnetLinkBuilder Location(this PrivateDnsZoneVnetLinkBuilder builder, Input<string> location)
        {
            builder.Arguments.Location = location;
            return builder;
        }

        /// <summary>
        /// Resource Group to locate this resource
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="resourceGroup"></param>
        /// <returns></returns>
        public static PrivateDnsZoneVnetLinkBuilder In(this PrivateDnsZoneVnetLinkBuilder builder, Input<string> resourceGroup)
        {
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }

        /// <summary>
        /// Set the vnet that is to be linked to the private DNS zone
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="vnet"></param>
        /// <returns></returns>
        public static PrivateDnsZoneVnetLinkBuilder LinkTo(this PrivateDnsZoneVnetLinkBuilder builder, Input<VirtualNetwork> vnet)
        {
            builder.Arguments.VirtualNetwork = new Inputs.SubResourceArgs { Id = vnet.Apply(v => v.Id) };
            return builder;
        }

        /// <summary>
        /// Set the private DNS zone that will receive the vnet link
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="privateZoneName"></param>
        /// <returns></returns>
        public static PrivateDnsZoneVnetLinkBuilder PrivateDnsZone(this PrivateDnsZoneVnetLinkBuilder builder, Input<string> privateZoneName)
        {
            builder.Arguments.PrivateZoneName = privateZoneName;
            return builder;
        }

        /// <summary>
        /// Enable auto registration of virtual machine records in the virtual network in the Private DNS zone
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static PrivateDnsZoneVnetLinkBuilder EnableAutoRegistration(this PrivateDnsZoneVnetLinkBuilder builder)
        {
            builder.Arguments.RegistrationEnabled = true;
            return builder;
        }

        /// <summary>
        /// Disable auto registration of virtual machine records in the virtual network in the Private DNS zone
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static PrivateDnsZoneVnetLinkBuilder DisableAutoRegistration(this PrivateDnsZoneVnetLinkBuilder builder)
        {
            builder.Arguments.RegistrationEnabled = false;
            return builder;
        }
    }
}
