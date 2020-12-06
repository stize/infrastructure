using System;
using Pulumi;
using Pulumi.Azure.Network;

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
        public SubnetBuilder()
        {            
        }

        public override Subnet Build(CustomResourceOptions cro)
        {
            var subnet = new Subnet($"{Arguments.Name}-subnet", Arguments, cro);
            return subnet;
        }
    }
}
