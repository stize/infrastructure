using System;
using Pulumi;

namespace Stize.Infrastructure.Strategies
{
    /// <summary>
    /// Default naming strategy for resources
    /// </summary>
    public class DefaultNamingStrategy : INamingStrategy
    {

        /// <summary>
        /// The resource context
        /// </summary>
        private ResourceContext context;

        /// <summary>
        /// Creates a new instance of <see cref="DefaultNamingStrategy"/>
        /// </summary>
        public DefaultNamingStrategy(ResourceContext context)
        {
            this.context = context;
        }

        public Output<string> GenerateName(Input<string> name)
        {
            Output<string> result;

            if (name == null) throw new ArgumentNullException("name", "A resource name cannot be null");
            result = name.Apply(n => n);

            if (context.Environment != null)
            {
                result = result.Apply(r => context.Environment.Apply(e => $"{r}-{e}"));
            }

            if (context.RandomId != null)
            {
                result = result.Apply(r => context.RandomId.Apply(rid => $"{r}-{rid}"));
            }

            return result;
        }
    }
}
