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
        /// Set the Subnet for the <see cref="NetworkInterfaceIPConfigurationArgs"/>
        /// </summary>
        /// <param name="builder">NI Configurations Args</param>
        /// <param name="subnetIp">subnet name</param>
        /// <returns></returns>
        //public static NetworkInterfaceBuilder IpConfigSubnetIp(this NetworkInterfaceBuilder builder, Input<string> subnetIp)
        //{
        //    builder.IpConfigArgs.PrivateIPAddress = subnetIp;
        //    builder.Arguments.IpConfigurations.Add(builder.IpConfigArgs);
        //    return builder;
        //}
        //public static NetworkInterfaceBuilder IpConfigIpAllocation(this NetworkInterfaceBuilder builder, Input<SubnetArgs> subnet)
        //{
        //    builder.IpConfigArgs.Subnet = subnet;
        //    builder.Arguments.IpConfigurations.Add(builder.IpConfigArgs);
        //    return builder;
        //}
        /// <summary>
        /// Sets the resource group the <see cref="Pulumi.Azure.Network.NetworkInterface" /> will be created on
        /// </summary>
        /// <param name="builder">NetworkInterface Builder</param>
        /// <param name="resourceGroup">Resource group name</param>
        /// <returns></returns>
        public static NetworkInterfaceBuilder NetworkSecurityGroup(this NetworkInterfaceBuilder builder, Input<Inputs.NetworkSecurityGroupArgs> nsg)
        {
            builder.Arguments.NetworkSecurityGroup = nsg;
            return builder;
        }
        /// <summary>
        /// Sets the resource group the <see cref="Pulumi.Azure.Network.NetworkInterface" /> will be created on
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
