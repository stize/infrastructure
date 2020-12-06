using System;
using Pulumi;
using Stize.Infrastructure.Azure.Networking;

namespace Stize.Infrastructure.Tests.Azure.Networking.Stacks
{
    public class SubnetBasicStack : Stack
    {
        public SubnetBasicStack()
        {
            var vnet = new VNetBuilder()
                .Name("vnet1")
                .Location("westeurope")
                .ResourceGroup("my-resource-group")
                .AddressSpace("172.16.0.0/24")
                .Build();

            var subnet = new SubnetBuilder()
                .Name("subnet1")
                .ResourceGroup(vnet.ResourceGroupName)
                .InVNet(vnet.Name)
                .AddressPrefix("172.16.0.0/24")
                .Build();
        }
    }
}
