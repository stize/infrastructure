using Pulumi;
using Pulumi.AzureNative.Network;
using Inputs = Pulumi.AzureNative.Network.Inputs;

namespace Stize.Infrastructure.Azure.Networking
{
    public static class PrivateDnsZoneExtensions
    {
        /// <summary>
        /// Set the name of the private DNS zone
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PrivateDnsZoneBuilder Name(this PrivateDnsZoneBuilder builder, Input<string> name)
        {
            builder.Arguments.PrivateZoneName = name;
            return builder;
        }

        /// <summary>
        /// Set the location of the private DNS zone
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PrivateDnsZoneBuilder Location(this PrivateDnsZoneBuilder builder, Input<string> location)
        {
            builder.Arguments.Location = location;
            return builder;
        }

        /// <summary>
        /// Create a private DNS zone with the name privatelink.blob.core.windows.net for blob storage private endpoints
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static PrivateDnsZoneBuilder ForBlobStorage(this PrivateDnsZoneBuilder builder)
        {
            return Name(builder, "privatelink.blob.core.windows.net");
        }

        /// <summary>
        /// Create a private DNS zone with the name privatelink.file.core.windows.net for file storage private endpoints
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static PrivateDnsZoneBuilder ForFileStorage(this PrivateDnsZoneBuilder builder)
        {
            return Name(builder, "privatelink.file.core.windows.net");
        }

        /// <summary>
        /// Create a private DNS zone with the name privatelink.table.core.windows.net for table storage private endpoints
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static PrivateDnsZoneBuilder ForTableStorage(this PrivateDnsZoneBuilder builder)
        {
            return Name(builder, "privatelink.table.core.windows.net");
        }

        /// <summary>
        /// Create a private DNS zone with the name privatelink.queue.core.windows.net for queue storage private endpoints
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static PrivateDnsZoneBuilder ForQueueStorage(this PrivateDnsZoneBuilder builder)
        {
            return Name(builder, "privatelink.queue.core.windows.net");
        }

        /// <summary>
        /// Create a private DNS zone with the name privatelink.vaultcore.azure.net for key vault private endpoints
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static PrivateDnsZoneBuilder ForKeyVault(this PrivateDnsZoneBuilder builder)
        {          
            return Name(builder, "privatelink.vaultcore.azure.net");
        }

        /// <summary>
        /// Set the resource group of the private DNS zone
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="resourceGroup"></param>
        /// <returns></returns>
        public static PrivateDnsZoneBuilder In(this PrivateDnsZoneBuilder builder, Input<string> resourceGroup)
        {
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }

        /// <summary>
        /// Link a virtual network to the private DNS zone
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="vnet">Virtual network to be linked</param>
        /// <param name="linkName">Name of virtual network link</param>
        /// <param name="enableAutoRegistration">Is auto-registration of virtual machine records in the virtual network in the Private DNS zone enabled?</param>
        /// <returns></returns>
        public static PrivateDnsZoneBuilder LinkTo(this PrivateDnsZoneBuilder builder, Input<VirtualNetwork> vnet, Input<string> linkName, Input<bool> enableAutoRegistration)
        {
            builder.VnetLinks.Add(new VirtualNetworkLinkArgs()
            {
                VirtualNetworkLinkName = linkName,
                Location = builder.Arguments.Location,
                PrivateZoneName = builder.Arguments.PrivateZoneName,
                ResourceGroupName = builder.Arguments.ResourceGroupName,
                Tags = builder.Arguments.Tags,
                RegistrationEnabled = enableAutoRegistration,
                VirtualNetwork = new Inputs.SubResourceArgs { Id = vnet.Apply(v => v.Id) }

            });
            return builder;
        }
    }
}
