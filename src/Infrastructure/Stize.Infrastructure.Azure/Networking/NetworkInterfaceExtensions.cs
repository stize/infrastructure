using Pulumi;
using Pulumi.AzureNative.Network;
using Inputs = Pulumi.AzureNative.Network.Inputs;

namespace Stize.Infrastructure.Azure.Networking
{
    public static class NetworkInterfaceExtensions
    {

        /// <summary>
        /// The extended location of the NIC
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="extendedLocationArgs"></param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder ExtendedLocation(this NetworkInterfaceBuilder builder, Input<Inputs.ExtendedLocationArgs> extendedLocationArgs)
        {
            builder.Arguments.ExtendedLocation = extendedLocationArgs;
            return builder;
        }
        /// <summary>
        /// Enable or disable IP Forwarding for the NIC
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="ipForwarding"></param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder EnableIPForwarding(this NetworkInterfaceBuilder builder, Input<bool> ipForwarding)
        {
            builder.Arguments.EnableIPForwarding = ipForwarding;
            return builder;
        }
        /// <summary>
        /// Enable or disable Accelerated Networking for the NIC
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="accleratedNetworking"></param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder EnableAcceleratedNetworking(this NetworkInterfaceBuilder builder, Input<bool> accleratedNetworking)
        {
            builder.Arguments.EnableAcceleratedNetworking = accleratedNetworking;
            return builder;
        }
        /// <summary>
        /// Sets the DNS Settings of the NIC
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dnsSettings"></param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder DnsSettings(this NetworkInterfaceBuilder builder, Input<Inputs.NetworkInterfaceDnsSettingsArgs> dnsSettings)
        {
            builder.Arguments.DnsSettings = dnsSettings;
            return builder;
        }

        /// <summary>
        /// Sets the name of this Ip Configuration for this NIC
        /// </summary>
        /// <param name="builder">NI builder</param>
        /// <param name="name">Ip Config name</param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder IpConfigName(this NetworkInterfaceBuilder builder, Input<string> name)
        {
            builder.IpConfigArgs.Name = name;
            return builder;
        }
        /// <summary>
        /// Sets the Subnet for the <see cref="Inputs.NetworkInterfaceIPConfigurationArgs"/> using the subnet's ID
        /// </summary>
        /// <param name="builder">NI builder</param>
        /// <param name="subnetId">Subnet Id</param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder IpConfigSubnetID(this NetworkInterfaceBuilder builder, Input<string> subnetId)
        {
            builder.IpConfigArgs.Subnet = new Inputs.SubnetArgs { Id = subnetId }; // TODO: Check if this successfully associates the NIC with the subnet specified.
            return builder;
        }
        /// <summary>
        /// Sets the private IP address version for the IP config for this NIC
        /// </summary>
        /// <param name="builder">NI builder</param>
        /// <param name="version">Address Version; i.e. 'IPv4', 'IPv6'</param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder IpConfigAddressVersion(this NetworkInterfaceBuilder builder, InputUnion<string, IPVersion> version)
        {
            builder.IpConfigArgs.PrivateIPAddressVersion = version;
            return builder;
        }
        /// <summary>
        /// Sets the private IP allocation method for the IP config of this NIC
        /// </summary>
        /// <param name="builder">NI builder</param>
        /// <param name="method">IP allocation method; i.e. 'Dynamic', 'Static'</param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder IpConfigAllocationMethod(this NetworkInterfaceBuilder builder, InputUnion<string, IPAllocationMethod> method)
        {
            builder.IpConfigArgs.PrivateIPAllocationMethod = method;
            return builder;
        }
        /// <summary>
        /// Sets the resource group the <see cref="NetworkInterface" /> will be created on
        /// </summary>
        /// <param name="builder">NetworkInterface Builder</param>
        /// <param name="nsgId">Network Security Group Id</param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder NetworkSecurityGroup(this NetworkInterfaceBuilder builder, Input<string> nsgId)
        {
            builder.Arguments.NetworkSecurityGroup = new Inputs.NetworkSecurityGroupArgs { Id = nsgId };
            return builder;
        }
        /// <summary>
        /// Sets the resource group the <see cref="NetworkInterface" /> will be created on
        /// </summary>
        /// <param name="builder">NetworkInterface Builder</param>
        /// <param name="resourceGroup">Resource group name</param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder ResourceGroup(this NetworkInterfaceBuilder builder, Input<string> resourceGroup)
        {
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }


        /// <summary>
        /// Sets the builder name. If the builder has an RandomId associated, 
        /// appends the hex value of the RandomId to the end of the name
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="name">Builder name</param>
        /// <returns>The builder argument</returns>
        public static NetworkInterfaceBuilder Name(this NetworkInterfaceBuilder builder, Input<string> name)
        {
            builder.Arguments.NetworkInterfaceName = name;
            return builder;
        }

        /// <summary>
        /// Sets the location on which the resource should be created on
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="location">Resource location</param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder Location(this NetworkInterfaceBuilder builder, Input<string> location)
        {
            builder.Arguments.Location = location;
            return builder;
        }

        /// <summary>
        /// Sets the tags for the resource
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="tags">Resource tags</param>
        /// <returns></returns
        public static NetworkInterfaceBuilder Tags(this NetworkInterfaceBuilder builder, InputMap<string> tags)
        {
            builder.Arguments.Tags = tags;
            return builder;
        }
    }
}
