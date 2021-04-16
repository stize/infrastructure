using Pulumi;
using Stize.Infrastructure.Azure.Networking;
using Stize.Infrastructure.Azure.Storage;

namespace Stize.Infrastructure.Azure.Tests.Networking.Stacks
{
    class PrivateDnsZoneRecordBasicStack : Stack
    {
        public PrivateDnsZoneRecordBasicStack()
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

            var record = new PrivateDnsZoneRecordBuilder("record1")
                .Name("record1")
                .In(rg.Name)
                .InPrivateDnsZone(zone.Name)
                .CreateARecord("0.0.0.0")
                .TimeToLive(3600)
                .Build();
        }
    }
}
