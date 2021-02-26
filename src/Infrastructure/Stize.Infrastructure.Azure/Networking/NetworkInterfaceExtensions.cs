using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Inputs = Pulumi.AzureNextGen.Network.Latest.Inputs;

namespace Stize.Infrastructure.Azure.Networking
{
    public static class NetworkInterfaceExtensions
    {
        /// <summary>
        /// Set the name for the <see cref="NetworkInterfaceIPConfigurationArgs"/>
        /// </summary>
        /// <param name="builder">NI Configurations Args</param>
        /// <param name="name">resource name</param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder IpConfigurations(this NetworkInterfaceBuilder builder, Input<string> name, Input<string> subnetName, 
            InputUnion<string,IPVersion> privateIpAddressVersion, InputUnion<string, IPAllocationMethod> privateIpAllocationMethod)
        {
            builder.Arguments.IpConfigurations = new Inputs.NetworkInterfaceIPConfigurationArgs
            {
                Name = name,
                PrivateIPAddressVersion = privateIpAddressVersion,
                PrivateIPAllocationMethod = privateIpAllocationMethod,
                Subnet = new Inputs.SubnetArgs
                {
                    Name = subnetName
                }                
            };
            return builder;
        }
        /// <summary>
        /// Sets the name of this Ip Configuration for this NI
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
        /// Sets the Subnet for the <see cref="Inputs.NetworkInterfaceIPConfigurationArgs"/> using the subnet's Name
        /// </summary>
        /// <param name="builder">NI builder</param>
        /// <param name="subnetId">Subnet Id</param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder IpConfigSubnetName(this NetworkInterfaceBuilder builder, Input<string> subnetId)
        {
            builder.IpConfigArgs.Subnet = new Inputs.SubnetArgs { Id = subnetId }; // TODO: Check if this successfully associates the NIC with the subnet specified.
            return builder;
        }
        /// <summary>
        /// Sets the private IP address version for the IP config for this NI
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
        /// Sets the private IP allocation method for the IP config of this NI
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
            if (builder.RandomId != null)
            {
                builder.Arguments.NetworkInterfaceName = builder.RandomId.Hex.Apply(r => $"{name}-{r}");
            }
            else
            {
                builder.Arguments.NetworkInterfaceName = name;
            }

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
    }
}
