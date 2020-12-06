using System;
using Pulumi;

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
        /// <param name="addressSpace">VNet address space</param>
        /// <returns></returns>
        public static VNetBuilder AddressSpace(this VNetBuilder builder, Input<string> addressSpace)
        {
            builder.Arguments.AddressSpaces = addressSpace;
            return builder;
        }
    }
}
