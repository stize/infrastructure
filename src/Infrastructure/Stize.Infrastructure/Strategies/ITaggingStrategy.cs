using System;
using Pulumi;
namespace Stize.Infrastructure.Strategies
{
    /// <summary>
    /// Strategy to tag all Pulumi resources
    /// </summary>
    public interface ITaggingStrategy
    {
        /// <summary>
        /// Addts the required tags to a resource
        /// </summary>
        /// <param name="tags">Existing resource tags</param>
        void AddTags(InputMap<string> tags);
    }
}
