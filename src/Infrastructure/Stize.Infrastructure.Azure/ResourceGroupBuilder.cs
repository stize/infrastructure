using System;
using Pulumi;
using Pulumi.Azure.Core;

namespace Stize.Infrastructure.Azure
{
    public class ResourceGroupBuilder : BaseBuilder<ResourceGroup>
    {

        public ResourceGroupArgs Arguments { get; private set; } = new ResourceGroupArgs();

        /// <summary>
        /// Creates a new instance of <see="ResourceGroupBuilder" />
        /// </summary>
        /// <param name="name">Pulumi internal name</param>
        public ResourceGroupBuilder(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of <see="ResourceGroupBuilder" />
        /// </summary>
        /// <param name="name">Pulumi internal name</param>
        /// <param name="arguments">Default resource group arguments</param>        
        public ResourceGroupBuilder(string name, ResourceGroupArgs arguments) : base(name)
        {
            this.Arguments = arguments;
        }

        /// <summary>
        /// Builds the resource using the given cro
        /// </summary>
        /// <param name="cro">Custom Resource Options</param>
        /// <returns></returns>
        public override ResourceGroup Build(CustomResourceOptions cro)
        {
            var rg = new ResourceGroup(Name, Arguments, cro);
            return rg;
        }
    }
}