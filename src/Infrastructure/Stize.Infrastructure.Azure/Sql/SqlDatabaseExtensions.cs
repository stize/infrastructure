using Pulumi;
using Pulumi.AzureNative.Sql;

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
        /// Generates a new SQL server to host the database, using the provided admin login credentials for the server. 
        /// Useful for when creating a geo-replica backup for a database.
        /// The location and resource group arguments for the new server are assumed from the database.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="serverName">Name for the new server</param>
        /// <param name="adminLogin">Administrator login for the new server</param>
        /// <param name="adminPassword">Administrator password for the new server</param>
        /// <returns></returns>
        public static SqlDatabaseBuilder Server(this SqlDatabaseBuilder builder, Input<string> serverName, Input<string> adminLogin, Input<string> adminPassword)
        {
            builder.NewServerArgs = new ServerArgs
            {
                ServerName = serverName,
                AdministratorLogin = adminLogin,
                AdministratorLoginPassword = adminPassword
            };
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
        /// Restores a deleted database using the Restorable Dropped Database Resource ID.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="databaseId">Resource ID of the Restorable Dropped Database to restore from</param>
        /// <param name="pointInTime">Point in time that you want to restore from. Only specify to restore from an earlier point in time.</param>
        /// <returns></returns>
        public static SqlDatabaseBuilder CreateAsRestoreOf(this SqlDatabaseBuilder builder, Input<string> databaseId, Input<string> pointInTime = null)
        {
            /* TODO: Improve usability of the method
             * "sourceDatabaseId must be the restorable dropped database resource ID and sourceDatabaseDeletionDate is ignored." - https://www.pulumi.com/docs/reference/pkg/azure-native/sql/database/#createmode_csharp
             * ^ documentation states to assign SourcedDatabaseId - must use RestorableDroppedDatabaseId when using CreateMode.Restore
             * Currently, user must pass the RestorableDroppedDatabaseId. This isn't friendly though as they would need to know the id.
             * More friendly for user to input a db name, server name, and rg name (and optional point in time to restore for more earlier backups)
             * It is possible to access a dropped (deleted) database using PS command Get-AzureRmSqlDeletedDatabaseBackup and providing parameters (resourcegroup, server, database)
             * There is a GetDatabase function provided by Pulumi, but it doesn't seem to be able to grab deleted databases. and there is no GetDeletedDatabase function.
            */
            builder.Arguments.CreateMode = CreateMode.Restore;
            builder.Arguments.RestorableDroppedDatabaseId = databaseId;
            builder.Arguments.RestorePointInTime = pointInTime ?? null;
            return builder;
        }

        /// <summary>
        /// Restores a deleted database using the database's original resource ID.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="databaseId">Original Resource ID of the database to restore from.</param>
        /// <param name="deletionDate">Deletion date of the database in (ISO 8601 format).</param>
        /// <param name="pointInTime">Point in time that you want to restore from (ISO 8601 format). Only specify to restore from an earlier point in time.</param>
        /// <returns></returns>
        public static SqlDatabaseBuilder CreateAsRestoreOf(this SqlDatabaseBuilder builder, Input<string> databaseId, Input<string> deletionDate, 
            Input<string> pointInTime = null)
        {
            /* Same comments SetAsRestore() method. 
             * This method seems redundant as you don't need to set SourceDatabaseDeletionDate to restore a deleted database - the RestorableDroppedDatabaseId will suffice.
             * "If sourceDatabaseId is the database’s original resource ID, then sourceDatabaseDeletionDate must be specified." - https://www.pulumi.com/docs/reference/pkg/azure-native/sql/database/#createmode_csharp
             * ^ documentation states to assign SourceDatabaseId with the original resource Id of the database - though, CreateMode.Restore requires RestorableDroppedDatabaseId.
            */
            builder.Arguments.CreateMode = CreateMode.Restore;
            builder.Arguments.RestorableDroppedDatabaseId = databaseId;
            builder.Arguments.SourceDatabaseDeletionDate = deletionDate;
            builder.Arguments.RestorePointInTime = pointInTime ?? null;
            return builder;
        }

        /// <summary>
        /// Sets this database to be a secondary replica of an existing database, using the existing primary database's resource ID.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="databaseId">Resource ID of the primary database</param>
        /// <returns></returns>
        public static SqlDatabaseBuilder CreateAsSecondary(this SqlDatabaseBuilder builder, Input<string> databaseId)
        {
            builder.Arguments.CreateMode = CreateMode.Secondary;
            builder.Arguments.SourceDatabaseId = databaseId;
            return builder;
        }

        /// <summary>
        /// Sets the database to be a copy of an existing database
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="databaseId">Resource ID of the database to copy</param>
        /// <returns></returns>
        public static SqlDatabaseBuilder CreateAsCopy(this SqlDatabaseBuilder builder, Input<string> databaseId)
        {
            builder.Arguments.CreateMode = CreateMode.Copy;
            builder.Arguments.SourceDatabaseId = databaseId;
            return builder;
        }

        /// <summary>
        /// "Sets the database to be a restoration of a geo-replicated backup database."
        /// https://www.pulumi.com/docs/reference/pkg/azure-native/sql/database/#createmode_csharp
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="databaseId">Recoverable Database Resource ID of the database to restore</param>
        /// <returns></returns>
        public static SqlDatabaseBuilder CreateAsRecoveryOf(this SqlDatabaseBuilder builder, Input<string> databaseId)
        {
            /* TODO: May need improvement and needs to be properly tested!
             * User must pass in the recoverableDatabaseId, which is associated with a database stored in the recoverable databases of the server.
             * Format of recoverableDatabaseId:
             * 'subscriptions/subscription-id/resourceGroups/resource-group-name/providers/Microsoft.Sql/servers/server-name/recoverableDatabases/recoverable-database-name'
            */
            builder.Arguments.CreateMode = CreateMode.Recovery;
            builder.Arguments.RecoverableDatabaseId = databaseId;
            return builder;
        }

        /// <summary>
        /// Sets the database to be a restoration of a point in time of an existing database, using the existing database's resource ID and the point in time (ISO8601 format) to restore from.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="databaseId">Resource ID of the database to restore from</param>
        /// <param name="restorePointInTime">The point in time (ISO8601 format) of the source database</param>
        /// <returns></returns>
        public static SqlDatabaseBuilder CreateAsPointInTimeRestore(this SqlDatabaseBuilder builder, Input<string> databaseId, Input<string> restorePointInTime)
        {
            builder.Arguments.CreateMode = CreateMode.PointInTimeRestore;
            builder.Arguments.SourceDatabaseId = databaseId;
            builder.Arguments.RestorePointInTime = restorePointInTime;
            return builder;
        }

        /// <summary>
        /// Creates a database by restoring from a long term retention vault. 
        /// recoveryServicesRecoveryPointResourceId must be specified as the recovery point resource ID.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder CreateAsLongTermRetentionRestore(this SqlDatabaseBuilder builder, Input<string> databaseId)
        {
            /* TODO: May need improvement and needs to be properly tested!
             * User must pass in the recoveryServicesRecoveryPointId, which is associated with a database
             * stored in a long term retention vault.
             * Possibly able to get resource id through 'Pulumi.AzureNative.RecoveryServices'
             * Format of RecoveryServicesRecoveryPointId:
             * '/subscriptions/subscription-id/resourceGroups/resource-group-name/providers/Microsoft.RecoveryServices/vault/vault-name/backupFabrics/Azure/protectionContainers/protection-container-name/protectedItems/protected-item-name/recoveryPoints/recovery-point-Id'
            */
            builder.Arguments.CreateMode = CreateMode.RestoreLongTermRetentionBackup;
            builder.Arguments.RecoveryServicesRecoveryPointId = databaseId;
            return builder;
        }

        public static SqlDatabaseBuilder SecondaryType(this SqlDatabaseBuilder builder, InputUnion<string, SecondaryType> secondaryType)
        {
            builder.Arguments.SecondaryType = secondaryType;
            return builder;
        }

        /// <summary>
        /// The state of read-only routing. If enabled, connections that have application intent set to readonly in their 
        /// connection string may be routed to a readonly secondary replica in the same region.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder ReadScale(this SqlDatabaseBuilder builder)
        {
            builder.Arguments.ReadScale = DatabaseReadScale.Enabled;
            return builder;
        }

        /// <summary>
        ///  The Sku name / service objective name for the database. Valid values depend on edition and
        ///  location and may include `S0`, `S1`, `S2`, `S3`, `P1`, `P2`, `P4`, `P6`, `P11`
        ///  and `ElasticPool`. You can list the available names with the cli: ```shell az
        ///  sql db list-editions -l westus -o table ```. For further information please see
        ///  [Azure CLI - az sql db](https://docs.microsoft.com/en-us/cli/azure/sql/db?view=azure-cli-latest#az-sql-db-list-editions).
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
        ///     The edition/tier of the database to be created. Applies only if `create_mode` is `Default`.
        ///     Valid values are: `Basic`, `Standard`, `Premium`, `DataWarehouse`, `Business`,
        ///     `BusinessCritical`, `Free`, `GeneralPurpose`, `Hyperscale`, `Premium`, `PremiumRS`,
        ///     `Standard`, `Stretch`, `System`, `System2`, or `Web`. Please see [Azure SQL Database
        ///     Service Tiers](https://azure.microsoft.com/en-gb/documentation/articles/sql-database-service-tiers/).
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

        /// <summary>
        /// Provides enhanced availability by spreading replicas across availability zones within one region.
        /// Sets the Zone Redundant property to 'true'.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder EnableZoneRedundant(this SqlDatabaseBuilder builder)
        {
            builder.Arguments.ZoneRedundant = true;
            return builder;
        }

        /// <summary>
        /// The max size of the database expressed in bytes.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="size">Provide max database size in GB.</param>
        /// <returns></returns>
        public static SqlDatabaseBuilder MaxDatabaseSizeGB(this SqlDatabaseBuilder builder, Input<double> size)
        {
            var bytes = size.Apply(e => e * 1073741824);
            builder.Arguments.MaxSizeBytes = bytes;
            return builder;
        }

        /// <summary>
        /// Minimal capacity that database will always have allocated, if not paused
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="capacity"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder MinCapacity(this SqlDatabaseBuilder builder, Input<double> capacity)
        {
            builder.Arguments.MinCapacity = capacity; // No idea what capacity actually is - TODO: find out what capacity is/does
            return builder;
        }

        /// <summary>
        /// The storage account type used to store backups for this database
        /// Default: 'GRS'
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="saType"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder StorageAccountType(this SqlDatabaseBuilder builder, InputUnion<string, StorageAccountType> saType = null)
        {
            builder.Arguments.StorageAccountType = saType ?? "GRS";
            return builder;
        }
        /// <summary>
        /// Database collation defines the rules that sort and compare data, and cannot be changed after database creation.
        /// The default database collation is 'SQL_Latin1_General_CP1_CI_AS'.
        /// For more details: https://docs.microsoft.com/en-us/sql/relational-databases/collations/collation-and-unicode-support?view=sql-server-ver15
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collation"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder DatabaseCollation(this SqlDatabaseBuilder builder, Input<string> collation = null)
        {
            builder.Arguments.Collation = collation ?? "SQL_Latin1_General_CP1_CI_AS";
            return builder;
        }

        /// <summary>
        /// Sample data to populate the database
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sampleData">Sample data type</param>
        /// <returns></returns>
        public static SqlDatabaseBuilder SampleData(this SqlDatabaseBuilder builder, InputUnion<string, SampleName> sampleData)
        {
            builder.Arguments.SampleName = sampleData;
            return builder;
        }
    }
}