using Pulumi;
using Pulumi.AzureNative.Network;
using Inputs = Pulumi.AzureNative.Network.Inputs;

namespace Stize.Infrastructure.Azure.Networking
{
    public static class NetworkInterfaceExtensions
    {

        /// <summary>
        /// The extended location (also known as 'Edge Zone') for the NIC
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="extLocation">Extended Location (also known as 'Edge Zone').</param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder ExtendedLocation(this NetworkInterfaceBuilder builder, Input<string> extLocation)
        {
            builder.Arguments.ExtendedLocation = new Inputs.ExtendedLocationArgs
            {
                Name = extLocation,
                Type = ExtendedLocationTypes.EdgeZone
            };
            return builder;
        }
        /// <summary>
        /// Enable or disable IP Forwarding for the NIC
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="ipForwarding">Set to ture for enabling IP forwarding.</param>
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
        /// <param name="accleratedNetworking">Set to true for enabling accelerated networking.</param>
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
        /// <param name="internalDnsNameLabel">Relative DNS name for this NIC used for internal communications between VMs in the same virtual network.</param>
        /// <param name="dnsServerIpAddresses">List of DNS servers IP addresses. Use ‘AzureProvidedDNS’ to switch to azure provided DNS resolution. ‘AzureProvidedDNS’ value cannot be combined with other IPs, it must be the only value in dnsServers collection.</param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder DnsSettings(this NetworkInterfaceBuilder builder, Input<string> internalDnsNameLabel, params Input<string>[] dnsServerIpAddresses)
        {
            builder.Arguments.DnsSettings = new Inputs.NetworkInterfaceDnsSettingsArgs
            {
                InternalDnsNameLabel = internalDnsNameLabel,
                DnsServers = dnsServerIpAddresses
            };
            return builder;
        }

        /// <summary>
        /// Add an IP configuration, with a dynamic IP address, to the Network Interface. 
        /// Must specify the IP Configuration name, Subnet Resource ID, and a true/false for whether the IP configuration is primary.
        /// The IP Version can be specified, but the default value is 'IPv4'.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="ipConfigName">IP Configuration name</param>
        /// <param name="subnetId">Subnet Resource ID</param>
        /// <param name="primaryIP">State true or false to declare whether the IP configuration is primary, or not.</param>
        /// <param name="ipVersion">IP Version. Valid values are 'IPv4' and 'IPv6'.</param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder AddDynamicIPConfiguration(this NetworkInterfaceBuilder builder, Input<string> ipConfigName, 
            Input<string> subnetId, Input<bool> primaryIP, InputUnion<string, IPVersion> ipVersion = null)
        {
            builder.IpConfigArgs.Add(new Inputs.NetworkInterfaceIPConfigurationArgs
            {
                Name = ipConfigName,
                Subnet = new Inputs.SubnetArgs { Id = subnetId },
                PrivateIPAddressVersion = ipVersion ?? IPVersion.IPv4,
                PrivateIPAllocationMethod = IPAllocationMethod.Dynamic,
                Primary = primaryIP

            });
            return builder;
        }


        /// <summary>
        /// Add an IP configuration, with a static IP address, to the Network Interface. 
        /// Must specify the IP Configuration name, Subnet Resource ID, private static IP address, and a true/false for whether the IP configuration is primary.
        /// The IP Version can be specified, but the default value is 'IPv4'.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="ipConfigName">IP Configuration name</param>
        /// <param name="subnetId">Subnet Resource ID</param>
        /// <param name="privateIpAddress">Private static IP address</param>
        /// <param name="primaryIP">State true or false to declare whether the IP configuration is primary, or not.</param>
        /// <param name="ipVersion">IP Version. Valid values are 'IPv4' and 'IPv6'.</param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder AddStaticIPConfiguration(this NetworkInterfaceBuilder builder, Input<string> ipConfigName, Input<string> subnetId, 
            Input<string> privateIpAddress, Input<bool> primaryIP, InputUnion<string, IPVersion> ipVersion = null)
        {
            builder.IpConfigArgs.Add(new Inputs.NetworkInterfaceIPConfigurationArgs
            {
                Name = ipConfigName,
                Subnet = new Inputs.SubnetArgs { Id = subnetId },
                PrivateIPAddressVersion = ipVersion ?? IPVersion.IPv4,
                PrivateIPAllocationMethod = IPAllocationMethod.Static,
                PrivateIPAddress = privateIpAddress,
                Primary = primaryIP
            });
            return builder;
        }

        ///// <summary>
        ///// Set the name of the Ip Configuration.
        ///// </summary>
        ///// <param name="builder"></param>
        ///// <param name="name">Name of the Ip Configuration.</param>
        ///// <returns></returns>
        //public static NetworkInterfaceBuilder IPConfigName(this NetworkInterfaceBuilder builder, Input<string> name)
        //{
        //    builder.IpConfigArgs.Name = name;
        //    return builder;
        //}

        ///// <summary>
        ///// Assigns the type of IP allocation to 'Dynamic'.
        ///// </summary>
        ///// <param name="builder"></param>
        ///// <returns></returns>
        //public static NetworkInterfaceBuilder EnableDynamicIPAllocation(this NetworkInterfaceBuilder builder)
        //{
        //    builder.IpConfigArgs.PrivateIPAllocationMethod = IPAllocationMethod.Dynamic;
        //    return builder;
        //}

        ///// <summary>
        ///// Assigns the type of IP allocation to 'Static' and expects IP address as an argument.
        ///// </summary>
        ///// <param name="builder"></param>
        ///// <param name="ip">IP address for the Network Interface.</param>
        ///// <returns></returns>
        //public static NetworkInterfaceBuilder EnableStaticIPAllocation(this NetworkInterfaceBuilder builder, Input<string> ip)
        //{
        //    builder.IpConfigArgs.PrivateIPAddress = ip;
        //    return builder;
        //}

        ///// <summary>
        ///// Sets the Subnet for the Network Interface using the subnet's resource ID.
        ///// </summary>
        ///// <param name="builder">NI builder</param>
        ///// <param name="subnetId">Subnet Id</param>
        ///// <returns></returns>
        //public static NetworkInterfaceBuilder Subnet(this NetworkInterfaceBuilder builder, Input<string> subnetId)
        //{
        //    builder.IpConfigArgs.Subnet = new Inputs.SubnetArgs { Id = subnetId }; // TODO: Check if this successfully associates the NIC with the subnet specified.
        //    return builder;
        //}
        ///// <summary>
        ///// Sets the private IP address version for the IP configuration for this NIC.
        ///// </summary>
        ///// <param name="builder">NI builder</param>
        ///// <param name="version">Address Version; i.e. 'IPv4', 'IPv6'</param>
        ///// <returns></returns>
        //public static NetworkInterfaceBuilder IpAddressVersion(this NetworkInterfaceBuilder builder, InputUnion<string, IPVersion> version)
        //{
        //    builder.IpConfigArgs.PrivateIPAddressVersion = version;
        //    return builder;
        //}

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
        /// <param name="name">Resource name</param>
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
