using System;
using Pulumi;
using Pulumi.AzureNative.Sql.Latest;
using Pulumi.AzureNextGen.Sql.Latest;

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
        /// Sets the elastic pool name
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder ElasticPoolName(this SqlDatabaseBuilder builder, Input<string> name)
        {
            builder.Arguments.ElasticPoolName = name;
            return builder;
        }

        /// <summary>
        //     The edition of the database to be created. Applies only if `create_mode` is `Default`.
        //     Valid values are: `Basic`, `Standard`, `Premium`, `DataWarehouse`, `Business`,
        //     `BusinessCritical`, `Free`, `GeneralPurpose`, `Hyperscale`, `Premium`, `PremiumRS`,
        //     `Standard`, `Stretch`, `System`, `System2`, or `Web`. Please see [Azure SQL Database
        //     Service Tiers](https://azure.microsoft.com/en-gb/documentation/articles/sql-database-service-tiers/).
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="edition"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder Edition(this SqlDatabaseBuilder builder, InputUnion<string, Pulumi.AzureNextGen.Sql.Latest.DatabaseEdition> edition)
        {
            builder.Arguments.Edition = edition;
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
            builder.Arguments.CreateMode = "Restore";
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
            builder.Arguments.ReadScale = Pulumi.AzureNextGen.Sql.Latest.ReadScale.Enabled;
            return builder;
        }

        /// <summary>
        //  The service objective name for the database. Valid values depend on edition and
        //  location and may include `S0`, `S1`, `S2`, `S3`, `P1`, `P2`, `P4`, `P6`, `P11`
        //  and `ElasticPool`. You can list the available names with the cli: ```shell az
        //  sql db list-editions -l westus -o table ```. For further information please see
        //  [Azure CLI - az sql db](https://docs.microsoft.com/en-us/cli/azure/sql/db?view=azure-cli-latest#az-sql-db-list-editions).
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="objectiveName"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder RequestedServiceObjectiveName(this SqlDatabaseBuilder builder, InputUnion<string, Pulumi.AzureNextGen.Sql.Latest.ServiceObjectiveName> objectiveName)
        {
            builder.Arguments.RequestedServiceObjectiveName = objectiveName;
            return builder;
        }
    }
}