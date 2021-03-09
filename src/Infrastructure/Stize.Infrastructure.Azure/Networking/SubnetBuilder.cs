using System;
using Pulumi;
using Pulumi.AzureNative.Network;
using Pulumi.Random;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.Networking
{
    public class SubnetBuilder : BaseBuilder<Subnet>
    {
        /// <summary>
        /// VNet arguments
        /// </summary>
        public SubnetArgs Arguments { get; private set; } = new SubnetArgs();

        /// <summary>
        /// Creates a new instance of <see cref="SubnetBuilder"/>
        /// </summary>
        /// <param name="name">Subnet internal name</param>
        public SubnetBuilder(string name) : base(name)
        {
        }
        /// <summary>
        /// Creates a new instance of <see cref="SubnetBuilder"/>
        /// </summary>
        /// <param name="name">Subnet internal name</param>
        public SubnetBuilder(string name, ResourceContext context) : base(name, context)
        {
        }

        /// <summary>
        /// Creates the Pulumi Subnet resource object 
        /// </summary>
        /// <param name="cro">Custom Resource Options</param>
        /// <returns></returns>
        public override Subnet Build(CustomResourceOptions cro)
        {
            var subnet = new Subnet(Name, Arguments, cro);
            return subnet;
        }       
    }
}
