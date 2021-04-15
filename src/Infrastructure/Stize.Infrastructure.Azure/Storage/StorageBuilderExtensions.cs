using Pulumi;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.Storage;
using Pulumi.AzureNative.Storage.Inputs;

namespace Stize.Infrastructure.Azure.Storage
{
    public static class StorageAccountExtensions
    {

        /// <summary>
        /// Sets the builder name. If the builder has an RandomId associated, 
        /// appends the hex value of the RandomId to the end of the name
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="name">Builder name</param>
        /// <returns>The builder argument</returns>
        public static StorageAccountBuilder Name(this StorageAccountBuilder builder, Input<string> name)
        {
            builder.Arguments.AccountName = name;
            return builder;
        }

        /// <summary>
        /// Sets the location on which the resource should be created on
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="location">Resource location</param>
        /// <returns></returns>
        public static StorageAccountBuilder Location(this StorageAccountBuilder builder, Input<string> location)
        {
            builder.Arguments.Location = location;
            return builder;
        }

        /// <summary>
        /// Resource Group to locate this storage account
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="resourceGroup"></param>
        /// <returns></returns>
        public static StorageAccountBuilder In(this StorageAccountBuilder builder, Input<string> resourceGroup)
        {        
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;    
        }

        /// <summary>
        /// Resource Group to locate this storage account
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="resourceGroup"></param>
        /// <returns></returns>
        public static StorageAccountBuilder In(this StorageAccountBuilder builder, ResourceGroup resourceGroup)
        {        
            builder.Arguments.ResourceGroupName = resourceGroup.Name;
            return builder;
        }        

        /// <summary>
        /// Sets SKU to Standard_LRS
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder StandardLRS(this StorageAccountBuilder builder)
        {
            return Sku(builder, SkuName.Standard_LRS);
        }

        /// <summary>
        /// Sets SKU to Standard_GRS
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder StandardGRS(this StorageAccountBuilder builder)
        {
            return Sku(builder, SkuName.Standard_GRS);
        }

        /// <summary>
        /// Sets SKU to Premium_LRS
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder PremiumLRS(this StorageAccountBuilder builder)
        {
            return Sku(builder, SkuName.Premium_LRS);
        }

        /// <summary>
        /// Sets SKU to Premium_ZRS
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder PremiumZRS(this StorageAccountBuilder builder)
        {
            return Sku(builder, SkuName.Premium_ZRS);
        }

        /// <summary>
        /// Sets SKU to Standard_GZRS
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder StandardGZRS(this StorageAccountBuilder builder)
        {
            return Sku(builder, SkuName.Standard_GZRS);
        }

        /// <summary>
        /// Sets SKU to Standard_RAGRS
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder StandardRAGRS(this StorageAccountBuilder builder)
        {
            return Sku(builder, SkuName.Standard_RAGRS);
        }

        /// <summary>
        /// Sets SKU to Standard_RAGZRS
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder StandardRAGZRS(this StorageAccountBuilder builder)
        {
            return Sku(builder, SkuName.Standard_RAGZRS);
        }

        /// <summary>
        /// Sets SKU to Standard_ZRS
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder StandardZRS(this StorageAccountBuilder builder)
        {
            return Sku(builder, SkuName.Standard_ZRS);
        }

        /// <summary>
        /// Sets the SKU for this Storage Account
        /// </summary>
        /// <param name="builder">Storage Account Builder</param>
        /// <param name="sku">SKU</param>
        /// <returns></returns>
        public static StorageAccountBuilder Sku(this StorageAccountBuilder builder, InputUnion<string, SkuName> sku)
        {
            builder.Arguments.Sku = new SkuArgs
            {
                Name = sku,
            };

            return builder;
        }

        /// <summary>
        /// Allows public access to blobs
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder AllowBlobPublicAccess(this StorageAccountBuilder builder)
        {
            builder.Arguments.AllowBlobPublicAccess = true;
            return builder;
        }

        /// <summary>
        /// Denies public access to blobs
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder DenyBlobPublicAccess(this StorageAccountBuilder builder)
        {
            builder.Arguments.AllowBlobPublicAccess = false;
            return builder;
        }

        /// <summary>
        /// Sets the Storage kind to Storage
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder Storage(this StorageAccountBuilder builder)
        {
            return Kind(builder, Pulumi.AzureNative.Storage.Kind.Storage);
        }

        /// <summary>
        /// Sets the Storage kind to StorageV2
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder StorageV2(this StorageAccountBuilder builder)
        {
            return Kind(builder, Pulumi.AzureNative.Storage.Kind.StorageV2);
        }

        /// <summary>
        /// Sets the Storage kind to BlobStorage
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder Blob(this StorageAccountBuilder builder)
        {
            return Kind(builder, Pulumi.AzureNative.Storage.Kind.BlobStorage);
        }

        /// <summary>
        /// Sets the Storage kind to BlockBlobStorage
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder BlockBlobStorage(this StorageAccountBuilder builder)
        {
            return Kind(builder, Pulumi.AzureNative.Storage.Kind.BlockBlobStorage);
        }

        /// <summary>
        /// Sets the Storage kind to FileStorage
        /// </summary>
        /// <param name="builder"></param> 
        /// <returns></returns>
        public static StorageAccountBuilder FileStorage(this StorageAccountBuilder builder)
        {
            return Kind(builder, Pulumi.AzureNative.Storage.Kind.FileStorage);
        }        

        /// <summary>
        /// Sets the Storage kind
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static StorageAccountBuilder Kind(this StorageAccountBuilder builder, Kind kind)
        {
            builder.Arguments.Kind = kind;
            return builder;
        }

        /// <summary>
        /// Enfoces https traffic
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder EnableHttpsTrafficOnly(this StorageAccountBuilder builder)
        {
            builder.Arguments.EnableHttpsTrafficOnly = true;
            return builder;
        }

        /// <summary>
        /// Removes the https traffic enforcement
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder DisableHttpsTrafficOnly(this StorageAccountBuilder builder)
        {
            builder.Arguments.EnableHttpsTrafficOnly = false;
            return builder;
        }

        /// <summary>
        /// Configures a custom domain for this storage
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <param name="useSubDomain"></param>
        /// <returns></returns>
        public static StorageAccountBuilder UseCustomDomain(this StorageAccountBuilder builder, Input<string> name, Input<bool> useSubDomain)
        {
            builder.Arguments.CustomDomain = new CustomDomainArgs
            {
                Name = name,
                UseSubDomainName = useSubDomain
            };

            return builder;
        }

        /// <summary>
        /// Configures a custom domain for this storage
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static StorageAccountBuilder UseCustomDomain(this StorageAccountBuilder builder, Input<CustomDomainArgs> args)
        {
            builder.Arguments.CustomDomain = args;
            return builder;
        }

        /// <summary>
        /// Allows public access to this storage
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder AllowPublicAccess(this StorageAccountBuilder builder)
        {
            builder.Arguments.AllowBlobPublicAccess = true;
            return builder;
        }

        /// <summary>
        /// Denies public access to this storage
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder DenyPublicAccess(this StorageAccountBuilder builder)
        {
            builder.Arguments.AllowBlobPublicAccess = false;
            return builder;
        }

        /// <summary>
        /// Allows the access with a SAS key
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder AllowSharedKeyAccess(this StorageAccountBuilder builder)
        {
            builder.Arguments.AllowSharedKeyAccess = true;
            return builder;
        }

        /// <summary>
        /// Denies the access with a SAS key
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder DenySharedKeyAccess(this StorageAccountBuilder builder)
        {
            builder.Arguments.AllowSharedKeyAccess = false;
            return builder;
        }
    }
}