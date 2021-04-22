using Pulumi;
using Stize.Infrastructure.Azure.Networking;
using Stize.Infrastructure.Azure.Storage;

namespace Stize.Infrastructure.Azure.Tests.Networking.Stacks
{
    class PrivateDnsZoneBasicStack : Stack
    {
        public PrivateDnsZoneBasicStack()
        {
            var rg = new ResourceGroupBuilder("rg1")
                .Name("rg1")
                .Location("westeurope")
                .Build();
            var vnet = new VNetBuilder("vnet1")
                .Name("vnet1")
                .Location(rg.Location)
                .ResourceGroup(rg.Name)
                .AddressSpace("172.68.0.0/16")
                .Build();
            var subnet = new SubnetBuilder("subnet1")
                .Name("subnet1")
                .AddressPrefix("172.68.0.0/16")
                .InVNet(vnet.Name)
                .ResourceGroup(rg.Name)
                .Build();
            var zone = new PrivateDnsZoneBuilder("zone1")
                .Location("global")
                .In(rg.Name)
                .ForBlobStorage()
                .LinkTo(vnet, "link1", true)
                .Build();
        }
    }
}
