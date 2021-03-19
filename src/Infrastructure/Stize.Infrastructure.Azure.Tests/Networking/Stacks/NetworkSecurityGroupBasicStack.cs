using System;
using Pulumi;
using Pulumi.AzureNative.Network;
using Stize.Infrastructure.Azure;
using Stize.Infrastructure.Azure.Networking;

namespace Stize.Infrastructure.Azure.Tests.Networking.Stacks
{
    public class NetworkSecurityGroupBasicStack : Stack
    {
        public NetworkSecurityGroupBasicStack()
        {

            var rg = new ResourceGroupBuilder("rg1")
                .Name("rg1")
                .Location("westeurope")
                .Build();
            var tags = new InputMap<string> { { "env", "dev" } };
            var nsg = new NetworkSecurityGroupBuilder("nsg1")
                .Location("westeurope")
                .ResourceGroup(rg.Name)
                .Name("nsg1")
                .Tags(tags)
                .Build();
        }
    }
}
