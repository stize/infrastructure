using Pulumi;
using Pulumi.AzureNative.Network.Inputs;

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
            builder.Arguments.VirtualNetworkName = name;
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

        /// <summary>
        /// A DDoS protection plan is a paid service that offers enhanced DDoS mitigation capabilities via adaptive tuning, attack notification, 
        /// and telemetry to protect against the impacts of a DDoS attack for all protected resources within this virtual network. 
        /// Basic DDoS protection is integrated into the Azure platform by default and at no additional cost.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="resourceId">Resource ID of the DDoS Protection Plan</param>
        /// <returns></returns>
        public static VNetBuilder DdosProtectionPlan(this VNetBuilder builder, Input<string> resourceId)
        {
            builder.Arguments.DdosProtectionPlan = new SubResourceArgs { Id = resourceId };
            builder.Arguments.EnableDdosProtection = true;
            return builder;
        }

        public static VNetBuilder EnableVmProtection(this VNetBuilder builder, Input<bool> vmProtection)
        {
            builder.Arguments.EnableVmProtection = vmProtection;
            return builder;
        }
    }
}
