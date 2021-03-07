using System;
using Pulumi;

namespace Stize.Infrastructure.Strategies
{
    public class DefaultTaggingStrategy : ITaggingStrategy
    {

        private ResourceContext context;

        /// <summary>
        /// Creates a new instance of <see cref="DefaultNamingStrategy"/>
        /// </summary>
        public DefaultTaggingStrategy(ResourceContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Applies the default tagging strategy
        /// </summary>
        /// <returns></returns>
        public void AddTags(InputMap<string> tags)
        {
            if (context.Environment != null) tags.Add("environment", context.Environment);
            if (context.RandomId != null) tags.Add("instanceId", context.RandomId);
            if (context.ManagedBy != null) tags.Add("managedBy", context.ManagedBy);
            tags.Add("createdWith", "pulumi");
        }
    }
}
