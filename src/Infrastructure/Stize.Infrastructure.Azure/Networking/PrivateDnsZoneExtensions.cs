using Pulumi;


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
        /// Create a private DNS zone with the name privatelink.blob.core.windows.net for blob private endpoints
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static PrivateDnsZoneBuilder CreateBlobStoragePrivateDnsZone(this PrivateDnsZoneBuilder builder)
        {
            return Name(builder, "privatelink.blob.core.windows.net");
        }

        /// <summary>
        /// Create a private DNS zone with the name privatelink.vaultcore.azure.net for key vault private endpoints
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static PrivateDnsZoneBuilder CreateKeyVaultPrivateDnsZone(this PrivateDnsZoneBuilder builder)
        {          
            return Name(builder, "privatelink.vaultcore.azure.net");
        }

        /// <summary>
        /// Set the resource group of this private DNS zone
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="resourceGroup"></param>
        /// <returns></returns>
        public static PrivateDnsZoneBuilder In(this PrivateDnsZoneBuilder builder, Input<string> resourceGroup)
        {
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }
    }
}
