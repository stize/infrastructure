using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;

namespace Stize.Infrastructure.Azure.Networking
{
    public static class ApplicationSecurityGroupExtensions
    {
        /// <summary>
        /// Sets the resource group the <see cref="ApplicationSecurityGroup" /> will be created on
        /// </summary>
        /// <param name="builder"><see cref="ApplicationSecurityGroup" /> Builder</param>
        /// <param name="resourceGroup">Resource group name</param>
        /// <returns></returns>
        public static ApplicationSecurityGroupBuilder ResourceGroup(this ApplicationSecurityGroupBuilder builder, Input<string> resourceGroup)
        {
            builder.Arguments.ResourceGroupName = resourceGroup;
            return builder;
        }

        /// <summary>
        /// Sets the builder name. If the builder has an RandomId associated, 
        /// appends the hex value of the RandomId to the end of the name
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="name">Builder name</param>
        /// <returns>The builder argument</returns>
        public static ApplicationSecurityGroupBuilder Name(this ApplicationSecurityGroupBuilder builder, Input<string> name)
        {            
            builder.Arguments.ApplicationSecurityGroupName = name;
            return builder;
        }

        /// <summary>
        /// Sets the location on which the resource should be created on
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="location">Resource location</param>
        /// <returns></returns
        public static ApplicationSecurityGroupBuilder Location(this ApplicationSecurityGroupBuilder builder, Input<string> location)
        {
            builder.Arguments.Location = location;
            return builder;
        }
        /// <summary>
        /// Sets the tags for the resource
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="tags">Resource tags</param>
        /// <returns></returns
        public static ApplicationSecurityGroupBuilder Tags(this ApplicationSecurityGroupBuilder builder, InputMap<string> tags)
        {
            builder.Arguments.Tags = tags;
            return builder;
        }
    }
}
