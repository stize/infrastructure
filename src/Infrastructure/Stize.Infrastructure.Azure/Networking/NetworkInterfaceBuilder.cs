using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;

namespace Stize.Infrastructure.Azure.Networking
{
    /// <summary>
    /// Network Interface class builder
    /// </summary>
    public class NetworkInterfaceBuilder : BaseBuilder<NetworkInterface>
    {
        /// <summary>
        /// NI arguments
        /// </summary>
        public NetworkInterfaceArgs Arguments { get; private set; } = new NetworkInterfaceArgs();

        /// <summary>
        /// Creates a new instance of <see cref="NetworkInterfaceBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        public NetworkInterfaceBuilder(string name) : base(name)
        {

        }

        /// <summary>
        /// Builds the Network interface
        /// </summary>
        /// <param name="cro">Custom Resource Object</param>
        /// <returns></returns>
        public override NetworkInterface Build(CustomResourceOptions cro)
        {
            var vnet = new NetworkInterface(Name, Arguments, cro);
            return vnet;
        }
    }
}
