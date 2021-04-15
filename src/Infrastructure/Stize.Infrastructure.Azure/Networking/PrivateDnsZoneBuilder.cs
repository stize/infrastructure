using Pulumi;
using Pulumi.AzureNative.Network;
using Stize.Infrastructure.Strategies;
using System.Collections.Generic;

namespace Stize.Infrastructure.Azure.Networking
{
    public class PrivateDnsZoneBuilder : BaseBuilder<PrivateZone>
    {
        /// <summary>
        /// Private DNS Zone Arguments
        /// </summary>
        public PrivateZoneArgs Arguments { get; private set; } = new PrivateZoneArgs() { Location = "Global" };

        /// <summary>
        /// List of vnet link configurations used to link vnets to the private DNS zone
        /// </summary>
        public List<VirtualNetworkLinkArgs> VnetLinks { get; private set; } = new List<VirtualNetworkLinkArgs>();
        /// <summary>
        /// Creates a new instance of <see cref="PrivateDnsZoneBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        public PrivateDnsZoneBuilder(string name) : base(name)
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="PrivateDnsZoneBuilder"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="context"></param>
        public PrivateDnsZoneBuilder(string name, ResourceContext context) : base(name, context)
        {

        }

        private void LinkVnets(List<VirtualNetworkLinkArgs> vnetLinks, PrivateZone zone)
        {
            foreach (var link in vnetLinks)
            {
                var cro = new CustomResourceOptions() { DependsOn = zone };
                new VirtualNetworkLink(link.VirtualNetworkLinkName.Apply(l => l).GetValueAsync().Result, link, cro);
            }
        }


        /// <summary>
        /// Creates the Pulumi Private DNS Zone resource object
        /// </summary>
        /// <param name="cro"></param>
        /// <returns></returns>
        public override PrivateZone Build(CustomResourceOptions cro)
        {
            ResourceStrategy.Tagging.AddTags(Arguments.Tags);
            var zone = new PrivateZone(Name, Arguments, cro);
            LinkVnets(VnetLinks, zone);
            return zone;
        }
    }
}


