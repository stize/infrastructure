using Pulumi;
using Stize.Infrastructure.Azure.Networking;

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

            var zone = new PrivateDnsZoneBuilder("zone1")
                .Name("zone1")
                .Location("global")
                .In(rg.Name)
                .Build();
        }
    }
}
