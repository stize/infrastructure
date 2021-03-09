using Pulumi;
using Pulumi.AzureNative.Network;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.Networking
{
    public class NetworkSecurityGroupBuilder : BaseBuilder<NetworkSecurityGroup>
    {
        /// <summary>
        /// Network Security Group arguments
        /// </summary>
        public NetworkSecurityGroupArgs Arguments { get; private set; } = new NetworkSecurityGroupArgs();

        /// <summary>
        /// Creates a new instance of <see cref="NetworkSecurityGroupBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        public NetworkSecurityGroupBuilder(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="NetworkSecurityGroupBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        public NetworkSecurityGroupBuilder(string name, ResourceContext context) : base(name, context)
        {
        }

        /// <summary>
        /// Builds the Network security group
        /// </summary>
        /// <param name="cro">Custom Resource Object</param>
        /// <returns></returns>
        public override NetworkSecurityGroup Build(CustomResourceOptions cro)
        {
            var nsg = new NetworkSecurityGroup(Name, Arguments, cro);
            return nsg;
        }
    }
}
