using System;
namespace Stize.Infrastructure.Strategies
{
    /// <summary>
    /// Set of startegies for a resource
    /// </summary>
    public interface IResourceStrategy
    {
        /// <summary>
        /// Naming strategy for the resources
        /// </summary>
        public INamingStrategy Naming { get; }

        /// <summary>
        /// Tagging strategy for the resources
        /// </summary>
        public ITaggingStrategy Tagging { get; }
    }
}
