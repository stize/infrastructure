using Pulumi;
using Pulumi.AzureNextGen.Network.Latest;
using Stize.Infrastructure.Strategies;
using Inputs = Pulumi.AzureNextGen.Network.Latest.Inputs;

namespace Stize.Infrastructure.Azure.Networking
{
    public class PrivateDnsZoneVnetLinkBuilder : BaseBuilder<VirtualNetworkLink>
    {
        /// <summary>
        /// Private DNS Zone Arguments
        /// </summary>
        public VirtualNetworkLinkArgs Arguments { get; private set; } = new VirtualNetworkLinkArgs();

        /// <summary>
        /// Creates a new instance of <see cref="VirtualNetworkLink"/>
        /// </summary>
        /// <param name="name"></param>
        public PrivateDnsZoneVnetLinkBuilder(string name) : base(name)
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="VirtualNetworkLink"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="context"></param>
        public PrivateDnsZoneVnetLinkBuilder(string name, ResourceContext context) : base(name, context)
        {

        }

        /// <summary>
        /// Creates the Pulumi Vnet link resource object that links a vnet to a private DNS zone
        /// </summary>
        /// <param name="cro"></param>
        /// <returns></returns>
        public override VirtualNetworkLink Build(CustomResourceOptions cro)
        {
            Arguments.VirtualNetworkLinkName = ResourceStrategy.Naming.GenerateName(Arguments.VirtualNetworkLinkName);
            ResourceStrategy.Tagging.AddTags(Arguments.Tags);
            var zone = new VirtualNetworkLink(Name, Arguments, cro);
            return zone;
        }
    }
}
