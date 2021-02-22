using Pulumi;
using Pulumi.AzureNextGen.Network.Latest.Inputs;

namespace Stize.Infrastructure.Azure.Networking
{
    public static class VNetExtensions
    {
        /// <summary>
        /// Sets the resource group the <see cref="Pulumi.Azure.Network.VirtualNetwork" will be created on/>
        /// </summary>
        /// <param name="builder">VNET Builder</param>
        /// <param name="resourceGroup">Resource group name</param>
        /// <returns></returns>
        public static VNetBuilder ResourceGroup(this VNetBuilder builder, Input<string> resourceGroup)
        {            
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }

        /// <summary>
        /// Virtual network address space
        /// </summary>
        /// <param name="builder">VNet Builder</param>
        /// <param name="addressPrefixes">VNet address space</param>
        /// <returns></returns>
        public static VNetBuilder AddressSpace(this VNetBuilder builder, InputList<string> addressPrefixes)
        {
            builder.Arguments.AddressSpace = new AddressSpaceArgs
            {
                AddressPrefixes = addressPrefixes
            };

            return builder;
        }


        /// <summary>
        /// Sets the builder name. If the builder has an RandomId associated, 
        /// appends the hex value of the RandomId to the end of the name
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="name">Builder name</param>
        /// <returns>The builder argument</returns>
        public static VNetBuilder Name(this VNetBuilder builder, Input<string> name)
        {
            if (builder.RandomId != null)
            {
                builder.Arguments.VirtualNetworkName = builder.RandomId.Hex.Apply(r => $"{name}-{r}");
            }
            else
            {
                builder.Arguments.VirtualNetworkName = name;
            }

            return builder;
        }

        /// <summary>
        /// Sets the location on which the resource should be created on
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="location">Resource location</param>
        /// <returns></returns>
        public static VNetBuilder Location(this VNetBuilder builder, Input<string> location)
        {
            builder.Arguments.Location = location;
            return builder;
        }        
    }
}
