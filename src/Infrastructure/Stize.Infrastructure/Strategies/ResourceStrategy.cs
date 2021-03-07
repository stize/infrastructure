using System;
using Pulumi;

namespace Stize.Infrastructure.Strategies
{
    /// <summary>
    /// Default resource strategy class
    /// </summary>
    public class ResourceStrategy : IResourceStrategy
    {

        /// <summary>
        /// The naming strategy to use in all the resources by default
        /// </summary>
        public INamingStrategy Naming { get; private set; }

        /// <summary>
        /// The tagging strategy to use in all the resources by default
        /// </summary>
        public ITaggingStrategy Tagging { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="ResourceStrategy"/>
        /// </summary>
        public ResourceStrategy(INamingStrategy namingStrategy, ITaggingStrategy taggingStrategy)
        {
            this.Naming = namingStrategy;
            this.Tagging = taggingStrategy;
        }

        /// <summary>
        /// Creates the default tagging strategy for the given RandomId and Environment values
        /// </summary>
        /// <param name="randomid"></param>
        /// <param name="environment"></param>
        /// <returns>The default resource strategy</returns>
        // TODO Find a better way to create resource strategies
        public static IResourceStrategy Default(ResourceContext context)
        {
            var naming = new DefaultNamingStrategy(context);
            var tagging = new DefaultTaggingStrategy(context);
            return new ResourceStrategy(naming, tagging);
        }
    }
}
