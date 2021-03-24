using Pulumi;
using Stize.Infrastructure.Azure.Networking;

namespace Stize.Infrastructure.Azure.Tests.Networking.Stacks
{
    class PrivateDnsZoneVnetLinkBasicStack : Stack
    {
        public PrivateDnsZoneVnetLinkBasicStack()
        {
            var rg = new ResourceGroupBuilder("rg1")
                .Name("rg1")
                .Location("westeurope")
                .Build();

            var vnet = new VNetBuilder("vnet1")
                .Name("vnet1")
                .Location("ukwest")
                .ResourceGroup(rg.Name)
                .AddressSpace("172.16.0.0/24")
                .Build();

            var link = new PrivateDnsZoneVnetLinkBuilder("link1")
                .Name("link1")
                .Location("global")
                .PrivateDnsZone("zone1")
                .LinkTo(vnet)
                .In(rg.Name)
                .EnableAutoRegistration()
                .Build();
        }
    }
}
