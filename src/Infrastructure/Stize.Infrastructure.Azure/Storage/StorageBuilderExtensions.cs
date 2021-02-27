using Pulumi;
using Pulumi.AzureNextGen.Storage.Latest;
using Pulumi.AzureNextGen.Storage.Latest.Inputs;

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
            if (builder.RandomId != null)
            {
                builder.Arguments.AccountName = builder.RandomId.Hex.Apply(r => $"{name}-{r}");
            }
            else
            {
                builder.Arguments.AccountName = name;
            }

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
        public static StorageAccountBuilder AllowBlowPublicAccess(this StorageAccountBuilder builder)
        {
            builder.Arguments.AllowBlobPublicAccess = true;
            return builder;
        }

        /// <summary>
        /// Denies public access to blobs
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder DenyBlowPublicAccess(this StorageAccountBuilder builder)
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
            return Kind(builder, Pulumi.AzureNextGen.Storage.Latest.Kind.Storage);
        }

        /// <summary>
        /// Sets the Storage kind to StorageV2
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder StorageV2(this StorageAccountBuilder builder)
        {
            return Kind(builder, Pulumi.AzureNextGen.Storage.Latest.Kind.StorageV2);
        }

        /// <summary>
        /// Sets the Storage kind to BlobStorage
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder Blob(this StorageAccountBuilder builder)
        {
            return Kind(builder, Pulumi.AzureNextGen.Storage.Latest.Kind.BlobStorage);
        }

        /// <summary>
        /// Sets the Storage kind to BlockBlobStorage
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder BlockBlobStorage(this StorageAccountBuilder builder)
        {
            return Kind(builder, Pulumi.AzureNextGen.Storage.Latest.Kind.BlockBlobStorage);
        }

        /// <summary>
        /// Sets the Storage kind to FileStorage
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StorageAccountBuilder FileStorage(this StorageAccountBuilder builder)
        {
            return Kind(builder, Pulumi.AzureNextGen.Storage.Latest.Kind.FileStorage);
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
    }
}