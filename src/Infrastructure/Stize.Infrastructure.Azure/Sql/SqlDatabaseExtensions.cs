using System;
using Pulumi;
using Pulumi.Azure.Sql.Inputs;

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
            builder.Arguments.Name = name;
            return builder;
        }

        /// <summary>
        /// Sets the server this database is created
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
        /// Sets the database edition
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="edition"></param>
        /// <returns></returns>
        public static SqlDatabaseBuilder Edition(this SqlDatabaseBuilder builder, Input<string> edition)
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
            builder.Arguments.ReadScale = true;
            return builder;
        }
    }
}