using System;
using Pulumi;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.Networking;

namespace Stize.Infrastructure.Tests.Azure.Networking.Stacks
{
    public class NetworkingBasicStack : Stack
    {
        public NetworkingBasicStack()
        {

            var rg = new ResourceGroupBuilder("rg1")
            .Name("rg1")
            .Location("westeurope")
            .Build();

            var builder = new VNetBuilder("vnet1");
            builder            
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("vnet1")
                .AddressSpace("172.16.0.0/24");

            builder.Build();
        }
    }
}
