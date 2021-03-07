using System;
using Pulumi;

namespace Stize.Infrastructure.Strategies
{
    public interface INamingStrategy : IStrategy
    {
        /// <summary>
        /// Generate the name for a resource
        /// </summary>
        /// <returns></returns>
        Output<string> GenerateName(Input<string> name);
    }
}
