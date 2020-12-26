using System;
using Pulumi;
using Pulumi.AzureNextGen.Resources.Latest;
using Pulumi.AzureNextGen.Sql.Latest;

namespace Stize.Infrastructure.Azure.Sql
{
    public static class SqlServerExtensions
    {

        /// <summary>
        /// Sets the resource name
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SqlServerBuilder Name(this SqlServerBuilder builder, Input<string> name)
        {
            builder.Arguments.ServerName = name;
            return builder;
        }

        /// <summary>
        /// Assignes the sql server to a resource group
        /// </summary>
        /// <param name="builder">SQL Server builder</param>
        /// <param name="resourceGroup">Resource group name</param>
        /// <returns></returns>
        public static SqlServerBuilder ResourceGroup(this SqlServerBuilder builder, ResourceGroup resourceGroup)
        {
            builder.ResourceGroup(resourceGroup.Name);
            return builder;
        }

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
        public static SqlServerBuilder Version(this SqlServerBuilder builder, InputUnion<string, ServerVersion> version)
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
    }
}