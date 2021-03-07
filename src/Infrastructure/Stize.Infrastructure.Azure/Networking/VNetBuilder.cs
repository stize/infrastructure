using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Stize.Infrastructure.Strategies;

namespace Stize.Infrastructure.Azure.Networking
{
    /// <summary>
    /// VNet class builder
    /// </summary>
    public class VNetBuilder : BaseBuilder<VirtualNetwork>
    {
        /// <summary>
        /// VNet arguments
        /// </summary>
        public VirtualNetworkArgs Arguments { get; private set; } = new VirtualNetworkArgs();

        /// <summary>
        /// Creates a new instance of <see cref="VNetBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        public VNetBuilder(string name) : base(name)
        {         
        }
        /// <summary>
        /// Creates a new instance of <see cref="VNetBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="context"></param>
        public VNetBuilder(string name, ResourceContext context) : base(name, context)
        {
        }
        
        /// <summary>
        /// Creates a new instance of <see cref="SubnetBuilder"/>
        /// </summary>
        /// <param name="name">Subnet internal name</param>
        /// <param name="context">The resource context</param>
        /// <param name="cro">The CustomResourceOptions</param>
        public VNetBuilder(string name, ResourceContext context, CustomResourceOptions cro) : base(name, context, cro)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="SubnetBuilder"/>
        /// </summary>
        /// <param name="name">Subnet internal name</param>
        public VNetBuilder(string name, CustomResourceOptions cro) : base(name, cro)
        {
        }

        /// <summary>
        /// Builds the Virtual network
        /// </summary>
        /// <param name="cro">Custom Resource Object</param>
        /// <returns></returns>
        public override VirtualNetwork Build(CustomResourceOptions cro)
        {
            Arguments.VirtualNetworkName = ResourceStrategy.Naming.GenerateName(Arguments.VirtualNetworkName);
            ResourceStrategy.Tagging.AddTags(Arguments.Tags);
            var vnet = new VirtualNetwork(Name, Arguments, cro);
            return vnet;
        }
    }
}
