using Pulumi;

namespace Stize.Infrastructure.Azure
{
    public static class ResourceGroupExtensions
    {
        /// <summary>
        /// Sets the builder name using the builder resource strategy
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="name">Builder name</param>
        /// <returns>The builder argument</returns>
        public static ResourceGroupBuilder Name(this ResourceGroupBuilder builder, Input<string> name)
        {
            builder.Arguments.ResourceGroupName = name;
            return builder;
        }

        /// <summary>
        /// Sets the location on which the resource should be created on
        /// </summary>
        /// <param name="builder">Builder instance</param>
        /// <param name="location">Resource location</param>
        /// <returns></returns>
        public static ResourceGroupBuilder Location(this ResourceGroupBuilder builder, Input<string> location)
        {
            builder.Arguments.Location = location;
            return builder;
        }

        /// <summary>
        /// Sets the tags for this Resource Group
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static ResourceGroupBuilder Tags(this ResourceGroupBuilder builder, InputMap<string> tags)
        {
            builder.Arguments.Tags = tags;
            return builder;
        }
    }
}