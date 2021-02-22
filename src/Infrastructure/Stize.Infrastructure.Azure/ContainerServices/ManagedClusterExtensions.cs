using System;
using Pulumi;
using Pulumi.AzureNextGen.ContainerService.Latest;

namespace Stize.Infrastructure.Azure.ContainerServices
{
    public static class ManagedClusterExtensions
    {
        /// <summary>
        /// Sets the resource group the <see cref="Pulumi.Azure.Network.VirtualNetwork" will be created on/>
        /// </summary>
        /// <param name="builder">ManagedCluster Builder</param>
        /// <param name="resourceGroup">Resource group name</param>
        /// <returns></returns>
        public static ManagedClusterBuilder ResourceGroup(this ManagedClusterBuilder builder, Input<string> resourceGroup)
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
        public static ManagedClusterBuilder Name(this ManagedClusterBuilder builder, Input<string> name)
        {
            if (builder.RandomId != null)
            {
                builder.Arguments.ResourceName = builder.RandomId.Hex.Apply(r => $"{name}-{r}");
            }
            else
            {
                builder.Arguments.ResourceName = name;
            }

            return builder;
        }

        /// <summary>
        /// Sets the location on which the resource should be created on
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="location">Resource location</param>
        /// <returns></returns>
        public static ManagedClusterBuilder Location(this ManagedClusterBuilder builder, Input<string> location)
        {
            builder.Arguments.Location = location;
            return builder;
        }
    }
}
