using Pulumi;
using Pulumi.AzureNative.Sql;
using Pulumi.AzureNative.Sql.Latest;

namespace Stize.Infrastructure.Azure.Sql
{
    public static class SqlDatabaseExtensions
    {
        /// <summary>
        /// Sets the database name
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder Name(this SqlDatabaseBuilder builder, Input<string> name)
        {
            builder.Arguments.DatabaseName = name;
            return builder;
        }

        /// <summary>
        /// Azure SQL server on which the database should be created on
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder Server(this SqlDatabaseBuilder builder, Input<string> serverName)
        {
            builder.Arguments.ServerName = serverName;
            return builder;
        }

        /// <summary>
        /// Azure SQL resource group
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="resourceGroup"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder ResourceGroup(this SqlDatabaseBuilder builder, Input<string> resourceGroup)
        {
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }

        /// <summary>
        /// Database location
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="localtion"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder Location(this SqlDatabaseBuilder builder, Input<string> localtion)
        {
            builder.Arguments.Location = localtion;
            return builder;
        }

        /// <summary>
        /// Sets the elastic pool id
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder ElasticPoolId(this SqlDatabaseBuilder builder, Input<string> id)
        {
            builder.Arguments.ElasticPoolId = id;
            return builder;
        }

        /// <summary>
        //     The edition/tier of the database to be created. Applies only if `create_mode` is `Default`.
        //     Valid values are: `Basic`, `Standard`, `Premium`, `DataWarehouse`, `Business`,
        //     `BusinessCritical`, `Free`, `GeneralPurpose`, `Hyperscale`, `Premium`, `PremiumRS`,
        //     `Standard`, `Stretch`, `System`, `System2`, or `Web`. Please see [Azure SQL Database
        //     Service Tiers](https://azure.microsoft.com/en-gb/documentation/articles/sql-database-service-tiers/).
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="edition"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder SkuTier(this SqlDatabaseBuilder builder, Input<string> edition)
        {
            builder.SkuArguments.Tier = edition;
            return builder;
        }

        /// <summary>
        /// Restores the database from an existing one
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder RestoreFrom(this SqlDatabaseBuilder builder, Input<string> databaseId)
        {
            builder.Arguments.CreateMode = Pulumi.AzureNative.Sql.CreateMode.Restore;
            builder.Arguments.SourceDatabaseId = databaseId;
            return builder;
        }

        /// <summary>
        /// Enables the read scale
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder ReadScale(this SqlDatabaseBuilder builder)
        {
            builder.Arguments.ReadScale = DatabaseReadScale.Enabled;
            return builder;
        }

        /// <summary>
        //  The Sku name / service objective name for the database. Valid values depend on edition and
        //  location and may include `S0`, `S1`, `S2`, `S3`, `P1`, `P2`, `P4`, `P6`, `P11`
        //  and `ElasticPool`. You can list the available names with the cli: ```shell az
        //  sql db list-editions -l westus -o table ```. For further information please see
        //  [Azure CLI - az sql db](https://docs.microsoft.com/en-us/cli/azure/sql/db?view=azure-cli-latest#az-sql-db-list-editions).
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="objectiveName"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder SkuServiceObjectiveName(this SqlDatabaseBuilder builder, Input<string> objectiveName)
        {
            builder.SkuArguments.Name = objectiveName;
            return builder;
        }

        /// <summary>
        /// Capacity of the particular SKU
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="capacity"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder SkuCapacity(this SqlDatabaseBuilder builder, Input<int> capacity)
        {
            builder.SkuArguments.Capacity = capacity;
            return builder;
        }

        /// <summary>
        /// Size of the particular SKU
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder SkuSize(this SqlDatabaseBuilder builder, Input<string> size)
        {
            builder.SkuArguments.Size = size;
            return builder;
        }

        /// <summary>
        /// Family of the particular SKU. 
        /// If the service has different generations of hardware, for the same SKU, then that can be captured here.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="family"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder SkuFamily(this SqlDatabaseBuilder builder, Input<string> family)
        {
            builder.SkuArguments.Family = family;
            return builder;
        }
    }
}