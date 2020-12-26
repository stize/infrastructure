using System;
using Pulumi;
using Pulumi.Azure.Sql.Inputs;

namespace Stize.Infrastructure.Azure.Sql
{
    public static class SqlServerExtensions
    {
        /// <summary>
        /// Assignes the sql server to a resource group
        /// </summary>
        /// <param name="builder">SQL Server builder</param>
        /// <param name="resourceGroup">Resource group name</param>
        /// <returns></returns>
        public static SqlServerBuilder ResourceGroup(this SqlServerBuilder builder, Input<string> resourceGroup)
        {
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }

        /// <summary>
        /// Sets the SQL Server location
        /// </summary>
        /// <param name="builder">SQL Server builder</param>
        /// <param name="location">Server location</param>
        /// <returns></returns>
        public static SqlServerBuilder Location(this SqlServerBuilder builder, Input<string> location)
        {
            builder.Arguments.Location = location;
            return builder;
        }

        /// <summary>
        /// Sets the SQL Server version to use. Default version is "12.0"
        /// </summary>
        /// <param name="builder">SQL Server builder</param>
        /// <param name="version">Server version</param>
        /// <returns></returns>
        public static SqlServerBuilder Version(this SqlServerBuilder builder, Input<string> version)
        {
            builder.Arguments.Version = version;
            return builder;
        }

        /// <summary>
        /// Sets the default admin login name
        /// </summary>
        /// <param name="builder">SQL Server builder</param>
        /// <param name="login">Default administrator username</param>
        /// <returns></returns>
        public static SqlServerBuilder AdministratorLogin(this SqlServerBuilder builder, Input<string> login)
        {
            builder.Arguments.AdministratorLogin = login;
            return builder;
        }

        /// <summary>
        /// Sets the default admin password
        /// </summary>
        /// <param name="builder">SQL Server builder</param>
        /// <param name="password">Default administrator password</param>
        /// <returns></returns>
        public static SqlServerBuilder AdministratorPassword(this SqlServerBuilder builder, Input<string> password)
        {
            builder.Arguments.AdministratorLoginPassword = password;
            return builder;
        }

        /// <summary>
        /// Sets the Advanced audit policy for this server
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SqlServerBuilder AdvancedAudit(this SqlServerBuilder builder, SqlServerExtendedAuditingPolicyArgs args)
        {
            builder.Arguments.ExtendedAuditingPolicy = args;
            return builder;
        } 
    }
}