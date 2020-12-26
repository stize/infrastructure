using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;

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
        /// Builds the Virtual network
        /// </summary>
        /// <param name="cro">Custom Resource Object</param>
        /// <returns></returns>
        public override VirtualNetwork Build(CustomResourceOptions cro)
        {
            var vnet = new VirtualNetwork(Name, Arguments, cro);
            return vnet;
        }
    }
}
